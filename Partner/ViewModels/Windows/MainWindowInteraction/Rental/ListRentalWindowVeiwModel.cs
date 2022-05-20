using Partner.Infrastructure.Commands;
using Partner.Models.Rental;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Rental;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental
{
    class ListRentalWindowVeiwModel : ViewModelBase
    {

        #region Данные

        #region DataTable данных Аредн : MainListRental

        private DataTable _MainListRental;

        /// <summary>MainListRental</summary>
        public DataTable MainListRental
        {
            get => _MainListRental;
            set => Set(ref _MainListRental, value);
        }

        public DataTable MainListRentalDelivery = new DataTable("MainListRentalDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #region Выбранный элемент таблицы "Аренды" : SelectedRental

        private int _SelectedRental = 0;

        /// <summary>SelectedRental</summary>
        public int SelectedRental
        {
            get => _SelectedRental;
            set => Set(ref _SelectedRental, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства аренд : SelectedRentalProperty

        private string _SelectedRentalProperty = "Действующие";

        /// <summary>SelectedClientProperty</summary>
        public string SelectedRentalProperty
        {
            get => _SelectedRentalProperty;
            set => Set(ref _SelectedRentalProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Действующие", "Завершенные" };

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

        #region Команда Фильтрации данных "FilterRantalStatusCommand"

        public ICommand FilterRantalStatusCommand { get; }

        private bool CanFilterRantalStatusCommandExecute(object p) => true;

        private void OnFilterRantalStatusCommandExecuted(object p)
        {
            if (SelectedRentalProperty == "Действующие")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListRentalDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListRentalDelivery.Columns.Add(column);

                int k = MainListRentalDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListRentalDelivery.Rows[i]["num"] = i + 1;
                }

                MainListRental = MainListRentalDelivery;
            }
            else if (SelectedRentalProperty == "Завершенные")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition='Завершенна') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition='Завершенна') ORDER BY name";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                ThisConnection.Close();

                MainListRentalDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListRentalDelivery.Columns.Add(column);

                int k = MainListRentalDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListRentalDelivery.Rows[i]["num"] = i + 1;
                }

                MainListRental = MainListRentalDelivery;
            }
        }

        #endregion

        #region Команда Вызова окна "ListBookingWindow"

        public ICommand OpenListBookingWindowCommand { get; }

        private bool CanOpenListBookingWindowCommandExecute(object p) => true;

        private void OnOpenListBookingWindowCommandExecuted(object p)
        {
            ListBookingWindow listBookingWindow = new ListBookingWindow();
            listBookingWindow.ShowDialog();

            FilterRantalStatusCommand.Execute(null);
        }

        #endregion

        #region Команда Добавления аренды : OpenSelectedDateRentalWindowCommand

        public ICommand OpenSelectedDateRentalWindowCommand { get; }

        private bool CanOpenSelectedDateRentalWindowCommandExecute(object p) => true;

        private void OnOpenSelectedDateRentalWindowCommandExecuted(object p)
        {
            SelectedDateRentalWindow selectedDateRentalWindow = new SelectedDateRentalWindow();
            selectedDateRentalWindow.Show();

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #region Команда Редактирования аренды : OpenEditDateRentalWindowCommand

        public ICommand OpenEditDateRentalWindowCommand { get; }

        private bool CanOpenEditDateRentalWindowCommandExecute(object p)
        {
            if ((SelectedRental > -1) && (SelectedRentalProperty != "Завершенные"))
                return true;
            else
                return false;
        }

        private void OnOpenEditDateRentalWindowCommandExecuted(object p)
        {
            DataStaticRental.IDClient = Convert.ToInt32(MainListRentalDelivery.Rows[SelectedRental][2]);
            DataStaticRental.IDContract = Convert.ToInt32(MainListRentalDelivery.Rows[SelectedRental][0]);
            DataStaticRental.Type = Convert.ToString(MainListRentalDelivery.Rows[SelectedRental][7]);

            if (Convert.ToString(MainListRentalDelivery.Rows[SelectedRental][6]) == "В ожидание начала срока аренды")
            {
                DataStaticRental.Title = Convert.ToString(MainListRentalDelivery.Rows[SelectedRental][6]);
            }
            else if (Convert.ToString(MainListRentalDelivery.Rows[SelectedRental][6]) == "В аренде")
            {
                DataStaticRental.Title = Convert.ToString(MainListRentalDelivery.Rows[SelectedRental][6]);
            }

            EditRentalWindow editRentalWindow = new EditRentalWindow();
            editRentalWindow.Show();

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #region Команда Экспорт информации о арендах в Excel : ExcelCommand
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

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Статус";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            #endregion

            #region Заполнение ExcelDataTable

            for (int i = 0; i < MainListRental.Rows.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["№"] = MainListRental.Rows[i]["num"];
                row["Наименование"] = MainListRental.Rows[i]["name"]; 
                row["Тип"] = MainListRental.Rows[i]["type"];
                row["Дата начала"] = MainListRental.Rows[i]["start_date_rental"];
                row["Дата окончания"] = MainListRental.Rows[i]["end_date_rental"];
                row["Статус"] = MainListRental.Rows[i]["condition"];
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
                    {
                        b = Convert.ToString(ExcelDataTable.Rows[j][i]);
                    }
                    else
                    {
                        b = Convert.ToString(Convert.ToDateTime(ExcelDataTable.Rows[j][i]).ToShortDateString());
                    }
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    if (b != null)
                        myRange.Value2 = b;
                }
                sheet1.Columns.AutoFit();
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListRentalWindowVeiwModel()
        {
            #region Команды

            FilterRantalStatusCommand = new LamdaCommand(OnFilterRantalStatusCommandExecuted, CanFilterRantalStatusCommandExecute);

            OpenListBookingWindowCommand = new LamdaCommand(OnOpenListBookingWindowCommandExecuted, CanOpenListBookingWindowCommandExecute);

            OpenSelectedDateRentalWindowCommand = new LamdaCommand(OnOpenSelectedDateRentalWindowCommandExecuted, CanOpenSelectedDateRentalWindowCommandExecute);

            OpenEditDateRentalWindowCommand = new LamdaCommand(OnOpenEditDateRentalWindowCommandExecuted, CanOpenEditDateRentalWindowCommandExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            
            MainListRentalDelivery = dt;
            
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListRentalDelivery.Columns.Add(column);

            int k = MainListRentalDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListRentalDelivery.Rows[i]["num"] = i + 1;
            }

            MainListRental = MainListRentalDelivery;

            #endregion
        }
    }
}
