using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Library
{
    public class DataOperations : BaseSqlServerConnections
    {
        public DataOperations(string pDefaultCatalog)
        {
            DefaultCatalog = pDefaultCatalog;
        }

        public DataTable Duplicates(string pStatement)
        {
            var dt = new DataTable();

            dt.Columns.Add(new DataColumn() {ColumnName = "Process", DataType = typeof(bool), DefaultValue = false});

            using (var cn = new SqlConnection() {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand() {Connection = cn})
                {
                    cmd.CommandText = pStatement;
                    try
                    {
                        cn.Open();
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return dt;
        }
    }
}
