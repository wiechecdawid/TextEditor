using System;
using System.Linq;
using MyBuffer = TextEditor.Core.Buffer;

namespace TextEditor.Core;

public class EditorView
{
    private readonly MyBuffer _buffer;
    private int startingLine = 0;
    private int startingColumn = 0;

    public EditorView(MyBuffer buffer)
    {
        _buffer = buffer;
        _buffer.BufferChanged += OnBufferChanged;
    }

    public (int height, int width) CursorPosition { get; set;} = (0, 0);

    public void DisplayBuffer()
    {
        Console.Clear();
        Console.Out.Write("\u001b[3J");
        startingLine = Console.BufferHeight > _buffer.Lines.Count ? 0 : startingLine;
        // if the buffer is smaller than the window, we can display the entire buffer. Otherwise, start at the starting line
        var text = string.Join("\n", _buffer.Lines.Skip(startingLine).Take(Console.BufferHeight).Select(x => 
        {
            return $"{(_buffer.Lines.IndexOf(x)+1)
                .ToString("D4")}\t{x.Skip(startingColumn).Take(Console.BufferWidth - 8 - startingColumn)}";
        }).ToList());
        
        Console.Write(text);
    }

    public void OnBufferChanged(object? sender, EventArgs e)
    {
        DisplayBuffer();
    }
}
