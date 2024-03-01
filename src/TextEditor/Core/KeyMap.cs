using System;
using System.Collections.Generic;

namespace TextEditor.Core;

public static class KeyMap
{
    private static Dictionary<ConsoleKey, Action> _keyActions = new();

    public static void AddKeyAction(ConsoleKey key, Action action)
    {
        _keyActions.Add(key, action);
    }

    public static void AddKeyActionRange(Dictionary<ConsoleKey, Action> keyActions)
    {
        foreach (var keyAction in keyActions)
        {
            _keyActions.Add(keyAction.Key, keyAction.Value);
        }
    }
}
