namespace Tables.Components
{
    internal class Sort
    {
        public List<string[]> TableData { get; set; } = [];
        public SortType NextSortType = SortType.Descending;
        
        /// <summary>
        /// Toggle a sort.
        /// </summary>
        /// <param name="index">Index of table.</param>
        /// <exception cref="InvalidOperationException">Thrown when index does not exist.</exception>
        public void ToggleSort(int index)
        {
            if (index > TableData[0].Length) throw new InvalidOperationException("Invalid column index for sorting.");
            if (NextSortType == SortType.OrderBy)
            {
                NextSortType = SortType.Descending;
                TableData = TableData.OrderBy(column => column[index]).ToList();
            }
            else
            {
                NextSortType = SortType.OrderBy;
                TableData = TableData.OrderByDescending(column => column[index]).ToList();
            }
        }

        /// <summary>
        /// Retrieves a subset of table data for a given page, limited to a specified maximum number of entries.
        /// Adjusts the maximum size if it exceeds the total number of available entries.
        /// </summary>
        /// <param name="page">The page number to retrieve (zero-based).</param>
        /// <param name="maxSize">The maximum number of entries to retrieve per page.</param>
        /// <returns>A list of string arrays representing the table data for the specified page.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="maxSize"/> is zero or less.</exception>
        public List<string[]> GetTableData(int page, int maxSize)
        {
            if (maxSize == 0) throw new InvalidOperationException("maxSize has to be higher than 0.");
            if (maxSize > TableData.Count && TableData.Count < 10) maxSize = TableData.Count;
            else if (maxSize > TableData.Count && TableData.Count >= 10) maxSize = 10;
             List<string[]> tableData = [];
            int start = page * maxSize;
            int end = Math.Min(start + maxSize, TableData.Count);
            for (int i = start; i < end; i++)
            {
                tableData.Add(TableData[i]);
            }
            return tableData;
        }
    }
}
