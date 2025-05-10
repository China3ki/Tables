using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tables.Components.TableComponents;

namespace Tables
{
    class Program
    {
        static void Main(params string[] args)
        {
            Table table = new Table("Aa", "Bab", "caaac11");
            table.AddData("asa", "s", "dd");
            table.AddData("asa", "s", "dd");
            table.AddData("asa", "s", "dd");
            table.AddData("asa", "s", "dd");
            table.AddData("asa", "s", "dd");
            table.AddData("asa", "s", "dd");
            table.InitTable(10,10);
        }
    }
}
