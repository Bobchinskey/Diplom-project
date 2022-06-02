using Partner.Infrastructure.Commands;
using Partner.Models.User;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Users;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Users
{
    internal class ListUsersWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных Технических обслуживаний : MainListMaintenance

        private DataTable _MainListUsers;

        /// <summary>MainListUsers</summary>
        public DataTable MainListUsers
        {
            get => _MainListUsers;
            set => Set(ref _MainListUsers, value);
        }

        public DataTable MainListUsersDelivery = new DataTable("MainListUsersDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #region Выбранный элемент таблицы пользователи : SelectedUser

        private int _SelectedUser = 0;

        /// <summary>SelectedUser</summary>
        public int SelectedUser
        {
            get => _SelectedUser;
            set => Set(ref _SelectedUser, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства пользователей : SelectedUserProperty

        private string _SelectedUserProperty = "Актуально";

        /// <summary>SelectedUserProperty</summary>
        public string SelectedUserProperty
        {
            get => _SelectedUserProperty;
            set => Set(ref _SelectedUserProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Актуально", "Архив", "Все" };

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

        #region Команда Фильтрации данный Пользователей : FilterDataCommand

        public ICommand FilterDataCommand { get; }

        private bool CanFilterDataCommandExecute(object p) => true;

        private void OnFilterDataCommandExecuted(object p)
        {
            if (SelectedUserProperty == "Актуально")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_user,surname+' '+ name + ' ' + patronymic as name, login,access_lavel,reality from [user] where reality!='Архив'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListUsersDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListUsersDelivery.Columns.Add(column);

                int k = MainListUsersDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListUsersDelivery.Rows[i]["num"] = i + 1;
                }

                MainListUsers = MainListUsersDelivery;
                VisibleRecoverButton = "Hidden";
            }
            else if (SelectedUserProperty == "Архив")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_user,surname+' '+ name + ' ' + patronymic as name, login,access_lavel,reality from [user] where reality='Архив'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListUsersDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListUsersDelivery.Columns.Add(column);

                int k = MainListUsersDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListUsersDelivery.Rows[i]["num"] = i + 1;
                }

                MainListUsers = MainListUsersDelivery;

                VisibleRecoverButton = "Visible";
            }
            else if (SelectedUserProperty == "Все")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_user,surname+' '+ name + ' ' + patronymic as name, login,access_lavel,reality from [user]";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListUsersDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListUsersDelivery.Columns.Add(column);

                int k = MainListUsersDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListUsersDelivery.Rows[i]["num"] = i + 1;
                }

                MainListUsers = MainListUsersDelivery;
                VisibleRecoverButton = "Hidden";
            }
        }

        #endregion

        #region Команда Удаления данных Пользователей : DropUserDataCommand

        public ICommand DropUserDataCommand { get; }

        private bool CanDropUserDataCommandExecute(object p)
        {
            if ((SelectedUser > -1) && (SelectedUserProperty == "Актуально"))
                return true;
            else return false;
        }

        private void OnDropUserDataCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Drop_user";
                command2.Parameters.AddWithValue("@id_user", Convert.ToString(Convert.ToInt32(MainListUsers.Rows[SelectedUser]["id_user"])));
                command2.Parameters.AddWithValue("@reality", "Архив");
                command2.ExecuteNonQuery();

                ThisConnection.Close();

                FilterDataCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Востановления данных Пользователей : RestoreUserDataCommand

        public ICommand RestoreUserDataCommand { get; }

        private bool CanRestoreUserDataCommandExecute(object p)
        {
            if ((SelectedUser > -1) && (VisibleRecoverButton == "Visible"))
                return true;
            else return false;
        }

        private void OnRestoreUserDataCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите востановить данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Drop_user";
                command2.Parameters.AddWithValue("@id_user", Convert.ToString(Convert.ToInt32(MainListUsers.Rows[SelectedUser]["id_user"])));
                command2.Parameters.AddWithValue("@reality", "Актуально");
                command2.ExecuteNonQuery();

                ThisConnection.Close();

                FilterDataCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Добавления данных пользователей : AddUserDataCommand 

        public ICommand AddUserDataCommand { get; }

        private bool CanAddUserDataCommandExecute(object p) => true;

        private void OnAddUserDataCommandExecuted(object p)
        {
            UserDataModel.Title = "Добавление пользователя";

            AddUserDataWindow addUserDataWindow = new AddUserDataWindow();
            addUserDataWindow.ShowDialog();

            FilterDataCommand.Execute(null);
        }

        #endregion

        #region Команда Редактирования данных пользователей : EditUserDataCommand 

        public ICommand EditUserDataCommand { get; }

        private bool CanEditUserDataCommandExecute(object p)
        {
            if ((SelectedUser > -1) && (SelectedUserProperty == "Актуально"))
                return true;
            else
                return false;
        }

        private void OnEditUserDataCommandExecuted(object p)
        {
            UserDataModel.Id = Convert.ToInt32(MainListUsers.Rows[SelectedUser]["id_user"]);
            UserDataModel.Title = "Редактирование данных пользователя";

            AddUserDataWindow addUserDataWindow = new AddUserDataWindow();
            addUserDataWindow.ShowDialog();

            FilterDataCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListUsersWindowViewModel()
        {
            #region Команды

            EditUserDataCommand = new LamdaCommand(OnEditUserDataCommandExecuted, CanEditUserDataCommandExecute);

            AddUserDataCommand = new LamdaCommand(OnAddUserDataCommandExecuted, CanAddUserDataCommandExecute);

            RestoreUserDataCommand = new LamdaCommand(OnRestoreUserDataCommandExecuted, CanRestoreUserDataCommandExecute);

            FilterDataCommand = new LamdaCommand(OnFilterDataCommandExecuted, CanFilterDataCommandExecute);

            DropUserDataCommand = new LamdaCommand(OnDropUserDataCommandExecuted, CanDropUserDataCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_user,surname+' '+ name + ' ' + patronymic as name, login,access_lavel,reality from [user] where reality!='Архив'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();

            MainListUsersDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListUsersDelivery.Columns.Add(column);

            int k = MainListUsersDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListUsersDelivery.Rows[i]["num"] = i + 1;
            }

            MainListUsers = MainListUsersDelivery;

            #endregion
        }
    }
}
