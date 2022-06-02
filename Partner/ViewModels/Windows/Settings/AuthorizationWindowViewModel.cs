using Partner.Infrastructure.Commands;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
using Partner.Views.Views.Manager.Pages;
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
using System.Windows.Media;

namespace Partner.ViewModels.Windows.Settings
{
    class AuthorizationWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Фамилия пользователя : Surname 

        private string _Surname;

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя пользователя : Name 

        private string _Name;

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Отчество пользователя : Patronymic 

        private string _Patronymic;

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region Тема приложения : StatusDesign 

        private string _StatusDesign;

        /// <summary>StatusDesign</summary>
        public string StatusDesign
        {
            get => _StatusDesign;
            set => Set(ref _StatusDesign, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства тип оплаты : SelectedApplicationDesignProperty

        private string _SelectedApplicationDesignProperty;

        /// <summary>SelectedApplicationDesignProperty</summary>
        public string SelectedApplicationDesignProperty
        {
            get => _SelectedApplicationDesignProperty;
            set => Set(ref _SelectedApplicationDesignProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Светлая", "Темная", "Красная", "Кораловая", "Бирюзовая", "Лаймовая", "Золотая" };

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

        #region Команда Сохранения данных : SaveDataUserSettingsCommand

        public ICommand SaveDataUserSettingsCommand { get; }

        private bool CanSaveDataUserSettingsCommandExecute(object p) => true;

        private void OnSaveDataUserSettingsCommandExecuted(object p)
        {
            if ((Surname == "") || (Name == "") || (Patronymic == ""))
            {
                MessageBox.Show("Заполните пустые поля","Ошибка");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                if (SelectedApplicationDesignProperty == StatusDesign)
                {
                    var command2 = ThisConnection.CreateCommand();
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Update_user_setting";
                    command2.Parameters.AddWithValue("@id_user", UserDataModel.id_user);
                    command2.Parameters.AddWithValue("@surname", Surname);
                    command2.Parameters.AddWithValue("@name", Name);
                    command2.Parameters.AddWithValue("@patronymic", Patronymic);
                    command2.Parameters.AddWithValue("@Status", SelectedApplicationDesignProperty);
                    command2.ExecuteNonQuery();
                }
                else
                {
                    var command2 = ThisConnection.CreateCommand();
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Update_user_setting";
                    command2.Parameters.AddWithValue("@id_user", UserDataModel.id_user);
                    command2.Parameters.AddWithValue("@surname", Surname);
                    command2.Parameters.AddWithValue("@name", Name);
                    command2.Parameters.AddWithValue("@patronymic", Patronymic);
                    command2.Parameters.AddWithValue("@Status", SelectedApplicationDesignProperty);
                    command2.ExecuteNonQuery();

                    if (SelectedApplicationDesignProperty == "Темная")
                    {
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Color.FromRgb(39, 39, 39));
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Color.FromRgb(109, 109, 109));
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.White);
                    }
                    else if (SelectedApplicationDesignProperty == "Светлая")
                    {
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.BlueViolet);
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.White);
                    }
                    else if (SelectedApplicationDesignProperty == "Красная")
                    {
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.Red);
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
                    }
                    else if (SelectedApplicationDesignProperty == "Кораловая")
                    {
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.LightCoral);
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
                    }
                    else if (SelectedApplicationDesignProperty == "Бирюзовая")
                    {
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.PaleTurquoise);
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
                    }
                    else if (SelectedApplicationDesignProperty == "Лаймовая")
                    {
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.Lime);
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
                    }
                    else if (SelectedApplicationDesignProperty == "Золотая")
                    {
                        Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.Gold);
                        Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.Black);
                        Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.White);
                        Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Colors.White);
                    }
                }

                UserDataModel.surname = Surname;
                UserDataModel.name = Name;
                UserDataModel.patronymic = Patronymic;

                Application.Current.Resources["Content"] = new MainPageRental();

                MessageBox.Show("Даннае обновлены");

                ThisConnection.Close();

                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
                    }
                }
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AuthorizationWindowViewModel()
        {
            #region Команды

            SaveDataUserSettingsCommand = new LamdaCommand(OnSaveDataUserSettingsCommandExecuted, CanSaveDataUserSettingsCommandExecute);

            #endregion

            #region Данные
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select surname,name,patronymic,Status from [user] where id_user = " + UserDataModel.id_user;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();

            Surname = thisReader["surname"].ToString();
            Name = thisReader["name"].ToString();
            Patronymic = thisReader["patronymic"].ToString();
            SelectedApplicationDesignProperty = thisReader["Status"].ToString();
            StatusDesign = thisReader["Status"].ToString();

            thisReader.Close();

            ThisConnection.Close();

            #endregion
        }
    }
}
