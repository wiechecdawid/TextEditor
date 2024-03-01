using System;
using System.Collections.Generic;
using System.IO;

namespace TextEditor.Core;

public class Buffer
{
    public event EventHandler BufferChanged;
    private List<string> _lines = new();

    public Buffer()
    {
    }

    public Buffer(string filePath)
    {
        LoadFromFile(filePath);
    }

    public List<string> Lines => _lines;

    public void LoadFromFile(string filePath)
    {
        try
        {
            _lines = new List<string>(File.ReadAllLines(filePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load file: {ex.Message}");
        }
    }

    public void SaveToFile(string filePath)
    {
        try
        {
            File.WriteAllLines(filePath, _lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save file: {ex.Message}");
        }
    }

    public void InsertLine(int index, string line)
    {
        _lines.Insert(index, line);
    }

    public void RemoveLine(int index)
    {
        _lines.RemoveAt(index);
    }

    public void RemoveFrom(int lineIndex, int columnIndex, int count)
    {
        if (lineIndex >= 0 && lineIndex < _lines.Count)
        {
            string line = _lines[lineIndex];
            if (columnIndex >= 0 && columnIndex < line.Length)
            {
                int endIndex = columnIndex + count;
                if (endIndex > line.Length)
                {
                    endIndex = line.Length;
                }
                _lines[lineIndex] = line.Remove(columnIndex, endIndex - columnIndex);
            }
            else if (columnIndex >= line.Length && lineIndex < _lines.Count - 1)
            {
                string nextLine = _lines[lineIndex + 1];
                int remainingCount = count - (line.Length - columnIndex);
                if (remainingCount < nextLine.Length)
                {
                    _lines[lineIndex] = line + nextLine.Substring(remainingCount);
                    _lines.RemoveAt(lineIndex + 1);
                }
                else
                {
                    _lines[lineIndex] = line + nextLine;
                    _lines.RemoveAt(lineIndex + 1);
                    RemoveFrom(lineIndex, line.Length, remainingCount - nextLine.Length);
                }
            }
        }

        BufferChanged?.Invoke(this, EventArgs.Empty);
    }

    public void WriteTo(int lineIndex, int columnIndex, string text)
    {
        if (_lines is null)
        {
            _lines = [.. text.Split(Environment.NewLine)];
            return;
        }

        if (lineIndex >= 0 && lineIndex < _lines.Count)
        {
            string line = _lines[lineIndex];
            if (columnIndex >= 0 && columnIndex <= line.Length)
            {
                _lines[lineIndex] = line.Insert(columnIndex, text);
            }
            else if (columnIndex > line.Length)
            {
                _lines[lineIndex] = line + text;
            }
        }

        RefactorLines();
        BufferChanged?.Invoke(this, EventArgs.Empty);
    }

    private void RefactorLines()
    {
        _lines = [.. string.Join(Environment.NewLine, _lines).Split(Environment.NewLine)];
    }
}
