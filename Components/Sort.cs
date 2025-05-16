using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tables.Components.TableComponents;

namespace Tables.Components
{
    internal class Sort
    {
        public List<string[]> TableData { get; set; } = [];
        private SortType _nextSortedType = SortType.OrderBy;
        
        public List<string[]> ToggleSort(int x)
        {
            if (x > TableData[0].Length) throw new InvalidOperationException("Invalid column index for sorting.");
            if (_nextSortedType == SortType.OrderBy)
            {
                _nextSortedType = SortType.Descending;
                return TableData.OrderBy(column => column[x]).ToList();
            }
            else
            {
                _nextSortedType = SortType.OrderBy;
                return TableData.OrderByDescending(column => column[x]).ToList();
            }
        }

    }
}
