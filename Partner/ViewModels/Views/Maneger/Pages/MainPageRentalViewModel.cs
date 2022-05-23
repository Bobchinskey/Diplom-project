using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.Models.InformativeWindowModels;
using Partner.Models.PersonalData;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
using Partner.Views.Windows.MainWindowInteraction.Maintenances;
using Partner.Views.Windows.MainWindowInteraction.Rates;
using Partner.Views.Windows.MainWindowInteraction.Rental;
using Partner.Views.Windows.MainWindowInteraction.VehicleWindow;
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

namespace Partner.ViewModels.Views.Maneger.Pages
{
    class MainPageRentalViewModel : ViewModelBase
    {

        #region Данные

        #region Фамилия : Surname

        private string _Surname = UserDataModel.surname;

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя : Name

        private string _Name = UserDataModel.name;

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Отчество : Patronymic

        private string _Patronymic = UserDataModel.patronymic;

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region Уровень доступа : AccessLavel

        private string _AccessLavel = UserDataModel.access_lavel;

        /// <summary>AccessLavel</summary>
        public string AccessLavel
        {
            get => _AccessLavel;
            set => Set(ref _AccessLavel, value);
        }

        #endregion

        #region Изображение : UserDataModel.image

        private object _Image = UserDataModel.image;

        /// <summary>Image</summary>
        public object Image
        {
            get => _Image;
            set => Set(ref _Image, value);
        }

        #endregion

        #region Коллекция новостного блока : List<NewsModel> News

        private List<NewsModel> _News;

        public List<NewsModel> News
        {
            get => _News;
            set => Set(ref _News, value);
        }

        #endregion

        #region Коллекция блока важной информации : List<ImportantInformationModel> ImportantInformation

        private List<ImportantInformationModel> _ImportantInformation;

        public List<ImportantInformationModel> ImportantInformation
        {
            get => _ImportantInformation;
            set => Set(ref _ImportantInformation, value);
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда обновления окна : ReturnWindowCommand

        public ICommand ReturnWindowCommand { get; }

        private bool CanReturnWindowCommandExecute(object p) => true;

        private void OnReturnWindowCommandExecuted(object p)
        {
            DataTable news = new DataTable();
            DataTable importantInformation = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select Top(5) [id_news],[date_publication],[heading],[news] from [News] ORDER BY [id_news] desc";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            news.Load(thisReader);
            News = news.AsEnumerable().Select(se => new NewsModel() { id_news = se.Field<int>("id_news"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), news = se.Field<string>("News") }).ToList();
            thisCommand.CommandText = "select Top(5) [id_important_information],[date_publication],[heading],[important_information] from [Important_information] ORDER BY [id_important_information] desc";
            SqlDataReader thisReader2 = thisCommand.ExecuteReader();
            importantInformation.Load(thisReader2);
            ImportantInformation = importantInformation.AsEnumerable().Select(se => new ImportantInformationModel() { id_important_information = se.Field<int>("id_important_information"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), important_information = se.Field<string>("important_information") }).ToList();
            ThisConnection.Close();
        }

        #endregion

        #region Команда вызова окна "Техническое обслуживание" : OpeMaintenanceWindowCommand

        public ICommand OpeMaintenanceWindowCommand { get; }

        private bool CanOpenMaintenanceWindowCommandExecute(object p) => true;

        private void OnOpenMaintenanceWindowCommandExecuted(object p)
        {
            SelectVehicleMaintenanceWindow selectVehicleMaintenanceWindow = new SelectVehicleMaintenanceWindow();
            selectVehicleMaintenanceWindow.ShowDialog();

            ReturnWindowCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Список аренд" : OpeListRentalWindowCommand

        public ICommand OpeListRentalWindowCommand { get; }

        private bool CanOpeListRentalWindowCommandExecute(object p) => true;

        private void OnOpeListRentalWindowCommandExecuted(object p)
        {
            VehicleDataModel.EditOrAdd = "Аренда";

            SelectVehicleRentalWindow selectVehicleRentalWindow = new SelectVehicleRentalWindow();
            selectVehicleRentalWindow.ShowDialog();

            ReturnWindowCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Добавление транспортных средств" : OpenAddVehicleWindowCommand

        public ICommand OpenAddVehicleWindowCommand { get; }

        private bool CanOpenAddVehicleWindowCommandExecute(object p) => true;

        private void OnOpenAddVehicleWindowCommandExecuted(object p)
        {
            VehicleDataModel.MakeModel = "";
            VehicleDataModel.VIN = "";
            VehicleDataModel.StateNumber = "";
            VehicleDataModel.SelectStateNumber = "Легковой";
            VehicleDataModel.YearManufacture = "";
            VehicleDataModel.SelectCategory = "B";
            VehicleDataModel.NumberEngine = "";
            VehicleDataModel.ChassisNumber = "";
            VehicleDataModel.BodyNumber = "";
            VehicleDataModel.Color = "";
            VehicleDataModel.EnvironmentalClass = "";
            VehicleDataModel.PowerEngine = "";
            VehicleDataModel.DisplacementEngine = "";
            VehicleDataModel.TypeEngine = "";
            VehicleDataModel.PermittedMaximumMass = "";
            VehicleDataModel.WeightWithoutLoad = "";
            VehicleDataModel.SeriesPTS = "";
            VehicleDataModel.NumberPTS = "";
            VehicleDataModel.DateIssuedPTS = DateTime.Today;
            VehicleDataModel.WhoIssuedPTS = "";
            VehicleDataModel.SeriesSTS = "";
            VehicleDataModel.NumberSTS = "";
            VehicleDataModel.DateIssuedSTS = DateTime.Today;
            VehicleDataModel.WhoIssuedSTS = "";
            VehicleDataModel.Image = null;
            VehicleDataModel.EditOrAdd = "Добавить автомобиль";

            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
            addVehicleWindow.ShowDialog();

            ReturnWindowCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Тарифы" : OpenListRateWindowCommand

        public ICommand OpenListRateWindowCommand { get; }

        private bool CanOpenListRateWindowCommandExecute(object p) => true;

        private void OnOpenListRateWindowCommandExecuted(object p)
        {
            RateWindow rateWindow = new RateWindow();
            rateWindow.ShowDialog();

            ReturnWindowCommand.Execute(null);
        }

        #endregion

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

            ReturnWindowCommand.Execute(null);
        }

        #endregion

        #region Команда вызывающая окно "Добавить юридическое лицо" : OpenAddLegalEntityWindowCommand

        public ICommand OpenAddLegalEntityWindowCommand { get; }

        private bool CanOpenAddLegalEntityWindowCommandExecute(object p) => true;

        private void OnOpenAddLegalEntityWindowCommandExecuted(object p)
        {

            AddLegalEntityWindow addLegalEntityWindow = new AddLegalEntityWindow();
            addLegalEntityWindow.ShowDialog();

            ReturnWindowCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Бронирование" : OpenListBookingWindowCommand

        public ICommand OpenListBookingWindowCommand { get; }

        private bool CanOpenListBookingWindowCommandExecute(object p) => true;

        private void OnOpenListBookingWindowCommandExecuted(object p)
        {
            VehicleDataModel.EditOrAdd = "Бронирование";

            SelectVehicleRentalWindow selectVehicleRentalWindow = new SelectVehicleRentalWindow();
            selectVehicleRentalWindow.ShowDialog();

            ReturnWindowCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public MainPageRentalViewModel()
        {
            #region Команды

            OpenListBookingWindowCommand = new LamdaCommand(OnOpenListBookingWindowCommandExecuted, CanOpenListBookingWindowCommandExecute);

            OpeMaintenanceWindowCommand = new LamdaCommand(OnOpenMaintenanceWindowCommandExecuted, CanOpenMaintenanceWindowCommandExecute);

            ReturnWindowCommand = new LamdaCommand(OnReturnWindowCommandExecuted, CanReturnWindowCommandExecute);

            OpeListRentalWindowCommand = new LamdaCommand(OnOpeListRentalWindowCommandExecuted, CanOpeListRentalWindowCommandExecute);

            OpenAddVehicleWindowCommand = new LamdaCommand(OnOpenAddVehicleWindowCommandExecuted, CanOpenAddVehicleWindowCommandExecute);

            OpenListRateWindowCommand = new LamdaCommand(OnOpenListRateWindowCommandExecuted, CanOpenListRateWindowCommandExecute);

            OpenAddClientWindowCommand = new LamdaCommand(OnOpenAddClientWindowCommandExecuted, CanOpenAddClientWindowCommandExecute);

            OpenAddLegalEntityWindowCommand = new LamdaCommand(OnOpenAddLegalEntityWindowCommandExecuted, CanOpenAddLegalEntityWindowCommandExecute);

            #endregion

            #region Данные

            DataTable news = new DataTable();
            DataTable importantInformation = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select Top(5) [id_news],[date_publication],[heading],[news] from [News] ORDER BY [id_news] desc";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            news.Load(thisReader);
            News = news.AsEnumerable().Select(se => new NewsModel() { id_news = se.Field<int>("id_news"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), news = se.Field<string>("News") }).ToList();
            thisCommand.CommandText = "select Top(5) [id_important_information],[date_publication],[heading],[important_information] from [Important_information] ORDER BY [id_important_information] desc";
            SqlDataReader thisReader2 = thisCommand.ExecuteReader();
            importantInformation.Load(thisReader2);
            ImportantInformation = importantInformation.AsEnumerable().Select(se => new ImportantInformationModel() { id_important_information = se.Field<int>("id_important_information"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), important_information = se.Field<string>("important_information") }).ToList();
            ThisConnection.Close();

            #endregion
        }
    }
}
