using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tables.Components.TableComponents
{
    internal class TableNavigation
    {
        /// <summary>
        /// Max size of columns.
        /// </summary>
        public int MaxColumns { get; set; } = 0;
        /// <summary>
        /// Max size of rows.
        /// </summary>
        public int MaxRows { get; set; } = 0;
        /// <summary>
        /// Cord X of the table.
        /// </summary>
        public int TableRowPos { get; set; } = 1;
        /// <summary>
        /// Cord Y of the table.
        /// </summary>
        public int TableColumnPos { get; set; } = 0;
        /// <summary>
        /// Position X of the table.
        /// </summary>
        public int ConsolePosX { get; set; } = 0;
        /// <summary>
        /// Position Y of the table.
        /// </summary>
        public int ConsolePosY { get; set; } = 0;
        /// <summary>
        /// Default position X of the table.
        /// </summary>
        private int _defaultConsolePosX = 0;
        /// <summary>
        /// Default position Y of the table.
        /// </summary>
        private int _defaultConsolePosY = 0;
        private ConsoleKey _lastAction;
        public void SetDefaultTablePosition(int x, int y)
        {
            _defaultConsolePosX = x;
            _defaultConsolePosY = y + 2;
            ConsolePosX = x;
            ConsolePosY = y;
        }
        public void SetDefaultTablePosition()
        {
            ConsolePosX = _defaultConsolePosX;
            ConsolePosY = _defaultConsolePosY;
            TableRowPos = 1;
            TableColumnPos = 0;
        }
        public void ChangePosition(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    ConsolePosX = TableRowPos == MaxRows ? ConsolePosX : ConsolePosX
                    TableRowPos = TableRowPos == MaxRows ? TableRowPos : TableRowPos += 1; // Check
                    break;
                case ConsoleKey.DownArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.LeftArrow:
                    break;
            }
        }
    }
}
