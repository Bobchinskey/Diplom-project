using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.Models.Rental;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Booking.AddBooking;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
using Partner.Views.Windows.MainWindowInteraction.Rental.AddRental;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental.AddRental
{
    class SelectedClientWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных клиентов : MainListClient

        private List<ListClient> _MainListClient;

        public List<ListClient> MainListClient
        {
            get => _MainListClient;
            set => Set(ref _MainListClient, value);
        }

        #endregion

        #region Выбранный элемент таблицы клиентов : SelectedClient

        private int _SelectedClient = 0;

        /// <summary>SelectedClient</summary>
        public int SelectedClient
        {
            get => _SelectedClient;
            set => Set(ref _SelectedClient, value);
        }

        #endregion

        #region Поисковая строка : SearchText

        private string _SearchText = "";

        /// <summary>SearchText</summary>
        
        public string SearchText
        {
            get => _SearchText;
            set => Set(ref _SearchText,value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда вызывающая окно "Добавить клиента" : OpenAddClientWindowCommand

        public ICommand OpenAddClientWindowCommand { get; }

        private bool CanOpenAddClientWindowCommandExecute(object p) => true;

        private void OnOpenAddClientWindowCommandExecuted(object p)
        {
            ListClientProcedure.EditOrAdd = "Добавление клиента";
            ListClientProcedure.surname = "";
            ListClientProcedure.name = "";
            ListClientProcedure.patronymic = "";
            ListClientProcedure.birthday = DateTime.Today;
            ListClientProcedure.place_birthday = "";
            ListClientProcedure.patronymic = "";
            ListClientProcedure.INN = "";
            ListClientProcedure.series_passport = "";
            ListClientProcedure.number_passport = "";
            ListClientProcedure.who_issued_passport = "";
            ListClientProcedure.date_issued_passport = DateTime.Today;
            ListClientProcedure.number_card = "";
            ListClientProcedure.validity_period_card = DateTime.Today;
            ListClientProcedure.CVC2 = "";
            ListClientProcedure.registration = "";
            ListClientProcedure.actual_place_residence = "";
            ListClientProcedure.phone_number = "";
            ListClientProcedure.email = "";

            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();

            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда вызывающая окно "Добавить юридическое лицо" : OpenAddLegalEntityWindowCommand

        public ICommand OpenAddLegalEntityWindowCommand { get; }

        private bool CanOpenAddLegalEntityWindowCommandExecute(object p) => true;

        private void OnOpenAddLegalEntityWindowCommandExecuted(object p)
        {
            AddLegalEntityWindow addLegalEntityWindow = new AddLegalEntityWindow();
            addLegalEntityWindow.ShowDialog();

            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда обновления данных : UpdateCommand

        public ICommand UpdateCommand { get; }

        private bool CanUpdateCommandExecute(object p) => true;
        
        private void OnUpdateCommandExecute(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, 'Юридическое лицо' as 'type' from legal_entity where reality != 'Архив') Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, 'Физическое лицо' from natural_person where reality != 'Архив') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), type = se.Field<string>("type") }).ToList();
            ThisConnection.Close();
            int k = MainListClient.Count;
            for (int i = 0; i < k; i++)
            {
                MainListClient[i].num = i + 1;
            }
        }

        #endregion

        #region Команда поиска : ScreachCommand

        public ICommand ScreachCommand { get; }

        private bool CanScreachCommandExecute(object p) => true;

        private void OnScreachCommandExecute(object p)
        {
            if (SearchText == "")
            {
                UpdateCommand.Execute(null);
            }
            else
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, 'Юридическое лицо' as 'type' from legal_entity where reality != 'Архив' and name_organization LIKE '%" + SearchText + "%') Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, 'Физическое лицо' from natural_person where reality != 'Архив' and surname LIKE '%" + SearchText + "%') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListClient.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListClient[i].num = i + 1;
                }
            }
        }

        #endregion

        #region Команда вызывающая окно "Добавление аренды" : OpenAddRentalWindowCommand

        public ICommand OpenAddRentalWindowCommand { get; }

        private bool CanOpenAddRentalWindowCommandExecute(object p) => SelectedClient > -1;

        private void OnOpenAddRentalWindowCommandExecute(object p)
        {
            if (DataStaticRental.TypeActions == "Аренда")
            {
                if (MainListClient[SelectedClient].type == "Физическое лицо")
                {
                    DataStaticRental.IDClient = MainListClient[SelectedClient].id_legal_entity;
                    DataStaticRental.Type = MainListClient[SelectedClient].type;
                    DataStaticRental.Title = "Добавление аренды";

                    AddRentalWindow addRentalWindow = new AddRentalWindow();
                    addRentalWindow.Show();
                }
                else
                {
                    DataStaticRental.IDClient = MainListClient[SelectedClient].id_legal_entity;
                    DataStaticRental.Type = MainListClient[SelectedClient].type;
                    DataStaticRental.Title = "Добавление аренды";

                    SelectedRepresentativesOrganizationWindow selectedRepresentativesOrganizationWindow = new SelectedRepresentativesOrganizationWindow();
                    selectedRepresentativesOrganizationWindow.Show();
                }
            }
            else
            {
                if (DataStaticRental.TypeActions == "Бронирование")
                {
                    DataStaticRental.IDClient = MainListClient[SelectedClient].id_legal_entity;
                    DataStaticRental.Type = MainListClient[SelectedClient].type;
                    DataStaticRental.Title = "Добавление бронирования";

                    AddBookingWindow addBookingWindow = new AddBookingWindow();
                    addBookingWindow.Show();
                }
            }
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

        public SelectedClientWindowViewModel()
        {
            #region Команды

            OpenAddClientWindowCommand = new LamdaCommand(OnOpenAddClientWindowCommandExecuted, CanOpenAddClientWindowCommandExecute);

            OpenAddLegalEntityWindowCommand = new LamdaCommand(OnOpenAddLegalEntityWindowCommandExecuted, CanOpenAddLegalEntityWindowCommandExecute);

            UpdateCommand = new LamdaCommand(OnUpdateCommandExecute, CanUpdateCommandExecute);

            ScreachCommand = new LamdaCommand(OnScreachCommandExecute, CanScreachCommandExecute);

            OpenAddRentalWindowCommand = new LamdaCommand(OnOpenAddRentalWindowCommandExecute, CanOpenAddRentalWindowCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, 'Юридическое лицо' as 'type' from legal_entity where reality != 'Архив') Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, 'Физическое лицо' from natural_person where reality != 'Архив') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), type = se.Field<string>("type") }).ToList();
            ThisConnection.Close();
            int k = MainListClient.Count;
            for (int i = 0; i < k; i++)
            {
                MainListClient[i].num = i + 1;
            }

            #endregion
        }
    }
}
