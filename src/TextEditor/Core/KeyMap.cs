using System;
using System.Collections.Generic;

namespace TextEditor.Core
{
    public class KeyMap
    {
        private Dictionary<ConsoleKey, Action<Buffer, Cursor>> _specialKeyActions;

        public KeyMap()
        {
            _specialKeyActions = new Dictionary<ConsoleKey, Action<Buffer, Cursor>>
            {
                { ConsoleKey.LeftArrow, (_, cursor) => MoveLeft(cursor) },
                { ConsoleKey.RightArrow, (_, cursor) => cursor.MoveRight() },
                { ConsoleKey.UpArrow, (_, cursor) => cursor.MoveUp() },
                { ConsoleKey.DownArrow, (_, cursor) => cursor.MoveDown() },
                { ConsoleKey.Backspace, (buffer, cursor) => cursor.Backspace() },
                { ConsoleKey.Delete, (buffer, cursor) => cursor.Delete()
            };
        }

        
    }
}
