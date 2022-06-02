using Partner.Infrastructure.Commands;
using Partner.Models.Reports;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Reports
{
    internal class ReportWindowViewModel : ViewModelBase
    {
        #region Данные 

        #region Заголовок окна : Title

        private string _Title = ReportDataModel.Title;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region DataTable данных Отчета : MainListReport

        private DataTable _MainListReport;

        /// <summary>MainListReport</summary>
        public DataTable MainListReport
        {
            get => _MainListReport;
            set => Set(ref _MainListReport, value);
        }

        public DataTable MainListReportDelivery = new DataTable("MainListReportDelivery");

        #endregion

        #region Переменная column, Type

        DataColumn column;

        string Type;

        #endregion

        #region Выбранный элемент таблицы "Отчеты" : SelectedReport

        private int _SelectedReport = 0;

        /// <summary>SelectedReport</summary>
        public int SelectedReport
        {
            get => _SelectedReport;
            set => Set(ref _SelectedReport, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

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
            column.ColumnName = "Автомобиль";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = Type;
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            #endregion

            #region Заполнение ExcelDataTable

            for (int i = 0; i < MainListReport.Rows.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["№"] = MainListReport.Rows[i]["№"];
                row["Автомобиль"] = MainListReport.Rows[i]["Автомобиль"];
                row[Type] = MainListReport.Rows[i][Type];
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

        public ReportWindowViewModel()
        {
            #region Команды

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            #endregion

            #region Данные

            if (ReportDataModel.Title == "Отчет по количеству аренд")
            {
                Type = "Колличество аренд";
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "№";
                column.ReadOnly = false;
                column.Unique = false;
                MainListReportDelivery.Columns.Add(column);

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select make_model + ' (' + vehicle.state_number + ')'  as Автомобиль, COUNT(rental.id_vehicle) as '"+ Type + "' from rental, vehicle where start_date_rental>='"+ ReportDataModel.StartDate + "' and start_date_rental<='" + ReportDataModel.EndDate + "' and vehicle.id_vehicle=rental.id_vehicle and vehicle.status != 'Архив'  Group By make_model, vehicle.state_number";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                MainListReportDelivery.Load(thisReader);
                ThisConnection.Close();

                int k = MainListReportDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListReportDelivery.Rows[i]["№"] = i + 1;
                }

                MainListReport = MainListReportDelivery;
            }
            else if (ReportDataModel.Title == "Отчет по среднему чеку")
            {
                Type = "Средний чек";

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "№";
                column.ReadOnly = false;
                column.Unique = false;
                MainListReportDelivery.Columns.Add(column);

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = " Select make_model + ' (' + vehicle.state_number + ')'  as Автомобиль, AVG(cost) as '"+ Type +"' from rental, vehicle where start_date_rental>='"+ ReportDataModel.StartDate + "' and start_date_rental<='"+ ReportDataModel.EndDate + "' and vehicle.id_vehicle=rental.id_vehicle and vehicle.status != 'Архив'  Group By make_model, vehicle.state_number";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                MainListReportDelivery.Load(thisReader);
                ThisConnection.Close();

                int k = MainListReportDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListReportDelivery.Rows[i]["№"] = i + 1;
                }

                MainListReport = MainListReportDelivery;

            }
            else if (ReportDataModel.Title == "Отчет по прибыли от автомобилей")
            {
                Type = "Прибыль от автомобилей";

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "№";
                column.ReadOnly = false;
                column.Unique = false;
                MainListReportDelivery.Columns.Add(column);

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = " Select make_model + ' (' + vehicle.state_number + ')' as Автомобиль, Sum(cost) as '" + Type + "' from rental, vehicle where start_date_rental>='" + ReportDataModel.StartDate + "' and start_date_rental<='" + ReportDataModel.EndDate + "' and vehicle.id_vehicle=rental.id_vehicle and vehicle.status != 'Архив'  Group By make_model, vehicle.state_number";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                MainListReportDelivery.Load(thisReader);
                ThisConnection.Close();

                int k = MainListReportDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListReportDelivery.Rows[i]["№"] = i + 1;
                }

                MainListReport = MainListReportDelivery;
            }
            else if (ReportDataModel.Title == "Отчет по доходам от дополнительных услуг")
            {

            }
            else if (ReportDataModel.Title == "Отчет по техническому обслуживанию")
            {
                Type = "Колличество ТО за выбранный период";

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "№";
                column.ReadOnly = false;
                column.Unique = false;
                MainListReportDelivery.Columns.Add(column);

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select make_model + ' (' + vehicle.state_number + ')' as Автомобиль, COUNT(maintenance.id_vehicle) as '"+ Type +"' from maintenance, vehicle where start_date_maintenance>='" + ReportDataModel.StartDate + "' and start_date_maintenance<='" + ReportDataModel.EndDate + "' and vehicle.id_vehicle=maintenance.id_vehicle and vehicle.status != 'Архив'  Group By make_model, vehicle.state_number";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                MainListReportDelivery.Load(thisReader);
                ThisConnection.Close();

                int k = MainListReportDelivery.Rows.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListReportDelivery.Rows[i]["№"] = i + 1;
                }

                MainListReport = MainListReportDelivery;
            }

            #endregion
        }
    }
}
