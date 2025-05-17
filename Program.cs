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
            Table smartphoneTable = new Table(" Model ", " Marka ", " Cena ");

            smartphoneTable.AddData("iPhone 15 Pro", "Apple", "5999 PLN");
            smartphoneTable.AddData("Galaxy S24 Ultra", "Samsung", "5699 PLN");
            smartphoneTable.AddData("Xiaomi 14 Ultra", "Xiaomi", "4399 PLN");
            smartphoneTable.AddData("Nothing Phone (2)", "Nothing", "2999 PLN");
            TableStyle.HeaderFontColor = ConsoleColor.Yellow;
            //TableStyle.MaxSizeToDisplay = 4;
            //TableStyle.TableOrientation = TableOrientation.Horizontal;
            smartphoneTable.InitTable(0,0);
        }
    }
}
