using System;
using System.Collections.Generic;
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
            Table table = new Table("Aa", "Bb", "cc");
            table.AddData("a", "s", "a");
            TableStyle.BorderStyle = Styles.Dashed;
            table.InitTable(0,0);
        }
    }
}
