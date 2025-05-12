using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Initialise table.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void InitTable(int x, int y)
        {
            RenderBorder(x, y);
            RenderData(x, y);
        }
        /// <summary>
        /// Calculates the maximum length of the field in each column of the table.
        /// </summary>
        /// <returns>An array containing the length of the longest string in each column.</returns>
        public int[] GetLongestFieldOfColumn()
        {
            int[] LongestFieldOfColumn = new int[MaxColumns];
            for (int x = 0; x < TableDataToShow.Count; x++)
            {
                for (int y = 0; y < TableDataToShow[0].Length; y++)
                {
                    int lengthOfField = TableDataToShow[x][y].Length;
                    if (TableStyle.TableOrientation == TableOrientation.Vertical) LongestFieldOfColumn[y] = LongestFieldOfColumn[y] < lengthOfField ? lengthOfField : LongestFieldOfColumn[y];
                    else LongestFieldOfColumn[x] = LongestFieldOfColumn[x] < lengthOfField ? lengthOfField : LongestFieldOfColumn[x];

                }
            }
            return LongestFieldOfColumn;
        }
        /// <summary>
        /// Renders the border of the table.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        private void RenderBorder(int x, int y)
        {
            char[] borderStyle = GetBorderStyleSet();
            int maxHeightOfTable = TableStyle.TableOrientation == TableOrientation.Vertical ? TableDataToShow.Count * 2 - 1 : TableDataToShow[0].Length * 2 - 1;
            List<string> tableBorderList = [];


            for(int i = 0; i < maxHeightOfTable; i++)
            {
                if (i == 0) tableBorderList.Add(PrintBorderLine(borderStyle[5], borderStyle[0], borderStyle[3], borderStyle[6]));
                if (i % 2 == 0) tableBorderList.Add(PrintBorderLine(borderStyle[1], ' ', borderStyle[1], borderStyle[1]));
                if (i % 2 != 0) tableBorderList.Add(PrintBorderLine(borderStyle[11], borderStyle[0], borderStyle[2], borderStyle[12]));
                if (i == maxHeightOfTable - 1) tableBorderList.Add(PrintBorderLine(borderStyle[8], borderStyle[0], borderStyle[10], borderStyle[7]));
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
        /// Render a data to the table.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        private void RenderData(int x, int y)
        {
            int height = y + 1;
            int numberOfAllRows = TableDataToShow.Count;

            for (int i = 0; i < numberOfAllRows; i++)
            {
                for (int j = 0; j < TableDataToShow[0].Length; j++)
                {
                    if (i == 0 && Headers.Length != 0)
                    {
                        Console.ForegroundColor = TableStyle.HeaderFontColor;
                        Console.BackgroundColor = TableStyle.HeaderBackgroundColor;
                    }
                    else
                    {
                        Console.ForegroundColor = TableStyle.FontColor;
                        Console.BackgroundColor = TableStyle.BackgroundColor;
                    }
                    if (i == 0 && j == 0)
                    {
                        Console.ForegroundColor = TableStyle.SelectedFieldHeaderFontColor;
                        Console.BackgroundColor = TableStyle.SelectedFieldHeaderBackgroundColor;
                        
                    } else if(i == 1 && j == 0)
                    {
                        Console.ForegroundColor = TableStyle.SelectedFieldFontColor;
                        Console.BackgroundColor = TableStyle.SelectedFieldBackgroundColor;
                    }
                    if (TableStyle.TableOrientation == TableOrientation.Horizontal)
                    {
                        Console.SetCursorPosition(GetDataPosition(x, i, j, TableDataToShow[i][j].Length), height);
                        height += 2;
                    }
                    else Console.SetCursorPosition(GetDataPosition(x, j, i, TableDataToShow[i][j].Length), height);
                    Console.Write(TableDataToShow[i][j]);
                    Console.ResetColor();
                }
                if (TableStyle.TableOrientation == TableOrientation.Horizontal) height = y + 1;
                else height += 2;
            }
        }
        private int GetDataPosition(int x, int tableDataFieldIndex, int tableDataRowIndex, int wordLength)
        {
            int[] LongestFieldOfColumn = GetLongestFieldOfColumn();
            int dataLength = wordLength;
            return x + LongestFieldOfColumn.Take(tableDataFieldIndex).Sum() + 1 + tableDataFieldIndex + (LongestFieldOfColumn[tableDataFieldIndex] - dataLength) / 2;
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
        /// Gets the char array of the border style.
        /// </summary>
        /// <returns>Char array</returns>
        /// <exception cref="InvalidOperationException">Thrown if style does not recognize</exception>
        private char[] GetBorderStyleSet()
        {
            switch(TableStyle.BorderStyle)
            {
                case Styles.Solid:
                    return ['═', '║', '╬', '╦', '╩', '╔', '╗', '╝', '╚', '╦', '╩', '╠', '╣'];
                case Styles.Dotted:
                    return ['.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'];
                case Styles.Dashed:
                    return ['-', '|', '-', '-', '-', '|', '|', '|', '|', '-', '-', '|', '|'];
                default:
                    throw new InvalidOperationException("Style does not exist.");

            }
        }
    }
}
