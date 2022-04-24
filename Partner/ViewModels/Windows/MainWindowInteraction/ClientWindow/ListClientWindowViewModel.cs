using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.ClientWindow
{
    class ListClientWindowViewModel : ViewModelBase
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

        #region Выбранный элемент Combobox свойства клиентов : SelectedClientProperty

        private string _SelectedClientProperty = "Актуально";

        /// <summary>SelectedClientProperty</summary>
        public string SelectedVehicleProperty
        {
            get => _SelectedClientProperty;
            set => Set(ref _SelectedClientProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Актуально", "Юр. лица", "Физ. лица", "Архив", "Все"};

        /// <summary>status</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region Видимость кнопки "Востановить удаленныю запись" : VisibleRecoverButton

        private string _VisibleRecoverButton = "Hidden";

        /// <summary>VisibleRecoverButton</summary>
        public string VisibleRecoverButton
        {
            get => _VisibleRecoverButton;
            set => Set(ref _VisibleRecoverButton, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда фильтрации клиентов по их статусу

        public ICommand FilterClientStatusCommand { get; }

        private bool CanFilterClientStatusCommandExecute(object p) => true;

        private void OnFilterClientStatusCommandExecuted(object p)
        {
            if (SelectedVehicleProperty == "Актуально")
            {
                VisibleRecoverButton = "Hidden";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, reality, 'Юридическое лицо' as 'type' from legal_entity where reality != 'Архив') Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, reality, 'Физическое лицо' from natural_person where reality != 'Архив') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListClient.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListClient[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Юр. лица")
            {
                VisibleRecoverButton = "Hidden";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_legal_entity,name_organization as 'name', phone_number, reality, 'Юридическое лицо' as 'type' from legal_entity where reality != 'Архив' ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListClient.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListClient[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Физ. лица")
            {
                VisibleRecoverButton = "Hidden";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic  as 'name', phone_number, reality, 'Физическое лицо' as type from natural_person  where reality != 'Архив' ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_natural_person"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListClient.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListClient[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Архив")
            {
                VisibleRecoverButton = "Visible";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, reality, 'Юридическое лицо' as 'type' from legal_entity where reality = 'Архив') Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, reality, 'Физическое лицо' from natural_person where reality = 'Архив') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListClient.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListClient[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Все")
            {
                VisibleRecoverButton = "Hidden";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, reality, 'Юридическое лицо' as 'type' from legal_entity) Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, reality, 'Физическое лицо' from natural_person) ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListClient.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListClient[i].num = i + 1;
                }
            }
        }

        #endregion

        #region Команда удаления клиента
        public ICommand DropClientCommand { get; }

        private bool CanDropClientCommandExecute(object p)
        {
            if ((SelectedClient > -1) && (SelectedVehicleProperty != "Архив"))
                return true;
            else return false;
        }

        private void OnDropClientCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (MainListClient[SelectedClient].type == "Юридическое лицо")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                    SqlConnection ThisConnection = new SqlConnection(connectionString);
                    ThisConnection.Open();
                    var command = ThisConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Drop_legal_entity";
                    command.Parameters.AddWithValue("@id_legal_entity", MainListClient[SelectedClient].id_legal_entity);
                    command.Parameters.AddWithValue("@reality", "Архив");
                    command.ExecuteNonQuery();
                    ThisConnection.Close();

                    FilterClientStatusCommand.Execute(null);
                }
                else
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                    SqlConnection ThisConnection = new SqlConnection(connectionString);
                    ThisConnection.Open();
                    var command = ThisConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Drop_natural_person";
                    command.Parameters.AddWithValue("@id_natural_person", MainListClient[SelectedClient].id_legal_entity);
                    command.Parameters.AddWithValue("@reality", "Архив");
                    command.ExecuteNonQuery();
                    ThisConnection.Close();

                    FilterClientStatusCommand.Execute(null);
                }
            }
        }

        #endregion

        #region Команда востановления удаленной записи
        public ICommand RepeatClientCommand { get; }

        private bool CanRepeatClientCommandExecute(object p) => SelectedClient >= 0;

        private void OnRepeatClientCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите восстановить данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (MainListClient[SelectedClient].type == "Юридическое лицо")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                    SqlConnection ThisConnection = new SqlConnection(connectionString);
                    ThisConnection.Open();
                    var command = ThisConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Drop_legal_entity";
                    command.Parameters.AddWithValue("@id_legal_entity", MainListClient[SelectedClient].id_legal_entity);
                    command.Parameters.AddWithValue("@reality", "Актуально");
                    command.ExecuteNonQuery();
                    ThisConnection.Close();

                    FilterClientStatusCommand.Execute(null);
                }
                else
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                    SqlConnection ThisConnection = new SqlConnection(connectionString);
                    ThisConnection.Open();
                    var command = ThisConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Drop_natural_person";
                    command.Parameters.AddWithValue("@id_natural_person", MainListClient[SelectedClient].id_legal_entity);
                    command.Parameters.AddWithValue("@reality", "Актуально");
                    command.ExecuteNonQuery();
                    ThisConnection.Close();

                    FilterClientStatusCommand.Execute(null);
                }
            }
        }

        #endregion

        #region Команда вызывающая окно "Добавить клиента"

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

            FilterClientStatusCommand.Execute(null);
        }

        #endregion

        #region Команда вызывающая окно "Редактирование информации о клиенте"

        public ICommand OpenEditClientWindowCommand { get; }

        private bool CanOpenEditClientWindowCommandExecute(object p)
        {
            if ((SelectedClient > -1) && (SelectedVehicleProperty != "Архив"))
                return true;
            else return false;
        }
        private void OnOpenEditClientWindowCommandExecuted(object p)
        {
            if (MainListClient[SelectedClient].type == "Физическое лицо")
            {
                ListClientProcedure.id_natural_person = MainListClient[SelectedClient].id_legal_entity;

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select * from [natural_person] where id_natural_person=" + ListClientProcedure.id_natural_person;
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                if (thisReader.HasRows)
                {
                    ListClientProcedure.surname = thisReader["surname"].ToString();
                    ListClientProcedure.name = thisReader["name"].ToString();
                    ListClientProcedure.patronymic = thisReader["patronymic"].ToString();
                    ListClientProcedure.gender = thisReader["gender"].ToString();
                    ListClientProcedure.birthday = Convert.ToDateTime(thisReader["birthday"].ToString());
                    ListClientProcedure.place_birthday = thisReader["place_birthday"].ToString();
                    ListClientProcedure.INN = thisReader["INN"].ToString();
                    ListClientProcedure.series_passport = thisReader["series_passport"].ToString();
                    ListClientProcedure.number_passport = thisReader["number_passport"].ToString();
                    ListClientProcedure.who_issued_passport = thisReader["who_issued_passport"].ToString();
                    ListClientProcedure.date_issued_passport = Convert.ToDateTime(thisReader["date_issued_passport"].ToString());
                    ListClientProcedure.number_card = thisReader["number_card"].ToString();
                    ListClientProcedure.validity_period_card = Convert.ToDateTime(thisReader["validity_period_card"].ToString());
                    ListClientProcedure.CVC2 = thisReader["CVC2"].ToString();
                    ListClientProcedure.registration = thisReader["registration"].ToString();
                    ListClientProcedure.actual_place_residence = thisReader["actual_place_residence"].ToString();
                    ListClientProcedure.phone_number = thisReader["phone_number"].ToString();
                    ListClientProcedure.email = thisReader["email"].ToString();
                }
                thisReader.Close();
                ThisConnection.Close();

                ListClientProcedure.EditOrAdd = "Редактирование информации о клиенте";

                EditClientWindow editClientWindow = new EditClientWindow();
                editClientWindow.ShowDialog();

                FilterClientStatusCommand.Execute(null);
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListClientWindowViewModel()
        {

            #region Команды

            FilterClientStatusCommand = new LamdaCommand(OnFilterClientStatusCommandExecuted, CanFilterClientStatusCommandExecute);

            OpenAddClientWindowCommand = new LamdaCommand(OnOpenAddClientWindowCommandExecuted, CanOpenAddClientWindowCommandExecute);

            OpenEditClientWindowCommand = new LamdaCommand(OnOpenEditClientWindowCommandExecuted, CanOpenEditClientWindowCommandExecute);

            DropClientCommand = new LamdaCommand(OnDropClientCommandExecuted, CanDropClientCommandExecute);

            RepeatClientCommand = new LamdaCommand(OnRepeatClientCommandExecuted, CanRepeatClientCommandExecute);

            #endregion

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_legal_entity,name_organization as 'name', phone_number, reality, 'Юридическое лицо' as 'type' from legal_entity where reality != 'Архив') Union (Select id_natural_person,surname + ' ' + [name] + ' ' + patronymic, phone_number, reality, 'Физическое лицо' from natural_person where reality != 'Архив') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListClient = dt.AsEnumerable().Select(se => new ListClient() { id_legal_entity = se.Field<int>("id_legal_entity"), name = se.Field<string>("name"), phone_number = se.Field<string>("phone_number"), reality = se.Field<string>("reality"), type = se.Field<string>("type")}).ToList();
            ThisConnection.Close();
            int k = MainListClient.Count;
            for (int i = 0; i < k; i++)
            {
                MainListClient[i].num = i + 1;
            }
        }

    }
}
