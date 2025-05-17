using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tables.Components.TableComponents;

namespace Tables.Components
{
    internal class Sort
    {
        public List<string[]> TableData { get; set; } = [];
        public SortType NextSortType = SortType.Descending;
        
        public void ToggleSort(int x)
        {
            if (x > TableData[0].Length) throw new InvalidOperationException("Invalid column index for sorting.");
            if (NextSortType == SortType.OrderBy)
            {
                NextSortType = SortType.Descending;
                TableData = TableData.OrderBy(column => column[x]).ToList();
            }
            else
            {
                NextSortType = SortType.OrderBy;
                TableData = TableData.OrderByDescending(column => column[x]).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public List<string[]> GetTableData(int maxSize)
        {
            if (maxSize == 0) throw new InvalidOperationException("maxSize has to be higher than 0.");
            if (maxSize > TableData.Count) maxSize = TableData.Count;
            List<string[]> tableData = [];
            for (int i = 0; i < maxSize; i++)
            {
                tableData.Add(TableData[i]);
            }
            return tableData;
        }
    }
}
