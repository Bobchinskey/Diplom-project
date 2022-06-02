using Partner.Infrastructure.Commands;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Fine;
using Partner.Views.Windows.MainWindowInteraction.Maintenances;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Maintenances
{
    class SelectVehicleMaintenanceWindowViewModel : ViewModelBase
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

        #region Выбранный элемент таблицы автомобилей : SelectedVehicle

        private int _SelectedVehicle = 0;

        /// <summary>SelectedVehicle</summary>
        public int SelectedVehicle
        {
            get => _SelectedVehicle;
            set => Set(ref _SelectedVehicle, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда обновления окно "Техническое обслуживание"

        public ICommand ReturnListMaintenanceWindowCommand { get; }

        private bool CanReturnListMaintenanceWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnReturnListMaintenanceWindowCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_vehicle, make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle where status!='Архив'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), MakeModel = se.Field<string>("make_model") }).ToList();
            ThisConnection.Close();
            int k = MainListVehicle.Count;
            for (int i = 0; i < k; i++)
            {
                MainListVehicle[i].num = i + 1;
            }
        }

        #endregion

        #region Команда вызывающая окно "Техническое обслуживание"

        public ICommand OpenListMaintenanceWindowCommand { get; }

        private bool CanOpenListMaintenanceWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnOpenListMaintenanceWindowCommandExecuted(object p)
        {
            VehicleDataModel.id_vehicle = MainListVehicle[SelectedVehicle].id_vehicle;

            if (VehicleDataModel.EditOrAdd == "Штрафы")
            {
                ListFineWindow listFineWindow = new ListFineWindow();
                listFineWindow.ShowDialog();
            }
            else if (VehicleDataModel.EditOrAdd == "Техническое обслуживание")
            {
                ListMaintenanceWindow listMaintenanceWindow = new ListMaintenanceWindow();
                listMaintenanceWindow.ShowDialog();
            }

            ReturnListMaintenanceWindowCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public SelectVehicleMaintenanceWindowViewModel()
        {

            #region Команды

            OpenListMaintenanceWindowCommand = new LamdaCommand(OnOpenListMaintenanceWindowCommandExecuted, CanOpenListMaintenanceWindowCommandExecute);

            ReturnListMaintenanceWindowCommand = new LamdaCommand(OnReturnListMaintenanceWindowCommandExecuted, CanReturnListMaintenanceWindowCommandExecute);

            #endregion

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_vehicle, make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle where status!='Архив'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), MakeModel = se.Field<string>("make_model") }).ToList();
            ThisConnection.Close();
            int k = MainListVehicle.Count;
            for (int i = 0; i < k; i++)
            {
                MainListVehicle[i].num = i + 1;
            }
        }
    }
}
