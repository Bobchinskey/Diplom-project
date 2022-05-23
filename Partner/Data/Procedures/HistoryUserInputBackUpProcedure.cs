using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Partner.Data.Procedures
{
    class HistoryUserInputBackUpProcedure
    {
        public HistoryUserInputBackUpProcedure(int id_user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            var command = ThisConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "add_history";
            command.Parameters.AddWithValue("@id_user", id_user);
            command.ExecuteNonQuery();

            DateTime dateBackup;

            SqlCommand thisCommand = ThisConnection.CreateCommand();

            thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select TOP(1) Date_BACKUP from [BACKUP_DataBase] ORDER BY Date_BACKUP DESC";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            if (thisReader.HasRows)
            {
                dateBackup = Convert.ToDateTime(thisReader["Date_BACKUP"].ToString());
            }
            else
            {
                dateBackup = DateTime.Today;
            }
            thisReader.Close();

            if (dateBackup < DateTime.Today)
            {
                command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BACKUP";
                command.Parameters.AddWithValue("@Date_BACKUP", DateTime.Today);
                command.ExecuteNonQuery();
            }
            
            ThisConnection.Close();
        }
    }
}
