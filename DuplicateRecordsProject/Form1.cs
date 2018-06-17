using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuplicateRecordsProject.Classes;
using DuplicateRecordsProject.Extensions;
using SQL_Library;
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

        private List<string> countryColumnList;
        /// <summary>
        /// Get databases for server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            

            var ops = new SqlDatabases();
            lstDatabaseNames.DataSource = ops.DatabaseNames();

            if (ops.IsKarenMachine)
            {
                lstDatabaseNames.SelectedIndex = lstDatabaseNames.FindString("NorthWindDemo");
                lstTableNames.SelectedIndex = lstTableNames.FindString("Customer");
                var tOps = new SqlTables("NorthWindDemo");
                countryColumnList = tOps.CountrySelected;
                //
                foreach (var item in countryColumnList)
                {
                    clbColumns.FindItemAndSetChecked(item);
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
            var ops = new SqlTables(lstDatabaseNames.Text);
            lstTableNames.DataSource = null;
            lstTableNames.DataSource = ops.TableNames();
        }
        /// <summary>
        /// Get all columns for selected table.
        /// If there is a primary key it's mark as one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ops = new SqlColumns(lstDatabaseNames.Text,lstTableNames.Text);
            var results = ops.GetColumns();

            clbColumns.Items.Clear();

            if (results.Count <= 0) return;

            cboOrderBy.Items.Clear();
            cboOrderBy.Items.Add("None");
            foreach (var column in results)
            {
                clbColumns.Items.Add(column);
                if (column.IsIdentity) continue;
                cboOrderBy.Items.Add(column.ColumnName);
            }

            cboOrderBy.SelectedIndex = 0;
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
        /// SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SomeTable'
        /// 
        /// </remarks>
        private void cmdCheckForDuplicates_Click(object sender, EventArgs e)
        {
            if (clbColumns.Items.Count == 0) return;

            var allColumns = clbColumns.Items.OfType<SqlColumn>();
            var identityColumn = allColumns.FirstOrDefault(col => col.IsIdentity);

            var sqlColumns = clbColumns.CheckedIColumnDetailsList();

            if (sqlColumns.Count >0)
            {
                if (identityColumn != null)
                {
                    var test = sqlColumns.Contains(identityColumn);
                    if (test)
                    {
                        sqlColumns.Remove(identityColumn);
                    }
                }
            }

            var container = new DuplicateItemContainer()
            {
                Columns = sqlColumns,
                OrderBy = cboOrderBy.SelectedIndex == 0 ? "" : cboOrderBy.Text
            };

            var manager = new DuplicateManager(container);

            // ReSharper disable once PossibleMultipleEnumeration
            var ops = new DataOperations(allColumns.First().Schema);
            var dt = ops.Duplicates(manager.Statement);
            if (!ops.IsSuccessFul)
            {
                MessageBox.Show("Failure");
                return;
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There are no duplicates in the selected table");
                return;
            }
            if (identityColumn != null)
            {
                var f = new ResultsForm(lstTableNames.Text, dt, identityColumn.ColumnName);
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
