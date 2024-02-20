namespace TextEditor.Core;

internal record struct Cursor(int Top, int Left);

internal class ConsoleHandler
{
    public event EventHandler<ConsoleResizedEventArgs>? OnConsoleResized;
    private readonly CancellationTokenSource _cts;
    private readonly KeyMap _map;
    private Cursor _cursor;

    public ConsoleHandler(CancellationTokenSource cts)
    {
        _cursor = new Cursor(Console.WindowHeight, Console.WindowWidth);
        _cts = cts;
        _map = new KeyMap();
    }

    public (int, int) GetSizes()
    {
        return (_cursor.Top, _cursor.Left);
    }

    public void UpdateSizes()
    {
        if (_cursor.Top != Console.WindowHeight || _cursor.Left != Console.WindowWidth)
        {
            _cursor.Top = Console.WindowHeight;
            _cursor.Left = Console.WindowWidth;
            OnConsoleResized?.Invoke(this, new ConsoleResizedEventArgs(_cursor.Top, _cursor.Left));
        }
    }

    public void ChangeCursorPosition(int top, int left)
    {
        Console.SetCursorPosition(left, top);
        _cursor.Top = top;
        _cursor.Left = left;
    }

    public void ReadKey()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                _cts.Cancel();
            }
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


internal class CursorMover(ConsoleHandler Handler)
{
    public void MoveRight()
    {
        var (top, left) = Handler.GetSizes();
        if (left < Console.WindowWidth - 1)
        {
            Handler.ChangeCursorPosition(top, left + 1);
        }
        else
        {
            Handler.ChangeCursorPosition(top + 1, 0);
        }
    }

    public void MoveLeft()
    {
        var (top, left) = Handler.GetSizes();
        if (left > 0)
        {
            Handler.ChangeCursorPosition(top, left - 1);
        }
        else
        {
            Handler.ChangeCursorPosition(top - 1, Console.WindowWidth - 1);
        }
    }

    public void MoveUp()
    {
        var (top, left) = Handler.GetSizes();
        if (top > 0)
        {
            Handler.ChangeCursorPosition(top - 1, left);
        }
    }

    public void MoveDown()
    {
        var (top, left) = Handler.GetSizes();
        if (top < Console.WindowHeight - 1)
        {
            Handler.ChangeCursorPosition(top + 1, left);
        }
    }
}