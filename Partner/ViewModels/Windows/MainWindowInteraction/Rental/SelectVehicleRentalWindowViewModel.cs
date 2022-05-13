using Partner.Infrastructure.Commands;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Rental;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental
{
    class SelectVehicleRentalWindowViewModel : ViewModelBase
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

        #region Команда обновления окно "Выбор транспортного средства"

        public ICommand ReturnListRentalWindowCommand { get; }

        private bool CanReturnListRentalWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnReturnListRentalWindowCommandExecuted(object p)
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

        public ICommand OpenListRentalWindowCommand { get; }

        private bool CanOpenListRentalWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnOpenListRentalWindowCommandExecuted(object p)
        {
            VehicleDataModel.id_vehicle = MainListVehicle[SelectedVehicle].id_vehicle;

            ListRentalWindow listRentalWindow = new ListRentalWindow();
            listRentalWindow.ShowDialog();

            ReturnListRentalWindowCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public SelectVehicleRentalWindowViewModel()
        {

            #region Команды

            OpenListRentalWindowCommand = new LamdaCommand(OnOpenListRentalWindowCommandExecuted, CanOpenListRentalWindowCommandExecute);

            ReturnListRentalWindowCommand = new LamdaCommand(OnReturnListRentalWindowCommandExecuted, CanReturnListRentalWindowCommandExecute);

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
