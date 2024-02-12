namespace TextEditor.Core;

internal class ConsoleHandler
{
    public event EventHandler<ConsoleResizedEventArgs>? OnConsoleResized;
    private int _height;
    private int _width;
    private readonly CancellationTokenSource _cts;

    public ConsoleHandler(CancellationTokenSource cts)
    {
        _height = Console.WindowHeight;
        _width = Console.WindowWidth;
        _cts = cts;
    }

    public (int, int) GetSizes()
    {
        return (_height, _width);
    }

    public void UpdateSizes()
    {
        if (_height != Console.WindowHeight || _width != Console.WindowWidth)
        {
            _height = Console.WindowHeight;
            _width = Console.WindowWidth;
            OnConsoleResized?.Invoke(this, new ConsoleResizedEventArgs(_height, _width));
        }
    }

    public void Print(string buffer)
    {
        Console.Clear();
        Console.Write(buffer);
    }

    public void Print(string[] buffer)
    {
        Console.Clear();
        Console.Write(string.Join('\n', buffer));
    }
}
