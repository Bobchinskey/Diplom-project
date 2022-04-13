using Partner.Infrastructure.Commands;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.VehicleWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.VehicleWindow
{
    class ListVehicleWindowViewModel : ViewModelBase
    {

        #region Данные

        #region List данных автомобилей : MainListVehicle

        private List<ListVehicle> _MainListVehicle;

        public List<ListVehicle> MainListVehicle
        {
            get => _MainListVehicle;
            set => Set(ref _MainListVehicle, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства автомобилей : SelectedVehicleProperty

        private string _SelectedVehicleProperty = "В автопарке";

        /// <summary>SelectedVehicleProperty</summary>
        public string SelectedVehicleProperty
        {
            get => _SelectedVehicleProperty;
            set => Set(ref _SelectedVehicleProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "В автопарке", "В архиве", "Все" };

        /// <summary>status</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда фильтрации автомобилей по их статусу

        public ICommand FilterVehicleStatusCommand { get; }

        private bool CanFilterVehicleStatusCommandExecute(object p) => true;

        private void OnFilterVehicleStatusCommandExecuted(object p)
        {
            if (SelectedVehicleProperty == "В автопарке")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_vehicle,make_model,state_number,category,status from vehicle where status!='В архиве'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), state_number = se.Field<string>("state_number"), category = se.Field<string>("category"), status = se.Field<string>("status") }).ToList();
                ThisConnection.Close();
                int k = MainListVehicle.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListVehicle[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "В архиве")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_vehicle,make_model,state_number,category,status from vehicle where status='В архиве'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), state_number = se.Field<string>("state_number"), category = se.Field<string>("category"), status = se.Field<string>("status") }).ToList();
                ThisConnection.Close();
                int k = MainListVehicle.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListVehicle[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Все")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_vehicle,make_model,state_number,category,status from vehicle";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), state_number = se.Field<string>("state_number"), category = se.Field<string>("category"), status = se.Field<string>("status") }).ToList();
                ThisConnection.Close();
                int k = MainListVehicle.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListVehicle[i].num = i + 1;
                }
            }
        }

        #endregion

        #region Команда вызывающая окно "Добавить автомобиль"

        public ICommand OpenAddVehicleWindowCommand { get; }

        private bool CanOpenAddVehicleWindowCommandExecute(object p) => true;

        private void OnOpenAddVehicleWindowCommandExecuted(object p)
        {
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
            addVehicleWindow.Show();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListVehicleWindowViewModel()
        {

            #region Команды

            FilterVehicleStatusCommand = new LamdaCommand(OnFilterVehicleStatusCommandExecuted, CanFilterVehicleStatusCommandExecute);

            OpenAddVehicleWindowCommand = new LamdaCommand(OnOpenAddVehicleWindowCommandExecuted, CanOpenAddVehicleWindowCommandExecute);

            #endregion

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_vehicle,make_model,state_number,category,status from vehicle where status!='В архиве'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), state_number = se.Field<string>("state_number"), category = se.Field<string>("category"), status = se.Field<string>("status")}).ToList();
            ThisConnection.Close();
            int k = MainListVehicle.Count;
            for (int i = 0; i < k; i++)
            {
                MainListVehicle[i].num = i + 1;
            }
        }
    }
}
