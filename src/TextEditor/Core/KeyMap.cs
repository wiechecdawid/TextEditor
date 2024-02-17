using System;
using System.Collections.Generic;

namespace TextEditor.Core
{
    public class KeyMap
    {
        private Dictionary<ConsoleKey, Action<Buffer, Cursor>> keyActions;

        public KeyMap()
        {
            keyActions = new Dictionary<ConsoleKey, Action<Buffer, (int top, int left)>>
            {
                // Character keys
                { ConsoleKey.A, (buffer, (int top, int left)) => buffer.WriteToBuffer(new string(['A']), top, left) },
                { ConsoleKey.B, (buffer, cursor) => buffer.AddKey('B', cursor) },
                // Add more character keys here

                // Backspace
                { ConsoleKey.Backspace, (buffer, cursor) => buffer.RemoveFromBuffer(cursor) },

                // Arrow keys
                { ConsoleKey.LeftArrow, (buffer, cursor) => cursor.MoveLeft() },
                { ConsoleKey.RightArrow, (buffer, cursor) => cursor.MoveRight() },
                { ConsoleKey.UpArrow, (buffer, cursor) => cursor.MoveUp() },
                { ConsoleKey.DownArrow, (buffer, cursor) => cursor.MoveDown() }
            };
        }

        public void InvokeKey(ConsoleKey key, Buffer buffer, Cursor cursor)
        {
            if (keyActions.ContainsKey(key))
            {
                keyActions[key].Invoke(buffer, cursor);
            }
        }
    }
}
