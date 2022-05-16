using Partner.Infrastructure.Commands;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Rental;
using Partner.Views.Windows.MainWindowInteraction.Rental.AddRental;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental
{
    class ListRentalWindowVeiwModel : ViewModelBase
    {

        #region Данные

        #region DataTable данных Аредн : MainListRental

        private DataTable _MainListRental;

        /// <summary>MainListRental</summary>
        public DataTable MainListRental
        {
            get => _MainListRental;
            set => Set(ref _MainListRental, value);
        }

        public DataTable MainListRentalDelivery = new DataTable("MainListRentalDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #region Выбранный элемент таблицы "Аренды" : SelectedRental

        private int _SelectedRental = 0;

        /// <summary>SelectedRental</summary>
        public int SelectedRental
        {
            get => _SelectedRental;
            set => Set(ref _SelectedRental, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства аренд : SelectedRentalProperty

        private string _SelectedRentalProperty = "Действующие";

        /// <summary>SelectedClientProperty</summary>
        public string SelectedRentalProperty
        {
            get => _SelectedRentalProperty;
            set => Set(ref _SelectedRentalProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Действующие", "Завершенные" };

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

        #region Команда Фильтрации данных "FilterRantalStatusCommand"

        public ICommand FilterRantalStatusCommand { get; }

        private bool CanFilterRantalStatusCommandExecute(object p) => true;

        private void OnFilterRantalStatusCommandExecuted(object p)
        {
            if (SelectedRentalProperty == "Действующие")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListRentalDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListRentalDelivery.Columns.Add(column);

                int k = MainListRentalDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListRentalDelivery.Rows[i]["num"] = i + 1;
                }

                MainListRental = MainListRentalDelivery;
            }
            else if (SelectedRentalProperty == "Завершенные")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition='Завершенна') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition='Завершенна') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListRentalDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListRentalDelivery.Columns.Add(column);

                int k = MainListRentalDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListRentalDelivery.Rows[i]["num"] = i + 1;
                }

                MainListRental = MainListRentalDelivery;
            }
        }

        #endregion

        #region Команда Вызова окна "ListBookingWindow"

        public ICommand OpenListBookingWindowCommand { get; }

        private bool CanOpenListBookingWindowCommandExecute(object p) => true;

        private void OnOpenListBookingWindowCommandExecuted(object p)
        {
            ListBookingWindow listBookingWindow = new ListBookingWindow();
            listBookingWindow.ShowDialog();

            FilterRantalStatusCommand.Execute(null);
        }

        #endregion

        #region Команда Добавления аренды

        public ICommand OpenSelectedDateRentalWindowCommand { get; }

        private bool CanOpenSelectedDateRentalWindowCommandExecute(object p) => true;

        private void OnOpenSelectedDateRentalWindowCommandExecuted(object p)
        {
            SelectedDateRentalWindow selectedDateRentalWindow = new SelectedDateRentalWindow();
            selectedDateRentalWindow.Show();

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListRentalWindowVeiwModel()
        {
            #region Команды

            FilterRantalStatusCommand = new LamdaCommand(OnFilterRantalStatusCommandExecuted, CanFilterRantalStatusCommandExecute);

            OpenListBookingWindowCommand = new LamdaCommand(OnOpenListBookingWindowCommandExecuted, CanOpenListBookingWindowCommandExecute);

            OpenSelectedDateRentalWindowCommand = new LamdaCommand(OnOpenSelectedDateRentalWindowCommandExecuted, CanOpenSelectedDateRentalWindowCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            
            MainListRentalDelivery = dt;
            
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListRentalDelivery.Columns.Add(column);

            int k = MainListRentalDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListRentalDelivery.Rows[i]["num"] = i + 1;
            }

            MainListRental = MainListRentalDelivery;

            #endregion
        }
    }
}
