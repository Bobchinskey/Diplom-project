using Partner.Infrastructure.Commands;
using Partner.Models.User;
using Partner.ViewModels.Base;
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
    internal class AddUserDataWindowViewModel : ViewModelBase
    {
        #region Данные 

        #region Заголовок окна : Title 

        private string _Title = UserDataModel.Title;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Фамилия пользователя : Surname 

        private string _Surname = "";

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя пользователя : Name 

        private string _Name = "";

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Отчество пользователя : Patronymic 

        private string _Patronymic = "";

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region Логин пользователя : Login 

        private string _Login = "";

        /// <summary>Login</summary>
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        #endregion

        #region Пароль пользователя : Password 

        private string _Password = "";

        /// <summary>Password</summary>
        public string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства Уровень доступа : SelectedAccesLavelProperty

        private string _SelectedAccesLavelProperty = "Менеджер";

        /// <summary>SelectedAccesLavelProperty</summary>
        public string SelectedAccesLavelProperty
        {
            get => _SelectedAccesLavelProperty;
            set => Set(ref _SelectedAccesLavelProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Менеджер", "Администратор" };

        /// <summary>status</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region Изменение уровня доступа : IsEnable 

        private bool _IsEnable = true;

        /// <summary>IsEnable</summary>
        public bool IsEnable
        {
            get => _IsEnable;
            set => Set(ref _IsEnable, value);
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавления данных пользователей : AddUserDataCommand 

        public ICommand AddUserDataCommand { get; }

        private bool CanAddUserDataCommandExecute(object p) => true;

        private void OnAddUserDataCommandExecuted(object p)
        {
            if (UserDataModel.Title == "Редактирование данных пользователя")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_user";
                command.Parameters.AddWithValue("@id_user", UserDataModel.Id);
                command.Parameters.AddWithValue("@login", Login);
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@surname", Surname);
                command.Parameters.AddWithValue("@name", Name);
                command.Parameters.AddWithValue("@patronymic", Patronymic);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае сохранены");
            }
            else if (UserDataModel.Title == "Добавление пользователя")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_user";
                command.Parameters.AddWithValue("@login", Login);
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@access_lavel", SelectedAccesLavelProperty);
                command.Parameters.AddWithValue("@surname", Surname);
                command.Parameters.AddWithValue("@name", Name);
                command.Parameters.AddWithValue("@patronymic", Patronymic);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае добавлены");
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

        public AddUserDataWindowViewModel()
        {
            #region Команды

            AddUserDataCommand = new LamdaCommand(OnAddUserDataCommandExecuted, CanAddUserDataCommandExecute);

            #endregion

            #region Данные

            if (UserDataModel.Title == "Редактирование данных пользователя")
            {
                IsEnable = false;

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from [user] where id_user = " + UserDataModel.Id;
                SqlDataReader thisReader = thisCommand.ExecuteReader();

                thisReader.Read();

                Login = thisReader["login"].ToString();
                Password = thisReader["password"].ToString();
                SelectedAccesLavelProperty = thisReader["access_lavel"].ToString();
                Surname = thisReader["surname"].ToString();
                Name = thisReader["name"].ToString();
                Patronymic = thisReader["patronymic"].ToString();
               
                thisReader.Close();

                ThisConnection.Close();
            }

            #endregion
        }
    }
}
