using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlStatementsLibrary;

namespace SQL_Library
{
    /// <summary>
    /// Get tables for the current catalog
    /// </summary>
    public class SqlTables : BaseSqlServerConnections
    {
        public SqlTables(string pDefaultCatalog)
        {
            DefaultCatalog = pDefaultCatalog;
        }

        /// <summary>
        /// Used by the author to auto select fields for testing purposes in the main form
        /// Shown event.
        /// </summary>
        public List<string> CountrySelected = new List<string>()
        {
            "CompanyName", "ContactName", "ContactTitle", "Address", "City", "PostalCode"
        };
        /// <summary>
        /// Get tables for the current catalog
        /// </summary>
        /// <returns></returns>
        public List<string> TableNames() 
        {

            var nameList = new List<string>();

            //var selectStatement = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES " + 
            //                      "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME != 'sysdiagrams' " + 
            //                        "AND TABLE_NAME NOT IN ('TableNames','TableColumnInformation') " + 
            //                      "ORDER BY TABLE_NAME";

            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    cmd.CommandText = Queries.TableNamesStatement;
                    try
                    {
                        cn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            nameList.Add(reader.GetString(0));
                        }

                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;

                    }
                }
            }

            return nameList;

        }

        private bool CheckDatabase(string databaseName)
        {
            // You know it's a string, use var
            var connString = $"Server={DatabaseServer};Integrated Security=True;database=master";
            // Note: It's better to take the connection string from the config file.

            var cmdText = "select count(*) from master.dbo.sysdatabases where name=@database";

            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var sqlCmd = new SqlCommand(cmdText, sqlConnection))
                {
                    // Use parameters to protect against Sql Injection
                    sqlCmd.Parameters.Add("@database", System.Data.SqlDbType.NVarChar).Value = databaseName;

                    // Open the connection as late as possible
                    sqlConnection.Open();
                    // count(*) will always return an int, so it's safe to use Convert.ToInt32
                    return Convert.ToInt32(sqlCmd.ExecuteScalar()) == 1;
                }
            }

        }

        public bool NorthWindDemoExists()
        {
            return CheckDatabase("NorthWindDemo");
        }

    }
}
