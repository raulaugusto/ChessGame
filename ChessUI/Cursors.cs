using ChessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChessUI
{
    public static class Cursors
    {

        public static readonly Cursor whiteCursor = LoadCursors("Assets/CursorW.cur");
        public static readonly Cursor blackCursor = LoadCursors("Assets/CursorB.cur");

        private static Cursor LoadCursors(string filePath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
