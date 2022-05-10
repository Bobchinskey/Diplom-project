using Partner.Infrastructure.Commands;
using Partner.Models.Maintenances;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Maintenances.Additional;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Maintenances
{
    class ListMaintenanceWindowViewModel : ViewModelBase
    {

        #region Данные

        #region List данных Технических обслуживаний : MainListMaintenance

        
        private DataTable _MainListMaintenance;

        /// <summary>MainListMaintenance</summary>
        public DataTable MainListMaintenance
        {
            get => _MainListMaintenance;
            set => Set(ref _MainListMaintenance, value);
        }

        public DataTable MainListMaintenanceDelivery = new DataTable("MainListMaintenanceDelivery");

        #endregion

        #region Переменные row, column

        DataColumn column;
        DataRow row;

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

        #region Команда обновления окна "ListMaintenanceWindow"

        public ICommand ReturnListMaintenanceWindowCommand { get; }

        private bool CanReturnListMaintenanceWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnReturnListMaintenanceWindowCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_maintenance,name_maintenance,start_date_maintenance,end_date_maintenance from maintenance where id_vehicle = '" + VehicleDataModel.id_vehicle + "'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            MainListMaintenanceDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListMaintenanceDelivery.Columns.Add(column);

            int k = MainListMaintenanceDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListMaintenanceDelivery.Rows[i]["num"] = i + 1;
            }

            MainListMaintenance = MainListMaintenanceDelivery;

        }

        #endregion

        #region Команда вызова окна "AddMaintenanceWindow"

        public ICommand AddMaintenanceWindowCommand { get; }

        private bool CanAddMaintenanceWindowCommandExecute(object p) => true;

        private void OnAddMaintenanceWindowCommandExecuted(object p)
        {
            AddMaintenanceWindow addMaintenanceWindow = new AddMaintenanceWindow();
            addMaintenanceWindow.ShowDialog();

            ReturnListMaintenanceWindowCommand.Execute(null);
        }

        #endregion

        #region Команда Удаления Технического обслуживания : DropMaintenanceCommand

        public ICommand DropMaintenanceCommand { get; }

        private bool CanDropMaintenanceCommandExecute(object p) => SelectedVehicle > -1;

        private void OnDropMaintenanceCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_maintenance";
                command.Parameters.AddWithValue("@id_maintenance", MainListMaintenance.Rows[SelectedVehicle][0]);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnListMaintenanceWindowCommand.Execute(null);
            }
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListMaintenanceWindowViewModel()
        {

            #region Команды

            ReturnListMaintenanceWindowCommand = new LamdaCommand(OnReturnListMaintenanceWindowCommandExecuted, CanReturnListMaintenanceWindowCommandExecute);

            AddMaintenanceWindowCommand = new LamdaCommand(OnAddMaintenanceWindowCommandExecuted, CanAddMaintenanceWindowCommandExecute);

            DropMaintenanceCommand = new LamdaCommand(OnDropMaintenanceCommandExecuted, CanDropMaintenanceCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_maintenance,name_maintenance,start_date_maintenance,end_date_maintenance from maintenance where id_vehicle = '" + VehicleDataModel.id_vehicle + "'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();

            MainListMaintenanceDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListMaintenanceDelivery.Columns.Add(column);

            int k = MainListMaintenanceDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListMaintenanceDelivery.Rows[i]["num"] = i + 1;
            }

            MainListMaintenance = MainListMaintenanceDelivery;

            #endregion

        }
    }
}
