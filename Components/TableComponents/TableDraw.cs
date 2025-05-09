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
        public string[] Headers { get; set; } = [];
        public int MaxColumns { get; set; }
        public List<string[]> TableDataToShow = [];
        /// <summary>
        /// Renders the border of the table.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void RenderBorder(int x, int y)
        {
            bool isEvenNumber = y % 2 == 0 ? true : false; 
            char[] borderStyle = GetBorderStyleSet();
            int maxHeightOfTable = 1 + TableDataToShow.Count * 2;
            StringBuilder tableList = new();


            for(int i = 0; i < maxHeightOfTable; i++)
            {
                if (i == 0) tableList.Append(PrintBorderLine(borderStyle[5], borderStyle[0], borderStyle[3], borderStyle[6]));
                if (i % 2 == 0) tableList.Append(PrintBorderLine(borderStyle[1], ' ', borderStyle[1], borderStyle[1]));
                if (i % 2 != 0) tableList.Append(PrintBorderLine(borderStyle[11], borderStyle[0], borderStyle[2], borderStyle[12]));
                if (i == maxHeightOfTable - 1) tableList.Append(PrintBorderLine(borderStyle[8], borderStyle[0], borderStyle[10], borderStyle[7]));
            }
      
                Console.SetCursorPosition(x, y);
                Console.Write(tableList);
        }
        private string PrintBorderLine(char leftChar, char defaultChar, char midChar, char rightChar)
        {
            int[] longestFieldOfRow = GetLongestFieldOfRow();
            StringBuilder rowLine = new();

            rowLine.Append(leftChar);
            for(int i = 0; i < MaxColumns; i++)
            {
                for (int j = 0; j < longestFieldOfRow[i]; j++) rowLine.Append(defaultChar);
                if (i != MaxColumns - 1) rowLine.Append(midChar);
            }
            rowLine.Append($"{rightChar}\n");
            return rowLine.ToString();
        }
        private int[] GetLongestFieldOfRow()
        {
            int[] longestFieldOfRow = new int[MaxColumns];
            for(int x = 0; x < TableDataToShow.Count; x++)
            {
                for(int y = 0; y < TableDataToShow[x].Length; y++)
                {
                    int lengthOfField = TableDataToShow[x][y].Length;
                    longestFieldOfRow[y] = longestFieldOfRow[y] < lengthOfField ? lengthOfField : longestFieldOfRow[y];
                }
            }
            return longestFieldOfRow;
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
