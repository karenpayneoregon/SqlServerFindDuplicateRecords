using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var selectStatement = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES " + 
                                  "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME != 'sysdiagrams' AND TABLE_NAME NOT IN ('TableNames','TableColumnInformation') " + 
                                  "ORDER BY TABLE_NAME";

            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    cmd.CommandText = selectStatement;
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

    }
}
