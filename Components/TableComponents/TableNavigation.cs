using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int TableRowPosition { get; set; } = 1;
        /// <summary>
        /// Cord Y of the table.
        /// </summary>
        public int TableColumnPosition { get; set; } = 0;
        /// <summary>
        /// Position Y of the table.
        /// </summary>
        public int ConsoleTableHeight { get; set; } = 0;
        /// <summary>
        /// Default position X of the table.
        /// </summary>
        public int DefaultTableWidth { get; set; } = 0;
        /// <summary>
        /// Default position Y of the table.
        /// </summary>
        public int DefaultTableHeight { get; set; } = 0;
        private ConsoleKey _lastAction;
        public void SetDefaultTablePosition(int x, int y)
        {
            DefaultTableWidth = x;
            DefaultTableHeight = y;
            ConsoleTableHeight = y + 1;
        }
        public void SetDefaultTablePosition()
        {
            ConsoleTableHeight = DefaultTableHeight;
            TableRowPosition = 1;
            TableColumnPosition = 0;
        }
        public int ChangePosition(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:                   
                    TableRowPosition = TableRowPosition == 1 ? 1 : TableRowPosition -= 1; // Check
                    return TableRowPosition + 1;
                case ConsoleKey.DownArrow:
                    TableRowPosition = TableRowPosition == MaxRows ? TableRowPosition : TableRowPosition += 1;
                    return TableRowPosition - 1;
                case ConsoleKey.RightArrow:
                    TableColumnPosition = TableColumnPosition == MaxColumns ? MaxColumns : TableColumnPosition += 1;
                    return TableColumnPosition - 1;
                case ConsoleKey.LeftArrow:
                    TableColumnPosition = TableColumnPosition == 0 ? 0 : TableColumnPosition -= 1;
                    return TableColumnPosition + 1;
                default:
                    return 0; // Exception ?;
            }
        }
        public int ChangeHeight(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    ConsoleTableHeight = TableRowPosition == MaxRows ? ConsoleTableHeight : ConsoleTableHeight -= 2;
                    return ConsoleTableHeight + 2;
                case ConsoleKey.DownArrow:
                    ConsoleTableHeight = TableRowPosition == MaxRows ? ConsoleTableHeight : ConsoleTableHeight += 2;
                    return ConsoleTableHeight - 2;
                default:
                    return 0; // Exception ?;
            }
        }
    }
}
