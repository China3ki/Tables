using System.Diagnostics;
using Tables.Components;
using Tables.Components.TableComponents;
namespace Tables
{
    sealed public class Table 
    {
        private TableDraw _tableDraw = new();
        private TableNavigation _tableNavigation = new();
        private Sort _sort = new();
        private Pagination _pagination = new();
        private bool _initTable = false;
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
        public void InitTable(int headerToColor = 0)
        {
            PrepareTable();
            _tableDraw.InitTable(_tableNavigation.DefaultTableWidth, _tableNavigation.DefaultTableHeight, headerToColor);
            if (!_initTable) ReadPressedKey();  
        }
        /// <summary>
        /// Initializes a table with starting parameters.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void InitTable(int x, int y, int headerToColor = 0)
        {
            _tableNavigation.SetDefaultTablePosition(x, y);
            PrepareTable();
            _tableDraw.InitTable(x, y, headerToColor);
            if (!_initTable) ReadPressedKey();
        }
        /// <summary>
        /// Sets Table in Horizontal position.
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
        /// <summary>
        /// Reads pressed key.
        /// </summary>
        private void ReadPressedKey()
        {
            _initTable = true;
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                HandleSorting(key);
                HandlePagination(key);
            } while (key != ConsoleKey.Enter);
        }
        /// <summary>
        /// Handle a table pagination.
        /// </summary>
        /// <param name="key">Representing pressed key.</param>
        private void HandlePagination(ConsoleKey key)
        {
            TableOrientation tableOrientation = TableStyle.TableOrientation;
            int currentPage = _pagination.CurrentPage;
            int lastPage = _pagination.LastPage;
            if((key == ConsoleKey.UpArrow && tableOrientation == TableOrientation.Vertical) || (key == ConsoleKey.LeftArrow && tableOrientation == TableOrientation.Horizontal))
            {
                if(_pagination.CurrentPage != 0)
                {
                    _pagination.CurrentPage--;
                    InitTable(_tableNavigation.TableRowPosition);
                }
            }
            else if((key == ConsoleKey.DownArrow && tableOrientation == TableOrientation.Vertical) || (key == ConsoleKey.RightArrow && tableOrientation == TableOrientation.Horizontal))
            {
                if(_pagination.CurrentPage != lastPage)
                {
                    _pagination.CurrentPage++;
                    InitTable(_tableNavigation.TableColumnPosition);
                }               
            }
        }
        /// <summary>
        /// Handle a table sorting
        /// </summary>
        /// <param name="key">Representing pressed key.</param>
        private void HandleSorting(ConsoleKey key)
        {
            int columnPos = _tableNavigation.TableColumnPosition;
            int rowPos = _tableNavigation.TableRowPosition;
            int maxColumns = _tableNavigation.MaxColumns;
            int maxRows = _tableNavigation.MaxRows;
            TableOrientation tableOrientation = TableStyle.TableOrientation;
            SortType nextSortType = _sort.NextSortType;
            if ((columnPos == 0 && key == ConsoleKey.LeftArrow && nextSortType == SortType.Descending) || (columnPos == maxColumns - 1 && key == ConsoleKey.RightArrow && nextSortType == SortType.OrderBy)) return;
            if ((rowPos == 0 && key == ConsoleKey.UpArrow && nextSortType == SortType.Descending) || (rowPos == maxRows - 1 && key == ConsoleKey.DownArrow && nextSortType == SortType.OrderBy)) return;
            if ((key == ConsoleKey.LeftArrow && nextSortType == SortType.Descending) || (key == ConsoleKey.RightArrow && nextSortType == SortType.OrderBy)) 
                _tableNavigation.ChangePosition(key);
            if ((key == ConsoleKey.UpArrow && nextSortType == SortType.Descending) || (key == ConsoleKey.DownArrow && nextSortType == SortType.OrderBy)) 
                _tableNavigation.ChangePosition(key);

            if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow) && tableOrientation == TableOrientation.Vertical)
            {
                _sort.ToggleSort(_tableNavigation.TableColumnPosition);
                _pagination.CurrentPage = 0;
                InitTable(_tableNavigation.TableColumnPosition);
            } 
            else if((key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow) && tableOrientation == TableOrientation.Horizontal)
            {
                _sort.ToggleSort(_tableNavigation.TableRowPosition);
                _pagination.CurrentPage = 0;
                InitTable(_tableNavigation.TableRowPosition);
            }
               
        }
        /// <summary>
        /// Prepares data to table.
        /// </summary>
        private void PrepareTable()
        {
            _tableDraw.ClearTable(_tableNavigation.DefaultTableWidth, _tableNavigation.DefaultTableHeight);
            _tableDraw.TableDataToShow.Clear();
            if (_tableDraw.Headers.Length != 0) _tableDraw.TableDataToShow.Add(_tableDraw.Headers);
            _tableDraw.TableDataToShow.AddRange(_sort.GetTableData(_pagination.CurrentPage,TableStyle.MaxSizeToDisplay));
            if (TableStyle.TableOrientation == TableOrientation.Horizontal) SetHorizontalTable();
            SetDefaultValues();
        }
        /// <summary>
        /// Sets default values for table.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when rows in table is zero.</exception>
        private void SetDefaultValues()
        {
            if (_tableDraw.TableDataToShow.Count == 0) throw new InvalidOperationException("Table should not be empty!");
            _tableDraw.MaxColumns = _tableDraw.TableDataToShow[0].Length;
            _tableNavigation.MaxColumns = _tableDraw.MaxColumns; 
            _tableNavigation.MaxRows = _tableDraw.TableDataToShow.Count;
            _pagination.MaxTableSize = TableStyle.MaxSizeToDisplay;
            _pagination.LastPage = _sort.TableData.Count % _pagination.MaxTableSize == 0 ? _sort.TableData.Count / _pagination.MaxTableSize - 1 : _sort.TableData.Count / _pagination.MaxTableSize;
        }
    }
}
