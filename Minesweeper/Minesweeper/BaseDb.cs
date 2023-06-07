using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public abstract class BaseDb
    {
        public SqlConnection Connection { get; } = new SqlConnection(Settings.Default.ConnectionString);

        public void RunCommand(string command)
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand(command,Connection);
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }
        public void RunCommand(string command, params object[] list)
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand(command, Connection);
            foreach (var value in list)
            {
                sqlCommand.Parameters.AddWithValue($"@{value.ToString().ToLower()}",value);

            }
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }

  
    }
}
