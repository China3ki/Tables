namespace Tables.Components.TableComponents
{
    /// <summary>
    /// Represents style of the table.
    /// </summary>
     static class TableStyle
    {
        /// <summary>
        /// Gets or sets orientation of the table.
        /// </summary>
        static public TableOrientation TableOrientation { get; set; } = TableOrientation.Vertical;
        /// <summary>
        /// Gets or sets border style of the table.
        /// </summary>
        static public Styles BorderStyle { get; set; } = Styles.Solid;
        /// <summary>
        /// Gets or sets border color of the table.
        /// </summary>
        static public ConsoleColor BorderColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Gets or sets font color of the table header.
        /// </summary>
        static public ConsoleColor HeaderFontColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Gets or sets background color of the table header.
        /// </summary>
        static public ConsoleColor HeaderBackgroundColor { get; set; } = ConsoleColor.Black;
        /// <summary>
        /// Gets or sets font color of the table.
        /// </summary>
        static public ConsoleColor FontColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Gets or sets background Color of the table.
        /// </summary>
        static public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        /// <summary>
        /// Gets or sets font color of the table selected field.
        /// </summary>
        static public ConsoleColor SelectedFieldFontColor { get; set; } = ConsoleColor.Green;
        /// <summary>
        /// Gets or sets background color of the table selected field.
        /// </summary>
        static public ConsoleColor SelectedFieldBackgroundColor { get; set; } = ConsoleColor.Black;       
        /// <summary>
        /// Gets or sets font color of the table selected header field.
        /// </summary>
        static public ConsoleColor SelectedFieldHeaderFontColor { get; set; } = ConsoleColor.Black;
        /// <summary>
        /// Gets or sets background color fo the table selected header field.
        /// </summary>
        static public ConsoleColor SelectedFieldHeaderBackgroundColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Sets default values of the table.
        /// </summary>
        static public void ResetTableStyle()
        {
            BorderStyle = Styles.Solid;
            BorderColor = ConsoleColor.White;
            HeaderFontColor = ConsoleColor.White;
            HeaderBackgroundColor = ConsoleColor.Black;
            FontColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            SelectedFieldFontColor = ConsoleColor.Green;
            SelectedFieldBackgroundColor = ConsoleColor.Black;
        } 
    }
}
