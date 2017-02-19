using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;

namespace Andon
{
    class SqlCEHelper
    {
        static string conStr_Local = "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\AppDatabase.sdf;Persist Security Info=False;";


        public static DataSet ExecuteDataTable(string SQLString)
        {
            using (SqlCeConnection connection = new SqlCeConnection(conStr_Local))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlCeDataAdapter command = new SqlCeDataAdapter(SQLString, connection);
                    command.Fill(ds, "data");
                }
                catch (SqlCeException sqlCeEx)
                {
                    throw (sqlCeEx);
                    
                }

                connection.Close();
                return (ds);
            }
        }

        public static int ExecuteNonQuery(string SQLString)
        {
            using (SqlCeConnection connection = new SqlCeConnection(conStr_Local))
            {
                using (SqlCeCommand cmd = new SqlCeCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlCeException sqlCeEx)
                    {
                        throw (sqlCeEx);
                    }
                }
            }
        }


        public static int ExecuteNonQuery(string SQLString, params SqlCeParameter[] cmdParms)
        {
            using (SqlCeConnection connection = new SqlCeConnection(conStr_Local))
            {
                using (SqlCeCommand cmd = new SqlCeCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        if (cmdParms != null)
                        {
                            foreach (SqlCeParameter localParam in cmdParms)
                                cmd.Parameters.Add(localParam);
                        }

                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SqlCeException sqlCeEx)
                    {
                        throw (sqlCeEx);
                    }
                }
            }
        }
    }
}
