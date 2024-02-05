namespace TextEditor.Core;

internal class ConsoleResizedEventArgs : EventArgs
{
    public int Height { get; }
    public int Width { get; }

    public ConsoleResizedEventArgs(int h, int w)
    {
        Height = h;
        Width = w;
    }
}
