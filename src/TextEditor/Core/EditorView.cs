namespace TextEditor.Core;

internal class EditorView
{
    private readonly FileHandler _fileHandler;
    private readonly ConsoleHandler _consoleHandler;
    private string _buffer;
}

internal class ConsoleHandler
{

}

internal class FileHandler
{
    private Stream _fs;
    public FileHandler(string path)
    {
        _fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }
}