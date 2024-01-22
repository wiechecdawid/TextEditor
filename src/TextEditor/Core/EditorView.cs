namespace TextEditor.Core;

internal class EditorView
{
    private readonly FileHandler _fileHandler;
    private string _buffer;

    public EditorView(FileHandler fh, int bufferSize)
    {
        
    }
}

internal class FileHandler
{
    private Stream _fs;
    public FileHandler(string path)
    {
        _fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    public string[] GetBufferLines(int start, int lines)
    {
        var buffer = new List<string>();
        using var sr = new StreamReader(_fs);
        int index = 0;
        while (sr.Peek() >= 0 || index < start + lines) {
            var line = sr.ReadLine();
            if (index >= start) 
            {
                buffer.Add(line);
            }
            index ++;
        }

        return [.. buffer];
    }
}