using System;

namespace TextEditor.Core;

public class EditorController
{
    public event EventHandler<KeyPressedEventArgs>? KeyPressed;
    private EditorView _editorView;

    public EditorController(
        EditorView editorView,
        params IKeyHandler[] keyHandlers
    )
    {
        _editorView = editorView;
        foreach (var keyHandler in keyHandlers)
        {
            KeyPressed += keyHandler.Handle;
        }
    }

    public void OnKeyPressed(ConsoleKeyInfo keyInfo)
    {
        var e = new KeyPressedEventArgs{Key = keyInfo};
        KeyPressed?.Invoke(this, e);
    }

    public void DisplayBuffer() => _editorView.DisplayBuffer();
}

public class KeyPressedEventArgs : EventArgs
{
    public ConsoleKeyInfo Key { get; set; }
}