namespace TextEditor.Core;

internal class ConsoleHandler
{
    public void Print(string buffer)
    {
        Console.Clear();
        Console.WriteLine(buffer);
    }
}

internal class EditorView
{
    private readonly Buffer _buffer;
    private readonly FileHandler _fileHandler;
    private readonly ConsoleHandler _consoleHandler;

    public EditorView(Buffer buffer, ConsoleHandler ch, FileHandler fh)
    {
        _consoleHandler = ch;
        _buffer = buffer;
        _fileHandler = fh;
    }

    public void DisplayBuffer()
    {
        _consoleHandler.Print(_buffer.GetBuffer());
    }

    public void OnKeyPressed(ConsoleKeyInfo key)
    {
        key.Key switch
        {
            ConsoleKey.UpArrow => _consoleHandler.Print("up"),
        };
    }
}

internal class Buffer
{
    private int _size;
    private string[] _buffer;
    private Action<IEnumerable<string>> _onBufferChanged;

    public void Subscribe(Action<IEnumerable<string>> action)
    {
        _onBufferChanged += action;
    }

    public string GetBuffer()
    {
        return string.Join('\n', _buffer);
    }

    public void Unsubscribe(Action<IEnumerable<string>> action)
    {
        if (_onBufferChanged is not null)
        {
            _onBufferChanged -= action;
        }
    }

    public void WriteToBuffer(int l, int col, ReadOnlySpan<string> txt)
    {
        // ref string line = ref _buffer[l];
        // line.Insert(col, txt.ToString());
        // _onBufferChanged?.Invoke(_buffer);
        throw new NotImplementedException();
    }

    public void RemoveFromBuffer(int l, int col, int count)
    {
        throw new NotImplementedException();
    }
}

internal class FileHandler
{
    private Stream _fs;
    public FileHandler(string path)
    {
        _fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    public string[] StreamLines(int start, int lines)
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