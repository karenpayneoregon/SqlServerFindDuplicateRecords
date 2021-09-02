using System.Data;
using System.Windows.Forms;

namespace DuplicateRecordsProject.Extensions
{
    public static class BindingSourceExtensions
    {
        /// <summary>
        /// Return DataRow for Current row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <returns>DataRow</returns>
        /// <remarks>Throws an exception if the DataSource is not a DataTable</remarks>
        public static string CurrentRow(this BindingSource sender, string column) => ((DataRowView)sender.Current).Row[column].ToString();

        /// <summary>
        /// Return DataSource as a DataTable
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        /// <remarks>Throws an exception if the DataSource is not a DataTable</remarks>
        public static DataTable DataTable(this BindingSource sender) => (DataTable)sender.DataSource;
    }
}
