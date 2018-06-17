﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace SQL_Library
{
    public class SqlColumns : BaseSqlServerConnections
    {
        public string Catalog => DefaultCatalog;
        public string TableName { get; set; }
        public SqlColumns(string pCatalog, string pTableName)
        {
            DefaultCatalog = pCatalog;
            TableName = pTableName;
        }

        private string _identityColumnName;
        /// <summary>
        /// Identity column name
        /// </summary>
        public string IdentityColumnName
        {
            get { return _identityColumnName; }
        }
        /// <summary>
        /// Indicate if the table has a identity column
        /// </summary>
        public bool HasIdentity => !string.IsNullOrWhiteSpace(_identityColumnName);

        public List<SqlColumn> GetColumns()
        {
            var columnList = new List<SqlColumn>();

            var selectColumnNamesStatement = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " + 
                                  "WHERE TABLE_NAME = @TableName";

            var selectIdentityStatement = "SELECT c.COLUMN_NAME " + 
                                          "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS p " + 
                                          "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE c ON  " + 
                                          "c.TABLE_NAME = p.TABLE_NAME " + 
                                          "AND c.CONSTRAINT_NAME = p.CONSTRAINT_NAME " + 
                                          "INNER JOIN INFORMATION_SCHEMA.COLUMNS cls ON  " + 
                                          "c.TABLE_NAME = cls.TABLE_NAME AND c.COLUMN_NAME = cls.COLUMN_NAME " +
                                          "WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND c.TABLE_NAME = @TableName";

            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    cmd.CommandText = selectColumnNamesStatement;
                    cmd.Parameters.AddWithValue("@TableName", TableName);
                    try
                    {
                        cn.Open();

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            columnList.Add(new SqlColumn() {Schema = Catalog, TableName = TableName, ColumnName = reader.GetString(0), IsIdentity = false});
                        }

                        reader.Close();

                        cmd.CommandText = selectIdentityStatement;
                        var keyName = Convert.ToString(cmd.ExecuteScalar());
                        if (!string.IsNullOrWhiteSpace(keyName))
                        {
                            var column = columnList.FirstOrDefault(col => col.ColumnName == keyName);
                            if (column != null)
                            {
                                column.IsIdentity = true;
                                _identityColumnName = column.ColumnName;
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return columnList;

        }
    }
}
