﻿
namespace Tasty.Spreadsheets
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface definition for spreadsheet data sets.
    /// </summary>
    public interface ISpreadsheetDataSet
    {
        /// <summary>
        /// Gets the set's table collection.
        /// </summary>
        IList<ISpreadsheetDataTable> Tables { get; }
    }
}