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
    internal class EditRentalWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Заголовок : Title

        private string _Title;

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

        #region Иконка кнопки : Icon

        private string _Icon;

        /// <summary>Icon</summary>

        public string Icon
        {
            get => _Icon;
            set => Set(ref _Icon, value);
        }

        #endregion

        #region Текст кнопки : ButtonText

        private string _ButtonText;

        /// <summary>ButtonText</summary>

        public string ButtonText
        {
            get => _ButtonText;
            set => Set(ref _ButtonText, value);
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

        private DateTime _StartDateRental;

        /// <summary>StartDateRental</summary>

        public DateTime StartDateRental
        {
            get => _StartDateRental;
            set => Set(ref _StartDateRental, value);
        }

        #endregion

        #region Дата окончания срока Аренды : EndDateRental

        private DateTime _EndDateRental;

        /// <summary>EndDateRental</summary>

        public DateTime EndDateRental
        {
            get => _EndDateRental;
            set => Set(ref _EndDateRental, value);
        }

        #endregion

        #region DataTable данных Дополнительных услуг : MainListAdditionalRates

        private DataTable _MainListAdditionalRates;

        /// <summary>MainListAdditionalRates</summary>
        public DataTable MainListAdditionalRates
        {
            get => _MainListAdditionalRates;
            set => Set(ref _MainListAdditionalRates, value);
        }

        public DataTable MainListAdditionalRatesDelivery = new DataTable("MainListAdditionalRatesDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        int Rental;

        #endregion

        #region Итоговая стоимость : Cost

        private string _Cost;

        /// <summary>Cost</summary>

        public string Cost
        {
            get => _Cost;
            set => Set(ref _Cost, value);
        }

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

        #region Команда изменения состояния аренды : EditRentalCommand

        public ICommand EditRentalCommand { get; }

        private bool CanEditRentalCommandExecute(object p) => true;

        private void OnEditRentalCommandExecuted(object p)
        {
            if (DataStaticRental.Title == "В ожидание начала срока аренды")
            {
                if (StartDateRental <= DateTime.Today.AddDays(2))
                {
                    if (MessageBox.Show("Убедитесь в правильносте заполненяемых документов.\nПродолжить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                        SqlConnection ThisConnection = new SqlConnection(connectionString);
                        ThisConnection.Open();
                        var command = ThisConnection.CreateCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Edit_rental";
                        command.Parameters.AddWithValue("@id_rental", Rental);
                        command.Parameters.AddWithValue("@condition", "В аренде");
                        command.ExecuteNonQuery();
                        ThisConnection.Close();

                        MessageBox.Show("Данные сохранены");

                        foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                        {
                            if (window.DataContext == this)
                            {
                                window.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно подтвердить начало аренды за 2 дня.", "Внимание");
                }
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_rental";
                command.Parameters.AddWithValue("@id_rental", Rental);
                command.Parameters.AddWithValue("@condition", "Завершенна");
                command.ExecuteNonQuery();
                ThisConnection.Close();

                if (DataStaticRental.Type == "Физическое лицо")
                {
                    //Создаём новый Word.Application
                    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                    //Загружаем документ
                    Microsoft.Office.Interop.Word.Document doc = null;

                    object fileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                                 + "//Resources/Document/ActReceivingTransfer.doc";
                    object falseValue = false;
                    object trueValue = true;
                    object missing = Type.Missing;

                    doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                    //Теперь у нас есть документ который мы будем менять.

                    //Очищаем параметры поиска
                    app.Selection.Find.ClearFormatting();
                    app.Selection.Find.Replacement.ClearFormatting();

                    //Задаём параметры замены и выполняем замену.
                    object findText = "<ID>";
                    object replaceWith = Convert.ToString(Rental);
                    object replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    string Day, Month;

                    if (StartDateRental.Day < 10)
                    {
                        Day = "0" + Convert.ToString(StartDateRental.Day);
                    }
                    else
                    {
                        Day = Convert.ToString(StartDateRental.Day);
                    }

                    if (StartDateRental.Month < 10)
                    {
                        Month = "0" + Convert.ToString(StartDateRental.Month);
                    }
                    else
                    {
                        Month = Convert.ToString(StartDateRental.Month);
                    }

                    findText = "<Day>";
                    replaceWith = Day;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<Month>";
                    replaceWith = Month;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<Year>";
                    replaceWith = Convert.ToString(StartDateRental.Year);
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<FIO>";
                    replaceWith = Client;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<NameOrganization>";
                    replaceWith = Client;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    #region Получение данных о транспортном средстве из БД

                    string MakeModel, VIN, StateNumber, TypeVehicle, YearManufacture, EngineNumber, ChassisNumber, BodyNumber, Color, SeriesPTS, NumberPTS;

                    ThisConnection.Open();
                    SqlCommand thisCommand = ThisConnection.CreateCommand();
                    thisCommand.CommandText = "select * from vehicle where id_vehicle = " + VehicleDataModel.id_vehicle;
                    SqlDataReader thisReader = thisCommand.ExecuteReader();
                    thisReader.Read();

                    VIN = thisReader["VIN"].ToString();
                    StateNumber = thisReader["state_number"].ToString();
                    TypeVehicle = thisReader["type_vehicle"].ToString();
                    YearManufacture = thisReader["year_manufacture"].ToString();
                    EngineNumber = thisReader["engine_number"].ToString();
                    ChassisNumber = thisReader["сhassis_number"].ToString();
                    BodyNumber = thisReader["body_number"].ToString();
                    Color = thisReader["color"].ToString();
                    SeriesPTS = thisReader["series_PTS"].ToString();
                    NumberPTS = thisReader["number_PTS"].ToString();
                    MakeModel = thisReader["make_model"].ToString();

                    thisReader.Close();

                    string Gender;

                    thisCommand = ThisConnection.CreateCommand();
                    thisCommand.CommandText = "select * from natural_person where id_natural_person = " + DataStaticRental.IDClient;
                    thisReader = thisCommand.ExecuteReader();
                    thisReader.Read();

                    Gender = thisReader["gender"].ToString();

                    thisReader.Close();

                    ThisConnection.Close();

                    #endregion

                    if (Gender == "Мужской")
                    {
                        Gender = "именуемый";
                    }
                    else
                    {
                        Gender = "именуемая";
                    }

                    findText = "<Declination>";
                    replaceWith = Gender;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<Post>";
                    replaceWith = "Физическое лицо";
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    #region Данные о транспортном средстве

                    findText = "<VIN>";
                    replaceWith = VIN;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<MakeModel>";
                    replaceWith = MakeModel;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<Type>";
                    replaceWith = TypeVehicle;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<StateNumber>";
                    replaceWith = StateNumber;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<YearManufacture>";
                    replaceWith = YearManufacture;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<NumberEngine>";
                    replaceWith = EngineNumber;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<NumberChassis>";
                    replaceWith = ChassisNumber;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<NumberBody>";
                    replaceWith = BodyNumber;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<Color>";
                    replaceWith = Color;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<SeriesPTS>";
                    replaceWith = SeriesPTS;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    findText = "<NumberPTS>";
                    replaceWith = NumberPTS;
                    replace = 2;

                    app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                    ref replace, ref missing, ref missing, ref missing, ref missing);

                    #endregion

                    //Открываем документ для просмотра.
                    app.Visible = true;
                }
                else
                {
                    DataStaticRental.Edit = "Выбор представителя для подписания акта выдачи";

                    SelectedRepresentativesOrganizationWindow selectedRepresentativesOrganizationWindow = new SelectedRepresentativesOrganizationWindow();
                    selectedRepresentativesOrganizationWindow.ShowDialog();

                    if (DataStaticRental.Edit == "Прошло успешно")
                    {

                        //Создаём новый Word.Application
                        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                        //Загружаем документ
                        Microsoft.Office.Interop.Word.Document doc = null;

                        object fileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                                     + "//Resources/Document/ActReceivingTransfer.doc";
                        object falseValue = false;
                        object trueValue = true;
                        object missing = Type.Missing;

                        doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing);

                        //Теперь у нас есть документ который мы будем менять.

                        //Очищаем параметры поиска
                        app.Selection.Find.ClearFormatting();
                        app.Selection.Find.Replacement.ClearFormatting();

                        //Задаём параметры замены и выполняем замену.
                        object findText = "<ID>";
                        object replaceWith = Convert.ToString(Rental);
                        object replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        string Day, Month;

                        if (StartDateRental.Day < 10)
                        {
                            Day = "0" + Convert.ToString(StartDateRental.Day);
                        }
                        else
                        {
                            Day = Convert.ToString(StartDateRental.Day);
                        }

                        if (StartDateRental.Month < 10)
                        {
                            Month = "0" + Convert.ToString(StartDateRental.Month);
                        }
                        else
                        {
                            Month = Convert.ToString(StartDateRental.Month);
                        }

                        findText = "<Day>";
                        replaceWith = Day;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<Month>";
                        replaceWith = Month;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<Year>";
                        replaceWith = Convert.ToString(StartDateRental.Year);
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);
                        findText = "<FIO>";
                        replaceWith = DataStaticRental.FIORepresentativesOrganization;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<NameOrganization>";
                        replaceWith = Client;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        #region Получение данных о транспортном средстве из БД

                        string MakeModel, VIN, StateNumber, TypeVehicle, YearManufacture, EngineNumber, ChassisNumber, BodyNumber, Color, SeriesPTS, NumberPTS;

                        ThisConnection.Open();
                        SqlCommand thisCommand = ThisConnection.CreateCommand();
                        thisCommand.CommandText = "select * from vehicle where id_vehicle = " + VehicleDataModel.id_vehicle;
                        SqlDataReader thisReader = thisCommand.ExecuteReader();
                        thisReader.Read();

                        VIN = thisReader["VIN"].ToString();
                        StateNumber = thisReader["state_number"].ToString();
                        TypeVehicle = thisReader["type_vehicle"].ToString();
                        YearManufacture = thisReader["year_manufacture"].ToString();
                        EngineNumber = thisReader["engine_number"].ToString();
                        ChassisNumber = thisReader["сhassis_number"].ToString();
                        BodyNumber = thisReader["body_number"].ToString();
                        Color = thisReader["color"].ToString();
                        SeriesPTS = thisReader["series_PTS"].ToString();
                        NumberPTS = thisReader["number_PTS"].ToString();
                        MakeModel = thisReader["make_model"].ToString();

                        thisReader.Close();

                        ThisConnection.Close();

                        #endregion

                        findText = "<Declination>";
                        replaceWith = "именуемый";
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<Post>";
                        replaceWith = DataStaticRental.PostRepresentativesOrganization;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        #region Данные о транспортном средстве

                        findText = "<VIN>";
                        replaceWith = VIN;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<MakeModel>";
                        replaceWith = MakeModel;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<Type>";
                        replaceWith = TypeVehicle;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<StateNumber>";
                        replaceWith = StateNumber;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<YearManufacture>";
                        replaceWith = YearManufacture;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<NumberEngine>";
                        replaceWith = EngineNumber;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<NumberChassis>";
                        replaceWith = ChassisNumber;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<NumberBody>";
                        replaceWith = BodyNumber;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<Color>";
                        replaceWith = Color;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<SeriesPTS>";
                        replaceWith = SeriesPTS;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        findText = "<NumberPTS>";
                        replaceWith = NumberPTS;
                        replace = 2;

                        app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                        ref replace, ref missing, ref missing, ref missing, ref missing);

                        #endregion

                        //Открываем документ для просмотра.
                        app.Visible = true;

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
                        MessageBox.Show("Необходимо выбрать представителя организации", "Ошибка");
                    }
                }
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public EditRentalWindowViewModel()
        {
            #region Команды

            EditRentalCommand = new LamdaCommand(OnEditRentalCommandExecuted, CanEditRentalCommandExecute);

            #endregion

            #region Данные

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand;
            SqlDataReader thisReader;

            if (DataStaticRental.Type == "Физическое лицо")
            {
                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select rental.id_rental,id_contract,cost,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition from contract_natural_person, rental,natural_person where id_contract= " + DataStaticRental.IDContract + " and natural_person.id_natural_person = contract_natural_person.id_natural_person and rental.id_rental = contract_natural_person.id_rental ";
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                Rental = Convert.ToInt32(thisReader["id_rental"].ToString());
                Client = thisReader["name"].ToString();
                StartDateRental = Convert.ToDateTime(thisReader["start_date_rental"].ToString());
                EndDateRental = Convert.ToDateTime(thisReader["end_date_rental"].ToString());
                Cost = thisReader["cost"].ToString();
                thisReader.Close();

                DataTable dt = new DataTable();

                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select additional_services_rental.id_additional_services, additional_services.name_additional_services,additional_services.cost_additional_services,additional_services.type_additional_services from contract_natural_person,additional_services_rental,additional_services where id_contract = " + DataStaticRental.IDContract + " and contract_natural_person.id_rental = additional_services_rental.id_rental and additional_services.id_additional_services = additional_services_rental.id_additional_services" ;
                thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);

                MainListAdditionalRatesDelivery = dt;
            }
            else
            {
                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select rental.id_rental,id_contract,cost,name_organization as name,rental.start_date_rental,rental.end_date_rental,rental.condition from contract_legal_entity, rental,legal_entity where id_contract= " + DataStaticRental.IDContract + " and legal_entity.id_legal_entity = contract_legal_entity.id_legal_entity and rental.id_rental = contract_legal_entity.id_rental ";
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                Rental = Convert.ToInt32(thisReader["id_rental"].ToString());
                Client = thisReader["name"].ToString();
                StartDateRental = Convert.ToDateTime(thisReader["start_date_rental"].ToString());
                EndDateRental = Convert.ToDateTime(thisReader["end_date_rental"].ToString());
                Cost = thisReader["cost"].ToString();
                thisReader.Close();

                DataTable dt = new DataTable();

                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select additional_services_rental.id_additional_services, additional_services.name_additional_services,additional_services.cost_additional_services,additional_services.type_additional_services from contract_legal_entity,additional_services_rental,additional_services where id_contract = " + DataStaticRental.IDContract + " and contract_legal_entity.id_rental = additional_services_rental.id_rental and additional_services.id_additional_services = additional_services_rental.id_additional_services";
                thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);

                MainListAdditionalRatesDelivery = dt;
            }

            thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select Deposit,make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle,rate where vehicle.id_vehicle = " + VehicleDataModel.id_vehicle + " and vehicle.id_vehicle = rate.id_vehicle ";
            thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Vehicle = thisReader["make_model"].ToString();
            Deposit = thisReader["Deposit"].ToString();
            thisReader.Close();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListAdditionalRatesDelivery.Columns.Add(column);

            int k = MainListAdditionalRatesDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListAdditionalRatesDelivery.Rows[i]["num"] = i + 1;
            }

            MainListAdditionalRates = MainListAdditionalRatesDelivery;

            ThisConnection.Close();

            if (DataStaticRental.Title == "В ожидание начала срока аренды")
            {
                ButtonText = "Начать аренду";
                Icon = "Save";
            }
            else
            {
                ButtonText = "Закрыть аренду";
                Icon = "FileWordOutline";
            }

            #endregion
        }
    }
}
