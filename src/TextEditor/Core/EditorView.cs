using System;
using MyBuffer = TextEditor.Core.Buffer;

namespace TextEditor.Core;

public class EditorView
{
    private readonly MyBuffer _buffer;
    private int startingLine = 0;

    public EditorView(MyBuffer buffer)
    {
        _buffer = buffer;
        _buffer.BufferChanged += OnBufferChanged;
    }

    public void DisplayBuffer()
    {
        Console.Clear();
        Console.Out.Write("\u001b[3J");
        if (Console.WindowHeight > _buffer.Lines.Count) startingLine = 0;
        // if the buffer is smaller than the window, we can display the entire buffer. Otherwise, start at the starting line
        for (int i = startingLine; i < Console.WindowHeight && i < _buffer.Lines.Count; i++)
        {
            Console.WriteLine($"{(i+1).ToString("D4")}\t{_buffer.Lines[i].PadRight(Console.WindowWidth - 1)}");
        }
    }

    public void OnBufferChanged(object? sender, EventArgs e)
    {
        DisplayBuffer();
    }
}