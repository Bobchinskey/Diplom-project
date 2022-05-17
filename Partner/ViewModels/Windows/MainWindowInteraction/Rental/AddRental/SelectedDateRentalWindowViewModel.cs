using Partner.Infrastructure.Commands;
using Partner.Models.Rental;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental.AddRental
{
    class SelectedDateRentalWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Дата начала аренды: StartDateRental

        private DateTime _StartDateRental = DateTime.Today;

        /// <summary>StartDateRental</summary>

        public DateTime StartDateRental
        {
            get => _StartDateRental;
            set => Set(ref _StartDateRental, value);
        }

        #endregion

        #region Дата окончания аренды: EndDateRental

        private DateTime _EndDateRental = DateTime.Today;

        /// <summary>EndDateRental</summary>

        public DateTime EndDateRental
        {
            get => _EndDateRental;
            set => Set(ref _EndDateRental, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда кнопки "Далее" открывющая окно "SelectedClientWindow"

        public ICommand OpenSelectedClientWindowWindowCommand { get; }

        private bool CanOpenSelectedClientWindowCommandExecute(object p) => true;

        private void OnOpenSelectedClientWindowCommandExecuted(object p)
        {
            if (StartDateRental <= EndDateRental)
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select vehicle.id_vehicle, rental.start_date_rental,rental.end_date_rental from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and vehicle.id_vehicle = '" + VehicleDataModel.id_vehicle + "') Union ((Select vehicle.id_vehicle, booking_natural_person.start_date_booking, booking_natural_person.end_date_booking from booking_natural_person, natural_person, vehicle where booking_natural_person.id_natural_person = natural_person.id_natural_person and vehicle.id_vehicle = booking_natural_person.id_vehicle and booking_natural_person.reality != 'Отменено' and booking_natural_person.reality != 'Закрыта' and vehicle.id_vehicle = '" + VehicleDataModel.id_vehicle + "') Union (Select vehicle.id_vehicle, booking_legal_entity.start_date_booking, booking_legal_entity.end_date_booking from booking_legal_entity, legal_entity, vehicle where booking_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = booking_legal_entity.id_vehicle and booking_legal_entity.reality != 'Отменено' and booking_legal_entity.reality != 'Закрыта' and vehicle.id_vehicle='" + VehicleDataModel.id_vehicle + "'))";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                int k = 0;
                int error = 0;

                for (int i = 1; dt.Rows.Count >= i; i++)
                {
                    if (((Convert.ToDateTime(dt.Rows[k][1]) <= StartDateRental) && (Convert.ToDateTime(dt.Rows[k][2]) >= StartDateRental)) || ((Convert.ToDateTime(dt.Rows[k][1]) <= EndDateRental) && (Convert.ToDateTime(dt.Rows[k][2]) >= EndDateRental)) || ((StartDateRental < Convert.ToDateTime(dt.Rows[k][1])) && (EndDateRental > Convert.ToDateTime(dt.Rows[k][2]))))
                    {
                        error++;
                    }
                    k++;
                }

                if (error == 0)
                {
                    DataStaticRental.StartDateRental = StartDateRental;
                    DataStaticRental.EndDateRental = EndDateRental;

                    SelectedClientWindow selectedClientWindow = new SelectedClientWindow();
                    selectedClientWindow.Show();

                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.DataContext == this)
                        {
                            window.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выбранный период аренды уже занят.", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Приод аренды выбран неверно.","Ошибка");
            }
        }

        #endregion



        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public SelectedDateRentalWindowViewModel()
        {
            #region Команды

            OpenSelectedClientWindowWindowCommand = new LamdaCommand(OnOpenSelectedClientWindowCommandExecuted, CanOpenSelectedClientWindowCommandExecute);

            #endregion
        }
    }
}
