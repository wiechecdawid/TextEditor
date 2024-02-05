

 namespace TextEditor.Core;

internal class ConsoleHandler
{
    public (int, int) GetSizes()
    {
        return (Console.BufferHeight, Console.BufferWidth);
    }
    public void Print(string buffer)
    {
        Console.Clear();
        Console.Write(buffer);
    }
}

internal class EditorView : IDisposable
{
    private readonly Buffer _buffer;
    private readonly FileHandler _fileHandler;
    private readonly ConsoleHandler _consoleHandler;

    public EditorView(ConsoleHandler ch, FileHandler fh)
    {
        _consoleHandler = ch;
        _buffer = Buffer.GetInstance();
        _fileHandler = fh;
        var (height, width) = _consoleHandler.GetSizes();
        _buffer.LoadBuffer(_fileHandler.StreamLines((0, height), (0, width)));
    }

    public void DisplayBuffer()
    {
        _consoleHandler.Print(_buffer.GetBuffer());
    }

    public void Dispose()
    {
        _fileHandler.Dispose();
    }
}
