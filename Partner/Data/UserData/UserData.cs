using Partner.Models.PersonalData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Data.UserData
{
    class UserData
    {
        public UserData(string Login, string Password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select * from [user] where [login]='" + Login + "' and [password]='" + Password + "' and reality='Актуально'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            if (thisReader.HasRows)
            {
                UserDataModel.access_lavel = thisReader["access_lavel"].ToString();
                UserDataModel.surname = thisReader["surname"].ToString();
                UserDataModel.name = thisReader["name"].ToString();
                UserDataModel.patronymic = thisReader["patronymic"].ToString();
                UserDataModel.id_user = Convert.ToInt32(thisReader["id_user"].ToString());
                UserDataModel.image = thisReader["image"];
            }
            else
                UserDataModel.id_user = -1;
            thisReader.Close();
            ThisConnection.Close();
        }
    }
}
