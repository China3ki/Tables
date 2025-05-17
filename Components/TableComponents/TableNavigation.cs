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
        public int TableRowPosition { get; set; } = 0;
        /// <summary>
        /// Cord Y of the table.
        /// </summary>
        public int TableColumnPosition { get; set; } = 0;
        /// <summary>
        /// Default position X of the table.
        /// </summary>
        public int DefaultTableWidth { get; set; } = 0;
        /// <summary>
        /// Default position Y of the table.
        /// </summary>
        public int DefaultTableHeight { get; set; } = 0;
        /// <summary>
        /// Sets default table values.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void SetDefaultTablePosition(int x, int y)
        {
            DefaultTableWidth = x;
            DefaultTableHeight = y;
        }
       /// <summary>
       /// Changes row or column position.
       /// </summary>
       /// <param name="key">Representing pressed key.</param>
        public void ChangePosition(ConsoleKey key)
        {
            if (TableStyle.TableOrientation == TableOrientation.Horizontal)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        TableRowPosition = TableRowPosition == 0 ? 0 : TableRowPosition -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        TableRowPosition = TableRowPosition == MaxRows - 1 ? MaxRows - 1 : TableRowPosition += 1;
                        break;
                }
            }
            else
            {
                switch(key)
                {
                    case ConsoleKey.RightArrow:
                        TableColumnPosition = TableColumnPosition == MaxColumns - 1 ? MaxColumns - 1 : TableColumnPosition += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        TableColumnPosition = TableColumnPosition == 0 ? 0 : TableColumnPosition -= 1;
                        break;
                }
            }

        }
    }
}
