namespace TextEditor.Core;

public class FileHandler : IDisposable
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
        while (sr.Peek() >= 0 && index < lineSelector.startingLine + lineSelector.lines)
        {
            var line = sr.ReadLine();
            string lineToAdd = line ?? "";
            if (columnSelector.startingColumn is not null)
            {
                try
                {
                    lineToAdd = line?.Substring(
                    (int)columnSelector.startingColumn, 
                    (int)(columnSelector.columns ?? line.Length - columnSelector.startingColumn) 
                    )!;
                }
                catch(ArgumentOutOfRangeException)
                {
                    if (columnSelector.startingColumn > 0)
                    {
                        lineToAdd = columnSelector.startingColumn < line.Length
                        ? line.Substring(
                            (int)columnSelector.startingColumn,
                            (int)(line.Length - columnSelector.startingColumn)
                        )
                        : "";
                    }
                }
            }

            if (index >= lineSelector.startingLine) 
            {
                buffer.Add(lineToAdd);
            }
            index ++;
        }

        return [.. buffer];
    }

    public void Dispose() => _fs.Dispose();
}