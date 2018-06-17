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
            var result = from T in sender.Items.OfType<SqlColumn>().Where((item, index) => sender.GetItemChecked(index)) select T;

            return result.ToList();
        }
        /// <summary>
        /// Find item in CheckedListBox where the items are of type SqlColumn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Text"></param>
        /// <param name="Checked"></param>
        /// <remarks>
        /// When coding this it started as LINQ many years ago from another code sample I did
        /// and noticed it was sluggish. Refactored to Lambda, acceptable. Next changed the string
        /// comparision to 
        /// 
        /// FirstOrDefault(@this => String.Equals(@this.Column.ColumnName, Text, StringComparison.CurrentCultureIgnoreCase));
        /// 
        /// Better code but still slow. Did == in the FirstOrDefault, nothing to write home about but faster than the following
        /// 
        /// .FirstOrDefault(@this => string.Equals(@this.Column.ColumnName, Text, StringComparison.OrdinalIgnoreCase));
        /// 
        /// So what has been presented is acceptable.
        /// 
        /// Lastly, so no reason to strong type "result' variable.
        /// </remarks>
        public static void FindItemAndSetChecked(this CheckedListBox sender, string Text, bool Checked = true)
        {
            var result = sender.Items.Cast<SqlColumn>()
                .Select((item, index) => new {Column = item, Index = index})
                .FirstOrDefault(@this => string.Equals(@this.Column.ColumnName, Text, StringComparison.OrdinalIgnoreCase));

            if (result != null)
            {
                sender.SetItemChecked(result.Index, Checked);
            }
        }
    }
}
