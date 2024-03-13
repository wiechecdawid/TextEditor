using System;
using System.Collections.Generic;

namespace TextEditor.Core;

public class CursorHandler : IKeyHandler
{
    private Dictionary<ConsoleKey, (int left, int top)> _map = new()
    {
        [ConsoleKey.UpArrow] = (0, -1),
        [ConsoleKey.DownArrow] = (0, 1),
        [ConsoleKey.LeftArrow] = (-1, 0),
        [ConsoleKey.RightArrow] = (1, 0 )
    };

    public int CursorLeft { get; private set; } = 0;
    public int CursorTop { get; private set; } = 0;

    public void Handle(object? sender, KeyPressedEventArgs e)
    {
        var controller = sender as EditorController;
        controller?.DisplayBuffer();
        if (_map.TryGetValue(e.Key.Key, out var result))
        {
            Move(result.left, result.top);
        }
    }

    public void Move(int left, int top)
    {
        if (CursorLeft + left < 0 && CursorTop + top < 0)
        {
        }
        else if (CursorLeft + left < 0 && CursorTop + top >= 0)
        {
            CursorTop -= 1;
            CursorLeft = Console.BufferWidth - left;
        }
        else if (CursorLeft + left >= 0 && CursorTop + top < 0)
        {
        }
        else if (CursorLeft + left > Console.BufferWidth && CursorTop + top < Console.BufferHeight)
        {
            CursorTop += 1;
            CursorLeft += left - Console.BufferWidth + 8;
        }
        else if (CursorLeft + left > Console.BufferWidth && CursorTop + top > Console.BufferHeight)
        {
        }
        else
        {
            CursorTop += top;
            CursorLeft += left;
        }
    }

    public void SetCursor()
    {
        Console.CursorLeft = CursorLeft;
        Console.CursorTop = CursorTop;
    }
}