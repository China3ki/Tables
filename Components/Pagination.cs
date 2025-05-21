using Tables.Components.TableComponents;

namespace Tables.Components
{
    internal class Pagination
    {
        public int CurrentPage { get; set; } = 0;
        public int LastPage { get; set; } = 0;
        public int MaxTableSize { get; set; } = TableStyle.MaxSizeToDisplay;

        /// <summary>
        /// Changes the current page of the table based on the provided console key input.
        /// Navigates between pages using arrow keys, depending on the table orientation:
        /// vertical tables use Up and Down arrows, horizontal tables use Left and Right arrows.
        /// </summary>
        /// <param name="key">The <see cref="ConsoleKey"/> input used to navigate between pages.</param>
        public void ChangeCurrentPage(ConsoleKey key)
        {
            TableOrientation tableOrientation = TableStyle.TableOrientation;
            switch(tableOrientation)
            {
                case TableOrientation.Vertical:
                    if (key == ConsoleKey.UpArrow && CurrentPage != 0) CurrentPage--;
                    else if (key  == ConsoleKey.DownArrow && CurrentPage != LastPage) CurrentPage++;
                    break;
                case TableOrientation.Horizontal:
                    if (key == ConsoleKey.LeftArrow && CurrentPage != 0) CurrentPage--;
                    else if (key == ConsoleKey.RightArrow && CurrentPage != LastPage) CurrentPage++;
                    break;
            }
        }
    }
}
