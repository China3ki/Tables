﻿using System.Text;

namespace Tables.Components.TableComponents
{
    internal class TableDraw
    {
        /// <summary>
        /// List of headers.
        /// </summary>
        public string[] Headers { get; set; } = [];
        /// <summary>
        /// Maximum size of columns.
        /// </summary>
        public int MaxColumns { get; set; }
        /// <summary>
        /// Data To Render.
        /// </summary>
        public List<string[]> TableDataToShow { get; set; } = [];
        /// <summary>
        /// First init of the table.
        /// </summary>
        private bool _firstInit = true;

        /// <summary>
        /// Initializes and renders the table at the specified console coordinates.
        /// Draws the table border and data content, with optional sorting functionality.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate for the table rendering.</param>
        /// <param name="y">An int representing the starting Y coordinate for the table rendering.</param>
        /// <param name="enableSorting">A boolean indicating whether sorting icons should be displayed in the table headers.</param>
        public void InitTable(int x, int y, bool enableSorting)
        {
            RenderBorder(x, y);
            RenderData(x, y, 0, enableSorting);
            _firstInit = false;
        }

        /// <summary>
        /// Updates and re-renders the table at the specified console coordinates.
        /// Redraws the table border and data, optionally highlighting a selected header and displaying sorting indicators.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate for the table rendering.</param>
        /// <param name="y">An int representing the starting Y coordinate for the table rendering.</param>
        /// <param name="headerToColor">The index of the header to be highlighted (colored differently) in the table.</param>
        /// <param name="sortType">The type of sorting indicator to display (ascending or descending).</param>
        /// <param name="enableSorting">A boolean indicating whether sorting icons should be displayed in the table headers. Defaults to <c>true</c>.</param>
        public void UpdateTable(int x, int y, int headerToColor, SortType sortType, bool enableSorting = true)
        {
            RenderBorder(x, y);
            RenderData(x, y, headerToColor, enableSorting, sortType);
        }

        /// <summary>
        /// Clears a table.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void ClearTable(int x, int y)
        {
            int maxWidth = GetLongestFieldOfColumn().Sum() + MaxColumns + 2;
            int maxHeight = TableDataToShow.Count * 2 + 1;
            for(int i = x; i < x + maxWidth; i++)
            {
                for(int j = y; j < y + maxHeight; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
        }

        /// <summary>
        /// Renders the border of the table.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        private void RenderBorder(int x, int y)
        {
            char[] borderStyle = GetBorderStyleSet();
            int maxHeightOfTable = TableDataToShow.Count * 2 - 1;
            List<string> tableBorderList = [];


            for(int i = 0; i < maxHeightOfTable; i++)
            {
                if (i == 0) tableBorderList.Add(PrintBorderLine(borderStyle[5], borderStyle[0], borderStyle[3], borderStyle[6]));
                if (i % 2 == 0) tableBorderList.Add(PrintBorderLine(borderStyle[1], ' ', borderStyle[1], borderStyle[1]));
                if (i % 2 != 0) tableBorderList.Add(PrintBorderLine(borderStyle[9], borderStyle[0], borderStyle[2], borderStyle[10]));
                if (i == maxHeightOfTable - 1) tableBorderList.Add(PrintBorderLine(borderStyle[8], borderStyle[0], borderStyle[4], borderStyle[7]));
            }
            int tableBorderListIndex = 0;
            for(int consoleY = y; consoleY < y + tableBorderList.Count; consoleY++)
            {
                Console.ForegroundColor = TableStyle.BorderColor;
                Console.SetCursorPosition(x, consoleY);
                Console.Write(tableBorderList[tableBorderListIndex]);
                tableBorderListIndex++;
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Renders a table of data in the console window with configurable styling, orientation, and optional sorting indicator.
        /// Colors are applied differently for headers, selected headers, and regular cells. 
        /// Sorting arrows are displayed next to a selected header if sorting is enabled.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate for the table rendering.</param>
        /// <param name="y">An int representing the starting Y coordinate for the table rendering.</param>
        /// <param name="headerToColor">The index of the header to be highlighted (colored differently) in the table.</param>
        /// <param name="enableSorting">A boolean indicating whether a sorting icon should be displayed next to the highlighted header.</param>
        /// <param name="sortType">The type of sorting indicator to display (ascending or descending). Defaults to <see cref="SortType.OrderBy"/>.</param>
        private void RenderData(int x, int y, int headerToColor, bool enableSorting, SortType sortType = SortType.OrderBy)
        {
            int height = y + 1;
            int numberOfAllRows = TableDataToShow.Count;
            int numberOfAllColumns = TableDataToShow[0].Length;
            int headersLength = Headers.Length;
            TableOrientation tableOrientation = TableStyle.TableOrientation;
            for (int i = 0; i < numberOfAllRows; i++)
            {
                for (int j = 0; j < numberOfAllColumns; j++)
                {
                    if ((i == 0 && headersLength != 0 && TableStyle.TableOrientation == TableOrientation.Vertical) || (j == 0 && headersLength != 0 && TableStyle.TableOrientation == TableOrientation.Horizontal))
                    {
                        Console.ForegroundColor = TableStyle.HeaderFontColor;
                        Console.BackgroundColor = TableStyle.HeaderBackgroundColor;
                    }
                    else
                    {
                        Console.ForegroundColor = TableStyle.FontColor;
                        Console.BackgroundColor = TableStyle.BackgroundColor;
                    }
                    if (((i == 0 && j == headerToColor && tableOrientation == TableOrientation.Vertical) || (i == headerToColor && j == 0 && tableOrientation == TableOrientation.Horizontal)) && headersLength != 0)
                    {
                        Console.ForegroundColor = TableStyle.SelectedFieldHeaderFontColor;
                        Console.BackgroundColor = TableStyle.SelectedFieldHeaderBackgroundColor;
                        Console.SetCursorPosition(GetDataPosition(x, j, TableDataToShow[i][j].Length), height);
                        Console.Write(TableDataToShow[i][j]);
                        Console.ResetColor();
                        Console.ForegroundColor = TableStyle.HeaderFontColor;
                        Console.BackgroundColor = TableStyle.HeaderBackgroundColor;
                        if (enableSorting)
                        {
                            if (sortType == SortType.OrderBy && !_firstInit) Console.Write('\u25BC');
                            else if (sortType == SortType.Descending && _firstInit) Console.Write('\u25B2');
                            else Console.Write('\u25B2');
                            Console.ResetColor();
                            continue;
                        }
                    } 
                    Console.SetCursorPosition(GetDataPosition(x, j, TableDataToShow[i][j].Length), height);
                    Console.Write(TableDataToShow[i][j]);
                    Console.ResetColor();
                }
                height += 2;
            }
        }

        /// <summary>
        /// Creates a single border line for a table based on the specified characters.
        /// </summary>
        /// <param name="leftChar">Character placed at the start of the line.</param>
        /// <param name="defaultChar">Character used to fill each column's width.</param>
        /// <param name="midChar">Character placed between columns.</param>
        /// <param name="rightChar">Character placed at the end of the line.</param>
        /// <returns>A formatted string representing a table border line.</returns>
        private string PrintBorderLine(char leftChar, char defaultChar, char midChar, char rightChar)
        {
            int[] LongestFieldOfColumn = GetLongestFieldOfColumn();
            StringBuilder rowLine = new();

            rowLine.Append(leftChar);
            for(int i = 0; i < MaxColumns; i++)
            {
                for (int j = 0; j < LongestFieldOfColumn[i]; j++) rowLine.Append(defaultChar);
                if (i != MaxColumns - 1) rowLine.Append(midChar);
            }
            rowLine.Append($"{rightChar}\n");
            return rowLine.ToString();
        }

        /// <summary>
        /// Calculates the starting position for data within a specific table column.
        /// It accounts for the total width of all preceding columns and centers the data
        /// within the target column based on its maximum field length.
        /// </summary>
        /// <param name="x">The initial offset from the start of the row.</param>
        /// <param name="tableDataFieldIndex">The index of the column for which the position is calculated.</param>
        /// <param name="wordLength">The length of the data to be placed in the column.</param>
        /// <returns>The position (offset) from the start of the row where the data should be placed.</returns>
        private int GetDataPosition(int x, int tableDataFieldIndex, int wordLength)
        {
            int[] LongestFieldOfColumn = GetLongestFieldOfColumn();
            int dataLength = wordLength;
            return x + LongestFieldOfColumn.Take(tableDataFieldIndex).Sum() + 1 + tableDataFieldIndex + (LongestFieldOfColumn[tableDataFieldIndex] - dataLength) / 2;
        }

        /// <summary>
        /// Calculates the maximum length of the field in each column of the table.
        /// </summary>
        /// <returns>An array containing the length of the longest string in each column.</returns>
        private int[] GetLongestFieldOfColumn()
        {
            int[] LongestFieldOfColumn = new int[MaxColumns];
            for (int x = 0; x < TableDataToShow.Count; x++)
            {
                for (int y = 0; y < TableDataToShow[0].Length; y++)
                {
                    int lengthOfField = TableDataToShow[x][y].Length + 2;
                    LongestFieldOfColumn[y] = LongestFieldOfColumn[y] < lengthOfField ? lengthOfField : LongestFieldOfColumn[y];
                }
            }
            return LongestFieldOfColumn;
        }

        /// <summary>
        /// Gets the char array of the border style.
        /// </summary>
        /// <returns>Char array</returns>
        /// <exception cref="InvalidOperationException">Thrown if style does not recognize</exception>
        private char[] GetBorderStyleSet()
        {
            switch(TableStyle.BorderStyle)
            {
                case Styles.Solid:
                    return ['═', '║', '╬', '╦', '╩', '╔', '╗', '╝', '╚', '╠', '╣'];
                case Styles.Dotted:
                    return ['.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'];
                case Styles.Dashed:
                    return ['-', '|', '-', '-', '-', '|', '|', '|', '|', '|', '|'];
                default:
                    throw new InvalidOperationException("Style does not exist.");
            }
        }
    }
}
