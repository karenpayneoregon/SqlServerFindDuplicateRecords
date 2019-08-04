using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuplicateRecordsProject;
using DuplicateRecordsProject.Extensions;
using SQL_Library;
using SupportLibrary;
using UtilityLibrary;

namespace DuplicateRecordsProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private List<string> _countryColumnList;
        /// <summary>
        /// Get databases for server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {

            var ops = new SqlDatabases();

            DatabaseNameListBox.DataSource = ops.DatabaseNames();

            if (ops.IsKarenMachine)
            {
                DatabaseNameListBox.SelectedIndex = DatabaseNameListBox.FindString("NorthWindDemo");
                DatabaseTableNamesListBox.SelectedIndex = DatabaseTableNamesListBox.FindString("Customer");
                var databaseTables = new SqlTables("NorthWindDemo");
                _countryColumnList = databaseTables.CountrySelected;
                
                foreach (var item in _countryColumnList)
                {
                    SelectedTableColumnCheckedListBox.FindItemAndSetChecked(item);
                }
            }

        }
        /// <summary>
        /// Get table names for selected database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var databaseTables = new SqlTables(DatabaseNameListBox.Text);
            DatabaseTableNamesListBox.DataSource = null;
            DatabaseTableNamesListBox.DataSource = databaseTables.TableNames();
        }
        /// <summary>
        /// Get all columns for selected table.
        /// If there is a primary key it's mark as one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var columnsForSelectedTable = new SqlColumns(DatabaseNameListBox.Text,DatabaseTableNamesListBox.Text);
            var results = columnsForSelectedTable.GetColumns();

            SelectedTableColumnCheckedListBox.Items.Clear();

            if (results.Count <= 0) return;

            OrderByComboBox.Items.Clear();
            OrderByComboBox.Items.Add("None");

            foreach (var column in results)
            {
                SelectedTableColumnCheckedListBox.Items.Add(column);
                if (column.IsIdentity) continue;
                OrderByComboBox.Items.Add(column.ColumnName);
            }

            OrderByComboBox.SelectedIndex = 0;

        }
        /// <summary>
        /// Get columns to perform duplicate check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// As coded columns should not be binary or similar type
        /// such as image, if so this will fail. Check types if unsure
        /// 
        /// SELECT COLUMN_NAME, DATA_TYPE
        ///     FROM INFORMATION_SCHEMA.COLUMNS
        ///     WHERE TABLE_NAME = 'SomeTable'
        /// 
        /// </remarks>
        private void cmdCheckForDuplicates_Click(object sender, EventArgs e)
        {
            if (SelectedTableColumnCheckedListBox.Items.Count == 0) return;

            var columns = SelectedTableColumnCheckedListBox.Items.OfType<SqlColumn>();
            var identityColumn = columns.FirstOrDefault(sqlColumn => sqlColumn.IsIdentity);

            var sqlColumns = SelectedTableColumnCheckedListBox.CheckedIColumnDetailsList();

            if (sqlColumns.Count >0)
            {
                if (identityColumn != null)
                {
                    var evaluateContains = sqlColumns.Contains(identityColumn);
                    if (evaluateContains)
                    {
                        sqlColumns.Remove(identityColumn);
                    }
                }
            }

            var container = new DuplicateItemContainer()
            {
                Columns = sqlColumns,
                OrderBy = OrderByComboBox.SelectedIndex == 0 ? "" : OrderByComboBox.Text
            };

            var manager = new DuplicateManager(container);

            // ReSharper disable once PossibleMultipleEnumeration
            var ops = new DataOperations(columns.First().Schema);
            var duplicatesDataTable = ops.Duplicates(manager.Statement);

            if (!ops.IsSuccessFul)
            {
                MessageBox.Show("Failure");
                return;
            }

            if (duplicatesDataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no duplicates in the selected table");
                return;
            }

            if (identityColumn != null)
            {

                var f = new ResultsForm(
                    DatabaseTableNamesListBox.Text, 
                    duplicatesDataTable, 
                    identityColumn.ColumnName);

                try
                {                
                    f.ShowDialog();
                }
                finally
                {
                    f.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No identity column, can not continue");
            }
        }
    }
}
