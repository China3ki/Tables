using System.Diagnostics;
using System.Text;
using Tables.Components;
using Tables.Components.TableComponents;
namespace Tables
{
    sealed public class Table 
    {
        private TableDraw _tableDraw = new();
        private TableNavigation _tableNavigation = new();
        private Sort _sort = new();
        /// <summary>
        /// Initializes new instance of <see cref="Table"/>. Class cannot be inherited.
        /// </summary>
        public Table() { }
        /// <summary>
        /// Initializes new instance of <see cref="Table"/>. with started headers. Class cannot be inherited.
        /// </summary>
        /// <param name="headers">Started headers.</param>
        public Table(params string[] headers)
        {
            _tableDraw.Headers = headers;
            _tableDraw.MaxColumns = headers.Length;
        }
        public void AddHeaders(params string[] headers)
        {
            bool rowLengthIsNotEqualHeaders = false;
            foreach (string[] row in _sort.TableData)
            {
                if (row.Length != headers.Length)
                {
                    rowLengthIsNotEqualHeaders = true; 
                    break;
                }
            }
            if(rowLengthIsNotEqualHeaders) throw new InvalidOperationException("The number of headers is longer than the number of fields"); // ?
            else
            {
                _tableDraw.Headers = headers;
            }
        }
        /// <summary>
        /// Adds a new row to the table.
        /// </summary>
        /// <param name="newRow">
        /// An array of string representing the data for the new row.
        /// The number of elements in the array must match the number of table header columns.
        /// </param>
        /// <exception cref="InvalidOperationException">Thrown when the number of elements in <parameref name ="newRow"/> is higher than number of table headers or existing fields.</exception>
        public void AddData(params string[] newRow)
        {
            bool newRowIsNotEqual = false;
            foreach(string[] row in _sort.TableData)
            {
                if(newRow.Length != row.Length)
                {
                    newRowIsNotEqual = true;
                    break;
                }
            }
            if (newRow.Length != _tableDraw.Headers.Length && _tableDraw.Headers.Length != 0) throw new InvalidOperationException("The number of fields is not equal the number of headers.");
            else if (newRowIsNotEqual) throw new InvalidOperationException("The number of new row fields is not equal the number of old fields.");
            else
            {
                _sort.TableData.Add(newRow);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitTable(int headeToColor = 0)
        {
            _tableNavigation.SetDefaultTablePosition();
            PrepareTable();
            _tableDraw.InitTable(_tableNavigation.DefaultTableWidth, _tableNavigation.ConsoleTableHeight, headeToColor);
            ReadPressedKey();
        }
        /// <summary>
        /// Initializes a table with starting parameters.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void InitTable(int x, int y, int headeToColor = 0)
        {
            _tableNavigation.SetDefaultTablePosition(x, y);
            PrepareTable();
            _tableDraw.InitTable(x, y, headeToColor);
            ReadPressedKey();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetHorizontalTable()
        {
            List<string[]> newList = [];
            int columnLength = _tableDraw.TableDataToShow[0].Length;
            int rowLength = _tableDraw.TableDataToShow.Count;
            for (int x = 0; x < columnLength; x++)
            {
                string[] newHorizontalRow = new string[rowLength];
                for (int y = 0; y < rowLength; y++)
                {
                    newHorizontalRow[y] = _tableDraw.TableDataToShow[y][x];
                }
                newList.Add(newHorizontalRow);
            }
            _tableDraw.TableDataToShow = newList;
        }
        private void ReadPressedKey()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                HandleSorting(key);
            } while (key != ConsoleKey.Enter);
        }
        private void HandleControl(ConsoleKey key)
        {
            if(TableStyle.TableOrientation == TableOrientation.Vertical)
            {

            } else
            {

            }
        }
        private void ChangePositionOfTable(ConsoleKey key)
        {
            int oldHeight = _tableNavigation.ChangeHeight(key);
            int oldPosition = _tableNavigation.ChangePosition(key);
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
            {
                if(TableStyle.TableOrientation == TableOrientation.Horizontal)
                {
                    _tableDraw.ChangeColorOfSelectedHeader(_tableNavigation.DefaultTableWidth, _tableNavigation.TableRowPosition, oldPosition, 0, 0, _tableNavigation.ConsoleTableHeight, oldHeight);
                } 
            }
            else if (key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow)
            {
                if(TableStyle.TableOrientation == TableOrientation.Vertical)
                {
            
                     _tableDraw.ChangeColorOfSelectedHeader(_tableNavigation.DefaultTableWidth, 0, 0, _tableNavigation.TableColumnPosition, oldPosition, _tableNavigation.ConsoleTableHeight, _tableNavigation.ConsoleTableHeight);
                }
                
            }
        }
        private void HandleSorting(ConsoleKey key)
        {
            int columnPos = _tableNavigation.TableColumnPosition;
            int rowPos = _tableNavigation.TableRowPosition;
            int maxColumns = _tableNavigation.MaxColumns;
            int maxRows = _tableNavigation.MaxRows;
            SortType nextSortType = _sort.NextSortType;
            if ((columnPos == 0 && key == ConsoleKey.LeftArrow && nextSortType == SortType.Descending) || (columnPos == maxColumns - 1 && key == ConsoleKey.RightArrow && nextSortType == SortType.OrderBy)) return;
            if ((rowPos == 0 && key == ConsoleKey.UpArrow && nextSortType == SortType.Descending) || (rowPos == maxRows - 1 && key == ConsoleKey.DownArrow && nextSortType == SortType.OrderBy)) return;
            if ((key == ConsoleKey.LeftArrow && nextSortType == SortType.Descending) || (key == ConsoleKey.RightArrow && nextSortType == SortType.OrderBy)) ChangePositionOfTable(key);

            if (TableStyle.TableOrientation == TableOrientation.Vertical)
            {
                _sort.ToggleSort(_tableNavigation.TableColumnPosition); // Add Horizontal :L  
                InitTable(_tableNavigation.DefaultTableWidth, _tableNavigation.DefaultTableHeight, _tableNavigation.TableColumnPosition);
            }
               
        }
        private void PrepareTable()
        {
            _tableDraw.TableDataToShow.Clear();
            if (_tableDraw.Headers.Length != 0) _tableDraw.TableDataToShow.Add(_tableDraw.Headers);
            _tableDraw.TableDataToShow.AddRange(_sort.GetTableData(TableStyle.MaxSizeToDisplay));
            if (TableStyle.TableOrientation == TableOrientation.Horizontal) SetHorizontalTable();
            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            if (_tableDraw.TableDataToShow.Count == 0) throw new InvalidOperationException("Table should not be empty!");
            _tableDraw.MaxColumns = _tableDraw.TableDataToShow[0].Length;
            _tableNavigation.MaxColumns = _tableDraw.MaxColumns; 
            _tableNavigation.MaxRows = _tableDraw.TableDataToShow.Count;

        }
    }
}
