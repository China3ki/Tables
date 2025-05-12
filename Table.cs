using System.Text;
using Tables.Components;
using Tables.Components.SortComponents;
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
        public void InitTable()
        {
            PrepareDataToTable();
            _tableDraw.InitTable(_tableNavigation.ConsolePosX, _tableNavigation.ConsolePosY);
        }
        /// <summary>
        /// Initializes a table with starting parameters.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void InitTable(int x, int y)
        {
            _tableNavigation.SetDefaultTablePosition(x, y);
            PrepareDataToTable();
            _tableDraw.InitTable(x, y);
        }
        private void PrepareDataToTable()
        {
            if (_tableDraw.Headers.Length != 0) _tableDraw.TableDataToShow.Add(_tableDraw.Headers);
            _tableDraw.TableDataToShow.AddRange(_sort.TableData);
            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            if (_tableDraw.TableDataToShow.Count == 0) throw new InvalidOperationException("Table should not empty!");
            switch(TableStyle.TableOrientation)
            { 
                case TableOrientation.Horizontal:
                    _tableDraw.MaxColumns = _tableDraw.TableDataToShow.Count; // Reverse number of column with number of rows;
                    break;
                case TableOrientation.Vertical:
                    _tableDraw.MaxColumns = _tableDraw.TableDataToShow[0].Length;
                    break;
            }
            _tableNavigation.MaxColumns = _tableDraw.MaxColumns; // MaxColumns should be default according to the TableDataToShow.
            _tableNavigation.MaxRows = _tableDraw.TableDataToShow[0].Length;
        }
    }
}
