namespace TextEditor.Core;

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
        _consoleHandler.OnConsoleResized += OnConsoleResized;
    }

    public void OnConsoleResized(object? sender, ConsoleResizedEventArgs e)
    {
        _buffer.LoadBuffer(_fileHandler.StreamLines((0, e.Height), (0, e.Width)));
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
