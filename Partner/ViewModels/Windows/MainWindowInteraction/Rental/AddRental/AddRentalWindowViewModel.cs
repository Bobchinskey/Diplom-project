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
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental.AddRental
{
    class AddRentalWindowViewModel : ViewModelBase
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

        #region Данные дополнительных услуг : AdditionalRateRental

        private DataTable _AdditionalRateRental;

        /// <summary>AdditionalRateRental</summary>
        public DataTable AdditionalRateRental
        {
            get => _AdditionalRateRental;
            set => Set(ref _AdditionalRateRental, value);
        }

        public DataTable AdditionalRateRentalDelivery = new DataTable("AdditionalRateRentalDelivery");
        DataColumn column;
        DataRow row;

        #endregion

        #region Выбранная дополнительная услуга : SelectAdditionalRateRental

        private int _SelectAdditionalRateRental = -1;

        /// <summary>SelectAdditionalRateRental</summary>
        public int SelectAdditionalRateRental
        {
            get => _SelectAdditionalRateRental;
            set => Set(ref _SelectAdditionalRateRental, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Вызывающая окно добавления дополнительных услуг : AddAdditionalRatesRentalWindow

        public ICommand OpenAddAdditionalRatesRentalCommand { get; }

        private bool CanOpenAddAdditionalRatesRentalCommandExecute(object p) => true;

        private void OnOpenAddAdditionalRatesRentalCommandExecuted(object p)
        {
            AddAdditionalRatesRentalWindow addAdditionalRatesRentalWindow = new AddAdditionalRatesRentalWindow();
            addAdditionalRatesRentalWindow.ShowDialog();

            if (DataAdditionalServices.Status == "Прошло успешно")
            {
                row = AdditionalRateRentalDelivery.NewRow();
                row["ID"] = DataAdditionalServices.IDAdditionalServices;
                row["NameRental"] = DataAdditionalServices.NameAdditionalServices;
                row["TypePayment"] = DataAdditionalServices.TypePayment;
                row["CostRental"] = DataAdditionalServices.CostAdditionalServices;
                AdditionalRateRentalDelivery.Rows.Add(row);

                AdditionalRateRental = AdditionalRateRentalDelivery;

                UpdateCostCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Удаления дополнительной услуги : DropAdditionalServicesCommand

        public ICommand DropAdditionalServicesCommand { get; }

        private bool CanDropAdditionalServicesCommandExecute(object p) => SelectAdditionalRateRental > -1;

        private void OnDropAdditionalServicesCommandExecuted(object p)
        {
            DataRow b = AdditionalRateRental.Rows[SelectAdditionalRateRental];
            AdditionalRateRentalDelivery.Rows.Remove(b);
            AdditionalRateRental = AdditionalRateRentalDelivery;

            UpdateCostCommand.Execute(null);
        }

        #endregion

        #region Команда Обновления итоговой стоимости : DropAdditionalServicesCommand

        public ICommand UpdateCostCommand { get; }

        private bool CanUpdateCostCommandExecute(object p) => true;

        private void OnUpdateCostCommandExecuted(object p)
        {
           if (AdditionalRateRental.Rows.Count > 0)
            {
                int CostSD = CostST;
                int k = 0;

                for (int i = 1; i <= AdditionalRateRental.Rows.Count; i++)
                {
                    if (Convert.ToString(AdditionalRateRental.Rows[k][2]) == "Разовый")
                    {
                        CostSD += Convert.ToInt32(AdditionalRateRental.Rows[k][3]);
                    }
                    else
                    {
                        CostSD = (Convert.ToInt32(AdditionalRateRental.Rows[k][3]) * day) + CostSD;
                    }
                    k++;
                }

                Cost = Convert.ToString(CostSD);
            }
            else
            {
                Cost = Convert.ToString(CostST);
            }
        }

        #endregion

        #region Команда Сохранить документ Word : ExportContractWordCommand

        public ICommand ExportContractWordCommand { get; }

        private bool CanExportContractWordCommandExecute(object p) => true;

        private void OnExportContractWordCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            if (DataStaticRental.Type == "Физическое лицо")
            {
                #region Процедуры добавления данных в БД

                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_rental";
                command.Parameters.AddWithValue("@id_vehicle", VehicleDataModel.id_vehicle);
                command.Parameters.AddWithValue("@start_date_rental", StartDateRental);
                command.Parameters.AddWithValue("@end_date_rental", EndDateRental);
                command.Parameters.AddWithValue("@cost", Cost);
                command.ExecuteNonQuery();

                int IdRental;

                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select Max(id_rental) as id_rental from rental";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                thisReader.Read();

                IdRental = Convert.ToInt32(thisReader["id_rental"].ToString());

                thisReader.Close();

                command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_contract_natural_person";
                command.Parameters.AddWithValue("@id_rental", IdRental);
                command.Parameters.AddWithValue("@id_natural_person", DataStaticRental.IDClient);
                command.ExecuteNonQuery();

                #endregion

                #region Экспорт в Word

                //Создаём новый Word.Application
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                //Загружаем документ
                Microsoft.Office.Interop.Word.Document doc = null;

                object fileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                             + "//Resources/Document/RentalContract.doc";
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
                object replaceWith = Convert.ToString(IdRental);
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

                if (EndDateRental.Month < 10)
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
                replaceWith = Convert.ToString(DateTime.Today.Year);
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
                replaceWith = "";
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                #region Получение данных о транспортном средстве из БД

                string MakeModel,VIN,StateNumber,TypeVehicle,YearManufacture,EngineNumber,ChassisNumber,BodyNumber,Color,SeriesPTS,NumberPTS;

                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select * from vehicle where id_vehicle = " + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
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

                string Registration, INN, SeriesPassport, NumberPassport, WhoIssuedPassport, DateIssuedPassport, Gender;


                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select * from natural_person where id_natural_person = " + DataStaticRental.IDClient;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();

                INN = thisReader["INN"].ToString();
                Registration = thisReader["registration"].ToString();
                SeriesPassport = thisReader["series_passport"].ToString();
                NumberPassport = thisReader["number_passport"].ToString();
                WhoIssuedPassport = thisReader["who_issued_passport"].ToString();
                DateIssuedPassport = thisReader["date_issued_passport"].ToString();
                Gender = thisReader["gender"].ToString();

                thisReader.Close();


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
                replaceWith = "";
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                findText = "<ClientInfo>";
                replaceWith = Client + ", адрес прописки: " + Registration + ", ИНН " + INN + ", Серия/Номер паспорта " + SeriesPassport + "/" + NumberPassport + ", Дата выдачи: " + DateIssuedPassport + ", Кем выдан: " + WhoIssuedPassport;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

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

                findText = "<Cost>";
                replaceWith = Cost;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                string EndDay, EndMonth;

                if (EndDateRental.Day < 10)
                {
                    EndDay = "0" + Convert.ToString(EndDateRental.Day);
                }
                else
                {
                    EndDay = Convert.ToString(EndDateRental.Day);
                }

                if (EndDateRental.Month < 10)
                {
                    EndMonth = "0" + Convert.ToString(EndDateRental.Month);
                }
                else
                {
                    EndMonth = Convert.ToString(EndDateRental.Month);
                }

                findText = "<EndDay>";
                replaceWith = EndDay;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                findText = "<EndMonth>";
                replaceWith = EndMonth;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                findText = "<EndYear>";
                replaceWith = Convert.ToString(EndDateRental.Year);
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                //Открываем документ для просмотра.
                app.Visible = true;

                #endregion
            }
            else
            {
                #region Процедуры добавления данных в БД

                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_rental";
                command.Parameters.AddWithValue("@id_vehicle", VehicleDataModel.id_vehicle);
                command.Parameters.AddWithValue("@start_date_rental", StartDateRental);
                command.Parameters.AddWithValue("@end_date_rental", EndDateRental);
                command.Parameters.AddWithValue("@cost", Cost);
                command.ExecuteNonQuery();

                int IdRental;

                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select Max(id_rental) as id_rental from rental";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                thisReader.Read();

                IdRental = Convert.ToInt32(thisReader["id_rental"].ToString());

                thisReader.Close();

                command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_contract_legal_entity";
                command.Parameters.AddWithValue("@id_rental", IdRental);
                command.Parameters.AddWithValue("@id_legal_entity", DataStaticRental.IDClient);
                command.ExecuteNonQuery();

                #endregion

                #region Экспорт в Word

                //Создаём новый Word.Application
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                //Загружаем документ
                Microsoft.Office.Interop.Word.Document doc = null;

                object fileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
             + "//Resources/Document/RentalContract.doc";
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
                object replaceWith = Convert.ToString(IdRental);
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

                if (EndDateRental.Month < 10)
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
                replaceWith = Convert.ToString(DateTime.Today.Year);
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
                replaceWith = "";
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                #region Получение данных о транспортном средстве из БД

                string MakeModel, VIN, StateNumber, TypeVehicle, YearManufacture, EngineNumber, ChassisNumber, BodyNumber, Color, SeriesPTS, NumberPTS;

                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select * from vehicle where id_vehicle = " + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
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

                string Registration, INN, SeriesPassport, NumberPassport, WhoIssuedPassport, DateIssuedPassport, Gender;


                thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select * from natural_person where id_natural_person = " + DataStaticRental.IDClient;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();

                INN = thisReader["INN"].ToString();
                Registration = thisReader["registration"].ToString();
                SeriesPassport = thisReader["series_passport"].ToString();
                NumberPassport = thisReader["number_passport"].ToString();
                WhoIssuedPassport = thisReader["who_issued_passport"].ToString();
                DateIssuedPassport = thisReader["date_issued_passport"].ToString();
                Gender = thisReader["gender"].ToString();

                thisReader.Close();


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
                replaceWith = "";
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                findText = "<ClientInfo>";
                replaceWith = Client + ", адрес прописки: " + Registration + ", ИНН " + INN + ", Серия/Номер паспорта " + SeriesPassport + "/" + NumberPassport + ", Дата выдачи: " + DateIssuedPassport + ", Кем выдан: " + WhoIssuedPassport;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

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

                findText = "<Cost>";
                replaceWith = Cost;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                string EndDay, EndMonth;

                if (EndDateRental.Day < 10)
                {
                    EndDay = "0" + Convert.ToString(EndDateRental.Day);
                }
                else
                {
                    EndDay = Convert.ToString(EndDateRental.Day);
                }

                if (EndDateRental.Month < 10)
                {
                    EndMonth = "0" + Convert.ToString(EndDateRental.Month);
                }
                else
                {
                    EndMonth = Convert.ToString(EndDateRental.Month);
                }

                findText = "<EndDay>";
                replaceWith = EndDay;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                findText = "<EndMonth>";
                replaceWith = EndMonth;
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                findText = "<EndYear>";
                replaceWith = Convert.ToString(EndDateRental.Year);
                replace = 2;

                app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
                ref replace, ref missing, ref missing, ref missing, ref missing);

                //Открываем документ для просмотра.
                app.Visible = true;

                #endregion

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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddRentalWindowViewModel()
        {
            #region Команды

            OpenAddAdditionalRatesRentalCommand = new LamdaCommand(OnOpenAddAdditionalRatesRentalCommandExecuted, CanOpenAddAdditionalRatesRentalCommandExecute);

            DropAdditionalServicesCommand = new LamdaCommand(OnDropAdditionalServicesCommandExecuted, CanDropAdditionalServicesCommandExecute);

            UpdateCostCommand = new LamdaCommand(OnUpdateCostCommandExecuted, CanUpdateCostCommandExecute);

            ExportContractWordCommand = new LamdaCommand(OnExportContractWordCommandExecuted, CanExportContractWordCommandExecute);

            #endregion

            #region Данные

            int CostRate = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();

            if (DataStaticRental.Type == "Физическое лицо")
            {
                thisCommand.CommandText = "Select [surname] + ' ' + [name] + ' ' + patronymic as name from natural_person  where id_natural_person=" + DataStaticRental.IDClient ;
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

            #region Создание колонок таблицы AdditionalRateRental

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NameRental";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TypePayment";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CostRental";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            #endregion

        }
    }
}
