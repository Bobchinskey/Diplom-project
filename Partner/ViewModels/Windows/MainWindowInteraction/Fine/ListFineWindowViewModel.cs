using Partner.Infrastructure.Commands;
using Partner.Models.Fine;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Fine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Fine
{
    internal class ListFineWindowViewModel : ViewModelBase
    {
        #region Данные

        #region DataTable Данных штрафов : MainListFine

        private DataTable _MainListFine;

        /// <summary>MainListFine</summary>

        public DataTable MainListFine
        {
            get => _MainListFine;
            set => Set(ref _MainListFine,value);
        }

        public DataTable MainListFineDelivery = new DataTable("MainListFineDelivery");

        #endregion

        #region Переменные : column

        DataColumn column;

        #endregion

        #region Выбранный элемент таблицы MainListFine : SelectedFine

        private int _SelectedFine = 0;

        /// <summary>SelectedFine</summary>

        public int SelectedFine
        {
            get => _SelectedFine;
            set => Set(ref _SelectedFine, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства штрафов : SelectedFineProperty

        private string _SelectedFineProperty = "Не оплаченные";

        /// <summary>SelectedFineProperty</summary>

        public string SelectedFineProperty
        {
            get => _SelectedFineProperty;
            set => Set(ref _SelectedFineProperty, value);
        }

        #endregion

        #region Массив данных Combobox свойства штрафов:  status

        private string[] _status = { "Не оплаченные", "Оплаченные"};

        /// <summary>SelectedFineProperty</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда фильтрации данных : FilterListFine

        public ICommand FilterListFine { get; }

        private bool CanFilterListFineExecute(object p) => true;

        private void OnFilterListFineExecute(object p)
        {
            if (SelectedFineProperty == "Не оплаченные")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                SqlCommand sqlCommand = ThisConnection.CreateCommand();
                sqlCommand.CommandText = "(Select fine.id_fine,natural_person.name + ' ' + natural_person.surname + ' ' + natural_person.patronymic as name,fine.name_fine,cost_fine,fine.date_issued_fine,fine.date_payments_fine from vehicle,rental,fine,contract_natural_person,natural_person where fine.date_payments_fine is NULL and fine.id_rental = rental.id_rental and rental.id_vehicle = vehicle.id_vehicle and contract_natural_person.id_rental = rental.id_rental and natural_person.id_natural_person = contract_natural_person.id_natural_person and vehicle.id_vehicle = " + VehicleDataModel.id_vehicle + ") Union (Select  fine.id_fine, name_organization, fine.name_fine, cost_fine, fine.date_issued_fine, fine.date_payments_fine from vehicle, rental, fine, contract_legal_entity, legal_entity where fine.date_payments_fine is NULL and fine.id_rental = rental.id_rental and rental.id_vehicle = vehicle.id_vehicle and contract_legal_entity.id_rental = rental.id_rental and legal_entity.id_legal_entity = contract_legal_entity.id_legal_entity and vehicle.id_vehicle = " + VehicleDataModel.id_vehicle + ")";
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dt.Load(reader);

                ThisConnection.Close();

                MainListFineDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListFineDelivery.Columns.Add(column);

                int k = MainListFineDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListFineDelivery.Rows[i]["num"] = i + 1;
                }

                MainListFine = MainListFineDelivery;
            }
            else if (SelectedFineProperty == "Оплаченные")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                SqlCommand sqlCommand = ThisConnection.CreateCommand();
                sqlCommand.CommandText = "(Select fine.id_fine,natural_person.name + ' ' + natural_person.surname + ' ' + natural_person.patronymic as name,fine.name_fine,cost_fine,fine.date_issued_fine,fine.date_payments_fine from vehicle,rental,fine,contract_natural_person,natural_person where NOT fine.date_payments_fine is NULL and fine.id_rental = rental.id_rental and rental.id_vehicle = vehicle.id_vehicle and contract_natural_person.id_rental = rental.id_rental and natural_person.id_natural_person = contract_natural_person.id_natural_person and vehicle.id_vehicle = " + VehicleDataModel.id_vehicle + ") Union (Select  fine.id_fine, name_organization, fine.name_fine, cost_fine, fine.date_issued_fine, fine.date_payments_fine from vehicle, rental, fine, contract_legal_entity, legal_entity where NOT fine.date_payments_fine is NULL and fine.id_rental = rental.id_rental and rental.id_vehicle = vehicle.id_vehicle and contract_legal_entity.id_rental = rental.id_rental and legal_entity.id_legal_entity = contract_legal_entity.id_legal_entity and vehicle.id_vehicle = " + VehicleDataModel.id_vehicle + ")";
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dt.Load(reader);

                ThisConnection.Close();

                MainListFineDelivery = dt;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "num";
                column.ReadOnly = false;
                column.Unique = false;
                MainListFineDelivery.Columns.Add(column);

                int k = MainListFineDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListFineDelivery.Rows[i]["num"] = i + 1;
                }

                MainListFine = MainListFineDelivery;
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
            column.ColumnName = "Кому выписан штраф";
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
            column.ColumnName = "К оплате";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Дата получения";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Дата оплаты";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            #endregion

            #region Заполнение ExcelDataTable

            for (int i = 0; i < MainListFine.Rows.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["№"] = MainListFine.Rows[i]["num"];
                row["Кому выписан штраф"] = MainListFine.Rows[i]["name"];
                row["Наименование"] = MainListFine.Rows[i]["name_fine"];
                row["К оплате"] = MainListFine.Rows[i]["cost_fine"];
                row["Дата получения"] = MainListFine.Rows[i]["date_issued_fine"];
                row["Дата оплаты"] = MainListFine.Rows[i]["date_payments_fine"];
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

                    if ((i != 4) && (i != 5))
                    {
                        b = Convert.ToString(ExcelDataTable.Rows[j][i]);
                    }
                    else
                    {
                        if (i == 5)
                        {
                            if (Convert.ToString(ExcelDataTable.Rows[j][i]) == "")
                            {
                                b = "";
                            }
                            else
                            b = Convert.ToString(Convert.ToDateTime(ExcelDataTable.Rows[j][i]).ToShortDateString());
                        }
                        else 
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

        #region Команда открытия окна "Добавление штрафа" : OpenAddFineWindowCommand

        public ICommand OpenAddFineWindowCommand { get; }

        private bool CanOpenAddFineWindowCommandExecute(object p) => true;

        private void OnOpenAddFineWindowCommandExecuted(object p)
        {
            DataFineModel.Title = "Добавление штрафа";

            AddFineWindow addFineWindow = new AddFineWindow();
            addFineWindow.ShowDialog();

            FilterListFine.Execute(null);
        }

        #endregion

        #region Команда открытия окна "Редактирование штрафа" : OpenEditFineWindowCommand

        public ICommand OpenEditFineWindowCommand { get; }

        private bool CanOpenEditFineWindowCommandExecute(object p)
        {
            if ((SelectedFine > -1) && (SelectedFineProperty != "Оплаченные"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnOpenEditFineWindowCommandExecuted(object p)
        {
            DataFineModel.Title = "Редактирование штрафа";
            DataFineModel.IdFine = Convert.ToInt32(MainListFine.Rows[SelectedFine]["id_fine"]);

            AddFineWindow addFineWindow = new AddFineWindow();
            addFineWindow.ShowDialog();

            FilterListFine.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListFineWindowViewModel()
        {
            #region Команды

            OpenEditFineWindowCommand = new LamdaCommand(OnOpenEditFineWindowCommandExecuted, CanOpenEditFineWindowCommandExecute);

            OpenAddFineWindowCommand = new LamdaCommand(OnOpenAddFineWindowCommandExecuted, CanOpenAddFineWindowCommandExecute);

            FilterListFine = new LamdaCommand(OnFilterListFineExecute, CanFilterListFineExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            SqlCommand sqlCommand = ThisConnection.CreateCommand();
            sqlCommand.CommandText = "(Select fine.id_fine,natural_person.name + ' ' + natural_person.surname + ' ' + natural_person.patronymic as name,fine.name_fine,cost_fine,fine.date_issued_fine,fine.date_payments_fine from vehicle,rental,fine,contract_natural_person,natural_person where fine.date_payments_fine is NULL and fine.id_rental = rental.id_rental and rental.id_vehicle = vehicle.id_vehicle and contract_natural_person.id_rental = rental.id_rental and natural_person.id_natural_person = contract_natural_person.id_natural_person and vehicle.id_vehicle = "+ VehicleDataModel.id_vehicle + ") Union (Select  fine.id_fine, name_organization, fine.name_fine, cost_fine, fine.date_issued_fine, fine.date_payments_fine from vehicle, rental, fine, contract_legal_entity, legal_entity where fine.date_payments_fine is NULL and fine.id_rental = rental.id_rental and rental.id_vehicle = vehicle.id_vehicle and contract_legal_entity.id_rental = rental.id_rental and legal_entity.id_legal_entity = contract_legal_entity.id_legal_entity and vehicle.id_vehicle = " + VehicleDataModel.id_vehicle + ")";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            dt.Load(reader);

            ThisConnection.Close();

            MainListFineDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListFineDelivery.Columns.Add(column);

            int k = MainListFineDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListFineDelivery.Rows[i]["num"] = i + 1;
            }

            MainListFine = MainListFineDelivery;

            #endregion
        }
    }
}
