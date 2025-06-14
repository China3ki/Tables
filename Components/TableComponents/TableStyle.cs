﻿using System;

namespace Tables.Components.TableComponents
{
    /// <summary>
    /// Represents style of the table.
    /// </summary>
     static public class TableStyle
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
        static public ConsoleColor HeaderFontColor { get; set; } = ConsoleColor.Yellow;
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
        static public int MaxSizeToDisplay { get; set; } = 10;
        static public void ResetTableStyle()
        {
            BorderStyle = Styles.Solid;
            BorderColor = ConsoleColor.White;
            HeaderFontColor = ConsoleColor.Yellow;
            HeaderBackgroundColor = ConsoleColor.Black;
            FontColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            SelectedFieldHeaderFontColor = ConsoleColor.Black;
            SelectedFieldHeaderBackgroundColor = ConsoleColor.White;
            MaxSizeToDisplay = 10;
        }
    }
}
