using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DuplicateRecordsProject.Extensions;

namespace DuplicateRecordsProject
{
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            InitializeComponent();
        }

        private readonly BindingSource _bs = new BindingSource();
        private readonly DataTable _dataTable;
        private readonly string _tableName;
        private readonly string _identityColumnName;

        private string _deleteStatement;

        /// <summary>
        /// When this form closes this is your DELETE statement.
        /// </summary>
        public string DeleteStatement => _deleteStatement;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTableName">SQL-Server Table name</param>
        /// <param name="pDataTable">DataTable containing duplicate rows</param>
        /// <param name="pIdentityColumn">Identity/primary key column name</param>
        public ResultsForm(string pTableName, DataTable pDataTable, string pIdentityColumn)
        {
            InitializeComponent();

            _tableName = pTableName;
            _dataTable = pDataTable;
            _identityColumnName = pIdentityColumn;

            Shown += ResultsForm_Shown;
        }

        private void ResultsForm_Shown(object sender, EventArgs e)
        {
            _bs.DataSource = _dataTable;
            dataGridView1.DataSource = _bs;

            Text = $"Duplicates for '{_tableName}'";
        }
        /// <summary>
        /// Build select list from unbound DataGridViewCheckBox column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        private void cmdProcess_Click(object sender, EventArgs ea)
        {
            // ReSharper disable once TooWideLocalVariableScope
            List<int> identityList;

            try
            {
                // get checked rows which are used to create a DELETE FROM statement.
                identityList = (_bs.DataTable()
                    .AsEnumerable()
                    .Where(T => T.Field<bool>("Process"))
                    .Select(T => T.Field<int>(_identityColumnName)))
                    .ToList();

                // Determine if there are one or more rows to create the DELETE statement
                if (identityList.Count >0)
                {
                    /*
                     * No formal parameters needed e.g. cmd.Parameters.Add . . .
                     */
                    var deleteStatement = $"DELETE FROM {_tableName} " + 
                                          $"WHERE {_identityColumnName} IN " + 
                                          $"({string.Join(",", identityList.ToArray().ToArray())})";

                    _deleteStatement = deleteStatement;

                    /*
                     * For learning
                     */
                    if (Environment.UserName == "Karens")
                    {
                        Console.WriteLine(deleteStatement);
                    }
                    else
                    {
                        MessageBox.Show(deleteStatement);
                    }                   
                }               
            }
            catch (Exception e)
            {
                MessageBox.Show($"Selection or rows failed\n{e.Message}");
            }
        }
    }
}
