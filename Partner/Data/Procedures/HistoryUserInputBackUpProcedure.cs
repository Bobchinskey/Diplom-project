using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            ThisConnection.Close();
        }
    }
}
