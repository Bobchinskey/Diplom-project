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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Booking.AddBooking
{
    internal class EditBookingWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Заголовок : Title

        private string _Title = DataStaticRental.Title;

        /// <summary>Title</summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Заказчик : Client

        private string _Client;

        /// <summary>Client</summary>

        public string Client
        {
            get => _Client;
            set => Set(ref _Client, value);
        }

        #endregion

        #region Транспортное средство : Vehicle

        private string _Vehicle;

        /// <summary>Vehicle</summary>

        public string Vehicle
        {
            get => _Vehicle;
            set => Set(ref _Vehicle, value);
        }

        #endregion

        #region Дата начала срока Аренды : StartDateRental

        private DateTime _StartDateRental = DataStaticRental.StartDateRental;

        /// <summary>StartDateRental</summary>

        public DateTime StartDateRental
        {
            get => _StartDateRental;
            set => Set(ref _StartDateRental, value);
        }

        #endregion

        #region Дата окончания срока Аренды : EndDateRental

        private DateTime _EndDateRental = DataStaticRental.EndDateRental;

        /// <summary>EndDateRental</summary>

        public DateTime EndDateRental
        {
            get => _EndDateRental;
            set => Set(ref _EndDateRental, value);
        }

        #endregion

        #region Итоговая стоимость : Cost

        private string _Cost;

        /// <summary>Cost</summary>

        public string Cost
        {
            get => _Cost;
            set => Set(ref _Cost, value);
        }

        int CostST;

        int day;

        #endregion

        #region Залог : Deposit

        private string _Deposit;

        /// <summary>Deposit</summary>

        public string Deposit
        {
            get => _Deposit;
            set => Set(ref _Deposit, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Отмены Бронирования : EditCancelBookingCommand

        public ICommand EditCancelBookingCommand { get; }

        private bool CanEditCancelBookingCommandExecute(object p) => true;

        private void OnEditCancelBookingCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            if (DataStaticRental.Type == "Физическое лицо")
            {
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_booking_natural_person";
                command.Parameters.AddWithValue("@id_booking", DataStaticRental.IDContract);
                command.Parameters.AddWithValue("@reality", "Отменено");
                command.ExecuteNonQuery();
            }
            else
            {
                if (DataStaticRental.Type == "Юридическое лицо")
                {
                    var command = ThisConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Edit_booking_legal_entity";
                    command.Parameters.AddWithValue("@id_booking", DataStaticRental.IDContract);
                    command.Parameters.AddWithValue("@reality", "Отменено");
                    command.ExecuteNonQuery();
                }
            }

            ThisConnection.Close();

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #region Команда Закрытия Бронирования : EditCloseBookingCommand

        public ICommand EditCloseBookingCommand { get; }

        private bool CanEditCloseBookingCommandExecute(object p) => true;

        private void OnEditCloseBookingCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            if (DataStaticRental.Type == "Физическое лицо")
            {
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_booking_natural_person";
                command.Parameters.AddWithValue("@id_booking", DataStaticRental.IDContract);
                command.Parameters.AddWithValue("@reality", "Закрыто");
                command.ExecuteNonQuery();

                AddRentalWindow addRentalWindow = new AddRentalWindow();
                addRentalWindow.Show();

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
                if (DataStaticRental.Type == "Юридическое лицо")
                {
                    DataStaticRental.Edit = "Выбор представителя для подписания акта выдачи";

                    SelectedRepresentativesOrganizationWindow selectedRepresentativesOrganizationWindow = new SelectedRepresentativesOrganizationWindow();
                    selectedRepresentativesOrganizationWindow.ShowDialog();

                    if (DataStaticRental.Edit == "Прошло успешно")
                    {
                        var command = ThisConnection.CreateCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Edit_booking_legal_entity";
                        command.Parameters.AddWithValue("@id_booking", DataStaticRental.IDContract);
                        command.Parameters.AddWithValue("@reality", "Закрыто");
                        command.ExecuteNonQuery();

                        AddRentalWindow addRentalWindow = new AddRentalWindow();
                        addRentalWindow.Show();

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
                        MessageBox.Show("Представитель организации не выбран!","Ошибка");
                    }
                }
            }

            ThisConnection.Close();

        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public EditBookingWindowViewModel()
        {
            #region Команды

            EditCancelBookingCommand = new LamdaCommand(OnEditCancelBookingCommandExecuted, CanEditCancelBookingCommandExecute);

            EditCloseBookingCommand = new LamdaCommand(OnEditCloseBookingCommandExecuted, CanEditCloseBookingCommandExecute);

            #endregion

            #region Данные

            int CostRate = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();

            if (DataStaticRental.Type == "Физическое лицо")
            {
                thisCommand.CommandText = "Select [surname] + ' ' + [name] + ' ' + patronymic as name from natural_person  where id_natural_person=" + DataStaticRental.IDClient;
            }
            else
            {
                thisCommand.CommandText = "Select name_organization as name from legal_entity  where id_legal_entity=" + DataStaticRental.IDClient;
            }
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Client = thisReader["name"].ToString();
            thisReader.Close();
            thisCommand.CommandText = "Select make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle  where id_vehicle=" + VehicleDataModel.id_vehicle;
            thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Vehicle = thisReader["make_model"].ToString();
            thisReader.Close();

            day = (EndDateRental - StartDateRental).Days + 1;

            if (day <= 3)
            {
                thisCommand.CommandText = "Select [1-3_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }
            else if (day <= 9)
            {
                thisCommand.CommandText = "Select [4-9_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }
            else if (day <= 29)
            {
                thisCommand.CommandText = "Select [10-29_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }
            else
            {
                thisCommand.CommandText = "Select [30_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }

            thisCommand.CommandText = "Select Deposit from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
            thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Deposit = thisReader["Deposit"].ToString();
            thisReader.Close();

            ThisConnection.Close();

            Cost = Convert.ToString(CostRate * day);
            CostST = CostRate * day;

            #endregion
        }
    }
}
