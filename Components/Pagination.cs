using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tables.Components.TableComponents;

namespace Tables.Components
{
    internal class Pagination
    {
        public int CurrentPage { get; set; } = 0;
        public int LastPage { get; set; } = 0;
        public int MaxTableSize { get; set; } = TableStyle.MaxSizeToDisplay;
    }
}
