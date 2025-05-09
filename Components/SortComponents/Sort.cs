using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tables.Components.SortComponents
{
    internal class Sort
    {
        public List<string[]> TableData { get; set; } = [];
        public int CurrentPage { get; set; } = 0;
    }
}
