using Partner.Data.Procedures;
using Partner.Data.UserData;
using Partner.Infrastructure.Commands;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
using Partner.Views.Views.Administration;
using Partner.Views.Views.Manager;
using Partner.Views.Windows.InformativeWindows;
using System.Configuration;
using System.Data.SqlClient;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Partner.ViewModels.Windows
{
    class AuthorizationWindowViewModel : ViewModelBase
    {

        #region Данные

        #region Login : TextBox

        private string _Login = "";

        /// <summary>Login</summary>
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        #endregion

        #region Password : PasswordBox

        private SecureString _SecurePassword;

        /// <summary>Password : PasswordBox</summary>
        public SecureString SecurePassword
        {
            get => _SecurePassword;
            set => Set(ref _SecurePassword, value);
        }
        public object DataContext { get; private set; }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда авторизации пользователя : AuthorizationModuleCommand

        public ICommand AuthorizationModuleCommand { get; }

        private bool CanAuthorizationModuleExecute(object p) => true;

        private void OnAuthorizationModuleExecuted(object p)
        {
            PasswordBox passwordBox = p as PasswordBox;
            string password = passwordBox.Password;
            if (Login != "")
            {
                if (password != "")
                {
                    UserData userData = new UserData(Login, password);
                    if (UserDataModel.id_user != -1)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                        SqlConnection ThisConnection = new SqlConnection(connectionString);
                        ThisConnection.Open();

                        SqlCommand thisCommand = ThisConnection.CreateCommand();
                        thisCommand.CommandText = "select Status from [user] where id_user=" + UserDataModel.id_user;
                        SqlDataReader thisReader = thisCommand.ExecuteReader();
                        thisReader.Read();

                        if (thisReader["Status"].ToString() == "Темная")
                        {
                            Application.Current.Resources["ForegroundMainText"] = new SolidColorBrush(Colors.White);
                            Application.Current.Resources["ForegroundAdditionalText"] = new SolidColorBrush(Colors.Black);
                            Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(Color.FromRgb(39,39,39));
                            Application.Current.Resources["MainColor"] = new SolidColorBrush(Color.FromRgb(109,109,109));
                        }
                        else if (thisReader["Status"].ToString() == "Красная")
                        {
                            Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.Red);
                            Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        }
                        else if (thisReader["Status"].ToString() == "Кораловая")
                        {
                            Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.LightCoral);
                            Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        }
                        else if (thisReader["Status"].ToString() == "Бирюзовая")
                        {
                            Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.PaleTurquoise);
                            Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        }
                        else if (thisReader["Status"].ToString() == "Лаймовая")
                        {
                            Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.Lime);
                            Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        }
                        else if (thisReader["Status"].ToString() == "Золотая")
                        {
                            Application.Current.Resources["MainColor"] = new SolidColorBrush(Colors.Gold);
                            Application.Current.Resources["UserInfoForeground"] = new SolidColorBrush(Colors.Black);
                        }

                        thisReader.Close();

                        ThisConnection.Close();

                        MessageBox.Show(UserDataModel.name + " " + UserDataModel.surname + "\nДобро пожаловать в программу 'ООО Партнер'\nУдачного рабочего дня!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        password = null;
                        HistoryUserInputBackUpProcedure mHistoryUserInputBackUpProcedure = new HistoryUserInputBackUpProcedure(UserDataModel.id_user);
                        if (UserDataModel.access_lavel == "Администратор")
                            UserDataModel.page = new MainPageAdministration();
                        else if (UserDataModel.access_lavel == "Менеджер")
                            UserDataModel.page = new MainPageManager();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();

                        foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                        {
                            if (window.DataContext == this)
                            {
                                window.Close();
                            }
                        }
                    }
                    else
                        MessageBox.Show("Неверное имя пользователя или пароль.");
                }
                else
                    MessageBox.Show("Введите пароль.");
            }
            else
                MessageBox.Show("Введите имя пользователя.");
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AuthorizationWindowViewModel()
        {
            #region Команды

            AuthorizationModuleCommand = new LamdaCommand(OnAuthorizationModuleExecuted, CanAuthorizationModuleExecute);

            #endregion
        }
    }
}
