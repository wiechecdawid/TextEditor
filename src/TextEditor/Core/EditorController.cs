using System;
using System.Threading;

namespace TextEditor.Core;

public class EditorController
{
    private EditorView _editorView;

    public EditorController(EditorView editorView)
    {
        _editorView = editorView;
    }

    public void HandleKeyPress(ConsoleKeyInfo keyInfo, CancellationToken stoppingToken)
    {
        
    }
}
