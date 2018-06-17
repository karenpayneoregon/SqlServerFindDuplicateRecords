using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Library
{
    public class SqlDatabases : BaseSqlServerConnections
    {
        public SqlDatabases()
        {
            if (IsKarenMachine)
            {
                DefaultCatalog = "NorthWindDemo";
            }
            else
            {
                if (DefaultCatalog == "NorthWindDemo")
                {
                    throw new Exception("Must select a catalog in your SQL-Server");
                }

                if (IsKarensDatabaseServer)
                {
                    throw new Exception("Must select SQL-Server instance name or localdb or SQLEXPRESS");
                }               
            }
            
        }
        public SqlDatabases(string pDefaultCatalog)
        {
            DefaultCatalog = pDefaultCatalog;
        }

        public List<string> DatabaseNames()
        {

            var nameList = new List<string>();

            var selectStatement = "SELECT name FROM sys.sysdatabases " + 
                                  "WHERE name NOT IN ('master','msdb','tempdb','model') " + 
                                  "ORDER BY name";

            using (var cn = new SqlConnection() {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand() {Connection = cn})
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
