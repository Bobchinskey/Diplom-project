using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Partner.ViewModels.Views.Maneger.Pages
{
    class MainPageRentalViewModel : ViewModelBase
    {

        #region Информационная таблица аренд : rentalTable

        private DataTable _rentalTable;

        /// <summary>rentalTable</summary>
        public DataTable rentalTable
        {
            get => _rentalTable;
            set => Set(ref _rentalTable, value);
        }

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        /*------------------------------------------------------------------------------------------------*/

        public MainPageRentalViewModel()
        {
            DataColumn column;
            DataRow row;

            int Count = 0;

            DataTable rentalStartTable = new DataTable();
            DataTable rentalMediunTable = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select  make_model, Substring((Select ',' + start_date_rental + '-' + end_date_rental  From rental_info_menu B Where B.make_model = A.make_model For XML Path('')),2,8000) As Workgroups From rental_info_menu A Group By A.make_model";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            rentalStartTable.Load(thisReader);

            for (int i = 0; i < rentalStartTable.Rows.Count; i++)
            {
                string phrase = Convert.ToString(rentalStartTable.Rows[i][1]);
                string[] words = phrase.Split(',');
                if (Count < words.Length)
                {
                    Count = words.Length;
                }
            }

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Автомобили";
            column.ReadOnly = true;
            column.Unique = false;
            rentalMediunTable.Columns.Add(column);

            for (int i = 0; Count > i; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = Convert.ToString(i);
                column.ReadOnly = true;
                column.Unique = false;
                rentalMediunTable.Columns.Add(column);
            }

            for (int i = 0; i < rentalStartTable.Rows.Count; i++)
            {
                row = rentalMediunTable.NewRow();
                row["Автомобили"] = rentalStartTable.Rows[i][0];
                string phrase = Convert.ToString(rentalStartTable.Rows[i][1]);
                string[] words = phrase.Split(',');
                for (int k = 0; k < words.Length; k++)
                {
                    row[k+1] = words[k];
                }
                rentalMediunTable.Rows.Add(row);
            }

            rentalTable = rentalMediunTable;

        }

    }
}
