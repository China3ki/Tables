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
            bool rowLengthIsLongerThanHeaders = false;
            foreach (string[] row in _sort.TableData)
            {
                if (row.Length > headers.Length)
                {
                    rowLengthIsLongerThanHeaders = true; 
                    break;
                }
            }
            if(rowLengthIsLongerThanHeaders) throw new InvalidOperationException("The number of headers is longer than the number of fields"); // ?
            else
            {
                _tableDraw.Headers = headers;
                _tableDraw.MaxColumns = headers.Length;
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
            bool newRowIsLongerThanOldRow = false;
            foreach(string[] row in _sort.TableData)
            {
                if(newRow.Length > row.Length)
                {
                    newRowIsLongerThanOldRow = true;
                    break;
                }
            }
            if (newRow.Length > _tableDraw.Headers.Length) throw new InvalidOperationException("The number of fields is longer than the number of headers.");
            else if (newRowIsLongerThanOldRow) throw new InvalidOperationException("The number of new row fields is longer than than the number of old fields.");
            else
            {
                _sort.TableData.Add(newRow);
                _tableDraw.MaxColumns = _tableDraw.MaxColumns > newRow.Length ? _tableDraw.MaxColumns : newRow.Length;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitTable()
        {

        }
        /// <summary>
        /// Initializes a table with starting parameters.
        /// </summary>
        /// <param name="x">An int representing the starting X coordinate.</param>
        /// <param name="y">An int representing the starting Y coordinate.</param>
        public void InitTable(int x, int y)
        {
            _tableNavigation.ConsolePosX = x;
            _tableNavigation.ConsolePosY = y;
            _tableDraw.TableDataToShow = _sort.TableData;
            _tableDraw.RenderBorder(x, y);
        }
    }
}
