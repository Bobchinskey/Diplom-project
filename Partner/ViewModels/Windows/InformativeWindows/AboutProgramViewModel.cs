using Partner.Infrastructure.Commands;
using Partner.ViewModels.Base;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.InformativeWindows
{
    class AboutProgramViewModel : ViewModelBase
    {

        #region Данные

        #region Дата последнего обновления приложения : LastUpdateProgram

        private string _LastUpdateProgram;

        /// <summary>IDAboutProgram</summary>
        public string LastUpdateProgram
        {
            get => _LastUpdateProgram;
            set => Set(ref _LastUpdateProgram, value);
        }

        #endregion

        #region Версия приложения : VersionProgram

        private string _VersionProgram;

        /// <summary>VersionProgram</summary>
        public string VersionProgram
        {
            get => _VersionProgram;
            set => Set(ref _VersionProgram, value);
        }

        #endregion

        #region Версия базы данных : VersionDatabase

        private string _VersionDatabase;

        /// <summary>VersionDatabase</summary>
        public string VersionDatabase
        {
            get => _VersionDatabase;
            set => Set(ref _VersionDatabase, value);
        }

        #endregion

        #region Сайт разработчика : DevelopersWebsite

        private string _DevelopersWebsite;

        /// <summary>DevelopersWebsite</summary>
        public string DevelopersWebsite
        {
            get => _DevelopersWebsite;
            set => Set(ref _DevelopersWebsite, value);
        }

        #endregion

        #region Страница программы : ProgramWebsite

        private string _ProgramWebsite;

        /// <summary>ProgramWebsite</summary>
        public string ProgramWebsite
        {
            get => _ProgramWebsite;
            set => Set(ref _ProgramWebsite, value);
        }

        #endregion

        #region Номер телефона разработчика : NumderPhone

        private string _NumderPhone;

        /// <summary>NumderPhone</summary>
        public string NumderPhone
        {
            get => _NumderPhone;
            set => Set(ref _NumderPhone, value);
        }

        #endregion

        #region Электронная почта разработчика : Email

        private string _Email;

        /// <summary>Email</summary>
        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда открытия сайта разработчика

        public ICommand OpenDevelopersWebsiteCommand { get; }

        private bool CanDevelopersWebsiteCommandExecute(object p) => true;

        private void OnDevelopersWebsiteCommandExecuted(object p)
        {
            Process.Start(DevelopersWebsite);
        }

        #endregion

        #region Команда открытия сайта приложения

        public ICommand OpenProgramWebsiteCommand { get; }

        private bool CanProgramWebsiteCommandExecute(object p) => true;

        private void OnProgramWebsiteCommandExecuted(object p)
        {
            Process.Start(ProgramWebsite);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AboutProgramViewModel()
        {

            #region Команды

            OpenDevelopersWebsiteCommand = new LamdaCommand(OnDevelopersWebsiteCommandExecuted, CanDevelopersWebsiteCommandExecute);

            OpenProgramWebsiteCommand = new LamdaCommand(OnProgramWebsiteCommandExecuted, CanProgramWebsiteCommandExecute);

            #endregion

            string connectionString = ConfigurationManager.ConnectionStrings["Program_Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select TOP 1 * from [about_program] ORDER BY [id_about_program] DESC";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            if (thisReader.HasRows)
            {
                LastUpdateProgram = thisReader["last_update_program"].ToString();
                VersionProgram = thisReader["version_program"].ToString();
                VersionDatabase = thisReader["version_database"].ToString();
                DevelopersWebsite = thisReader["developers_website"].ToString();
                ProgramWebsite = thisReader["program_website"].ToString();
                NumderPhone = thisReader["numder_phone"].ToString();
                Email = thisReader["email"].ToString();
            }
            thisReader.Close();
            ThisConnection.Close();
        }
    }
}
