using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SqlColumn = UtilityLibrary.SqlColumn;

namespace DuplicateRecordsProject.Extensions
{
    public static class CheckedListBoxExtensions
    {
        /// <summary>
        /// Returns all checked items as ColumnDetails list.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>List of ColumnDetails</returns>
        /// <remarks>recommended to check the count, if 0 no items checked.</remarks>
        public static List<SqlColumn> CheckedIColumnDetailsList(this CheckedListBox sender)
        {
            var result = from column in sender.Items.OfType<SqlColumn>()
                .Where((item, index) => sender.GetItemChecked(index)) select column;

            return result.ToList();
        }
        /// <summary>
        /// Find item in CheckedListBox where the items are of type SqlColumn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pText"></param>
        /// <param name="pChecked"></param>
       public static void FindItemAndSetChecked(this CheckedListBox sender, string pText, bool pChecked = true)
        {
            var result = sender.Items.Cast<SqlColumn>()
                .Select((item, index) => new {Column = item, Index = index})
                .FirstOrDefault(@this => string.Equals(@this.Column.ColumnName, pText, StringComparison.OrdinalIgnoreCase));

            if (result != null)
            {
                sender.SetItemChecked(result.Index, pChecked);
            }
        }
    }
}
