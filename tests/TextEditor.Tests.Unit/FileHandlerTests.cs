
using TextEditor.Core;

namespace TextEditor.Tests.Unit;

public class FileHandlerTests : IAsyncLifetime
{
    private const string FILE_CONTENT = """
        namespace TextEditor.Core;

        internal class FileHandler : IDisposable
        {
            private Stream _fs;

            public FileHandler(string path)
            {
                _fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }

            public string[] StreamLines((int startingLine, int lines) lineSelector, (int? startingColumn, int? columns) columnSelector)
            {
                var buffer = new List<string>();
                using var sr = new StreamReader(_fs);
                int index = 0;
                while (sr.Peek() >= 0 || index < lineSelector.startingLine + lineSelector.lines)
                {
                    var line = sr.ReadLine();
                    string lineToAdd = line ?? "";
                    if (columnSelector.startingColumn is not null)
                    {
                        try
                        {
                            lineToAdd = line?.Substring((int)columnSelector.startingColumn,(int)(columnSelector.columns ?? line.Length - columnSelector.startingColumn));
                        }
                        catch(ArgumentOutOfRangeException e)
                        {
                            if (columnSelector.startingColumn > 0)
                            {
                                lineToAdd = line.Substring(
                                    (int)columnSelector.startingColumn,
                                    (int)(line.Length - columnSelector.startingColumn)
                                );
                            }
                        }
                    }

                    if (index >= lineSelector.startingLine) 
                    {
                        buffer.Add(line);
                    }
                    index ++;
                }

                return [.. buffer];
            }

            public void Dispose() => _fs.Dispose();
        }
        """;

    [Fact]
    public void StreamLinesShould_ReturnArrayOfLines()
    {
        // Arrange
        var sut = new FileHandler("./test.txt");

        // Act
        var result = sut.StreamLines((0, 1), (0, 10));

        // Assert
        Assert.Equal("namespace ", result[0]);
        Assert.Equal("", result[1]);
        Assert.Equal(50, result.Length);
    }

    public Task DisposeAsync()
    {
        var file = new FileInfo("./test.txt");
        file.Delete();

        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        using var fs = File.OpenWrite("./test.txt");
        using var sw = new StreamWriter(fs);

        await sw.WriteAsync(FILE_CONTENT);
    }
}