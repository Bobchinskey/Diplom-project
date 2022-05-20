using Partner.Infrastructure.Commands;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Booking;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Booking
{
    internal class MainListBookingWindowViewModel : ViewModelBase
    {
        #region Данные

        #region DataTable данных Броней : MainListBooking

        private DataTable _MainListBooking;

        /// <summary>MainListBooking</summary>
        public DataTable MainListBooking
        {
            get => _MainListBooking;
            set => Set(ref _MainListBooking, value);
        }

        public DataTable MainListBookingDelivery = new DataTable("MainListBookingDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #region Выбранный элемент таблицы "Бронирование" : SelectedBooking

        private int _SelectedBooking = 0;

        /// <summary>SelectedBooking</summary>
        public int SelectedBooking
        {
            get => _SelectedBooking;
            set => Set(ref _SelectedBooking, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства Бронирования : SelectedBookingProperty

        private string _SelectedBookingProperty = "Открытое";

        /// <summary>SelectedClientProperty</summary>
        public string SelectedBookingProperty
        {
            get => _SelectedBookingProperty;
            set => Set(ref _SelectedBookingProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Открытое", "Закрытое","Отмененное" };

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

        #region Команда Фильтрация данных об бронировании Транспортных средств : FilterBookingListDataCommand

        public ICommand FilterBookingListDataCommand { get; }

        private bool CanFilterBookingListDataCommandExecute(object p) => true;

        private void OnFilterBookingListDataCommandExrcute(object p)
        {
            if (SelectedBookingProperty == "Открытое")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select booking_natural_person.start_date_booking,booking_natural_person.end_date_booking,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,'Физическое лицо' as type from booking_natural_person,natural_person,vehicle where booking_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle=booking_natural_person.id_vehicle and booking_natural_person.reality!='Отменено' and booking_natural_person.reality!='Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') Union (Select booking_legal_entity.start_date_booking, booking_legal_entity.end_date_booking, legal_entity.name_organization as name, 'Юридическое лицо' as type  from booking_legal_entity, legal_entity, vehicle where booking_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = booking_legal_entity.id_vehicle and booking_legal_entity.reality != 'Отменено' and booking_legal_entity.reality != 'Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();
                MainListBookingDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListBookingDelivery.Columns.Add(column);

                int k = MainListBookingDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListBookingDelivery.Rows[i]["num"] = i + 1;
                }

                MainListBooking = MainListBookingDelivery;

            }
            else if (SelectedBookingProperty == "Закрытое")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select booking_natural_person.start_date_booking,booking_natural_person.end_date_booking,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,'Физическое лицо' as type from booking_natural_person,natural_person,vehicle where booking_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle=booking_natural_person.id_vehicle and booking_natural_person.reality='Закрытоe' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') Union (Select booking_legal_entity.start_date_booking, booking_legal_entity.end_date_booking, legal_entity.name_organization as name, 'Юридическое лицо' as type  from booking_legal_entity, legal_entity, vehicle where booking_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = booking_legal_entity.id_vehicle and booking_legal_entity.reality = 'Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();
                MainListBookingDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListBookingDelivery.Columns.Add(column);

                int k = MainListBookingDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListBookingDelivery.Rows[i]["num"] = i + 1;
                }

                MainListBooking = MainListBookingDelivery;

            }
            else if (SelectedBookingProperty == "Отмененное")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select booking_natural_person.start_date_booking,booking_natural_person.end_date_booking,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,'Физическое лицо' as type from booking_natural_person,natural_person,vehicle where booking_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle=booking_natural_person.id_vehicle and booking_natural_person.reality='Отмененно' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') Union (Select booking_legal_entity.start_date_booking, booking_legal_entity.end_date_booking, legal_entity.name_organization as name, 'Юридическое лицо' as type  from booking_legal_entity, legal_entity, vehicle where booking_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = booking_legal_entity.id_vehicle and booking_legal_entity.reality = 'Отмененно' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();
                MainListBookingDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListBookingDelivery.Columns.Add(column);

                int k = MainListBookingDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListBookingDelivery.Rows[i]["num"] = i + 1;
                }

                MainListBooking = MainListBookingDelivery;

            }
        }

        #endregion

        #region Команда экспорт в Excel автомобилей : ExcelCommand
        public ICommand ExcelCommand { get; }

        private bool CanExcelCommandExecute(object p) => true;

        private void OnExcelCommandExecuted(object p)
        {
            System.Data.DataTable ExcelDataTable = new System.Data.DataTable("ExcelDataTable");
            DataColumn column;
            DataRow row;

            #region Создание колонок: ExcelDataTable

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "№";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Наименование";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Тип";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Дата начала";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Дата окончания";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            #endregion

            #region Заполнение ExcelDataTable

            for (int i = 0; i < MainListBooking.Rows.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["№"] = Convert.ToString(MainListBooking.Rows[i][4]);
                row["Наименование"] = Convert.ToString(MainListBooking.Rows[i][2]);
                row["Тип"] = Convert.ToString(MainListBooking.Rows[i][3]);
                row["Дата начала"] = Convert.ToString(MainListBooking.Rows[i][0]);
                row["Дата окончания"] = Convert.ToString(MainListBooking.Rows[i][1]);
                ExcelDataTable.Rows.Add(row);
            }

            #endregion

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
            for (int j = 0; j < ExcelDataTable.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange =
                    (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].font.bold = true;
                myRange.Value2 = ExcelDataTable.Columns[j].ColumnName;
            }
            for (int i = 0; i < ExcelDataTable.Columns.Count; i++)
            {
                for (int j = 0; j < ExcelDataTable.Rows.Count; j++)
                {
                    string b;
                    if ((i != 3) && (i != 4))
                        b = Convert.ToString(ExcelDataTable.Rows[j][i]);
                    else
                        b = Convert.ToString(Convert.ToDateTime(ExcelDataTable.Rows[j][i]).ToShortDateString());
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    if (b != null)
                        myRange.Value2 = b;
                }
                sheet1.Columns.AutoFit();
            }

            FilterBookingListDataCommand.Execute(null);
        }

        #endregion

        #region Команда Вызова окна "ListRentalInformationWindow" : OpenListRentalInformationWindowCommand
        public ICommand OpenListRentalInformationWindowCommand { get; }

        private bool CanOpenListRentalInformationWindowCommandExecute(object p) => true;

        private void OnOpenListRentalInformationWindowCommandExecuted(object p)
        {
            ListRentalInformationWindow listRentalInformationWindow = new ListRentalInformationWindow();
            listRentalInformationWindow.ShowDialog();

            FilterBookingListDataCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public MainListBookingWindowViewModel()
        {
            #region Команды

            FilterBookingListDataCommand = new LamdaCommand(OnFilterBookingListDataCommandExrcute, CanFilterBookingListDataCommandExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            OpenListRentalInformationWindowCommand = new LamdaCommand(OnOpenListRentalInformationWindowCommandExecuted, CanOpenListRentalInformationWindowCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select booking_natural_person.start_date_booking,booking_natural_person.end_date_booking,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,'Физическое лицо' as type from booking_natural_person,natural_person,vehicle where booking_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle=booking_natural_person.id_vehicle and booking_natural_person.reality!='Отменено' and booking_natural_person.reality!='Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') Union (Select booking_legal_entity.start_date_booking, booking_legal_entity.end_date_booking, legal_entity.name_organization as name, 'Юридическое лицо' as type  from booking_legal_entity, legal_entity, vehicle where booking_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = booking_legal_entity.id_vehicle and booking_legal_entity.reality != 'Отменено' and booking_legal_entity.reality != 'Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            MainListBookingDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListBookingDelivery.Columns.Add(column);

            int k = MainListBookingDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListBookingDelivery.Rows[i]["num"] = i + 1;
            }

            MainListBooking = MainListBookingDelivery;

            #endregion
        }
    }
}
