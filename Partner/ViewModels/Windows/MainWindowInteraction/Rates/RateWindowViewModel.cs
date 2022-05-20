using Partner.Infrastructure.Commands;
using Partner.Models.Rate;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Rates;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rates
{
    class RateWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных тарифов : MainListRate

        private List<ListRateInformation> _MainListRate;

        public List<ListRateInformation> MainListRate
        {
            get => _MainListRate;
            set => Set(ref _MainListRate, value);
        }

        #endregion

        #region Выбранный элемент таблицы тарифы : SelectedRate

        private int _SelectedRate = 0;

        /// <summary>SelectedRate</summary>
        public int SelectedRate
        {
            get => _SelectedRate;
            set => Set(ref _SelectedRate, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда обновления окна : RepeatRateWindowCommand

        public ICommand RepeatRateWindowCommand { get; }

        private bool CanRepeatRateWindowCommandExecute(object p) => true;

        private void OnRepeatRateWindowCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select vehicle.id_vehicle,vehicle.make_model,rate.id_rate,rate.[1-3_day],rate.[4-9_day],rate.[10-29_day],rate.[30_day],rate.Deposit,rate.excess_mileage,rate.reality from rate,vehicle where vehicle.id_vehicle=rate.id_vehicle and rate.reality='Актуально'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListRate = dt.AsEnumerable().Select(se => new ListRateInformation() { id_vehicle = se.Field<int>("id_vehicle"), id_rate = se.Field<int>("id_rate"), Rate1_3 = se.Field<string>("1-3_day"), Rate4_9 = se.Field<string>("4-9_day"), Rate10_29 = se.Field<string>("10-29_day"), Rate30 = se.Field<string>("30_day"), Deposit = se.Field<string>("Deposit"), excess_mileage = se.Field<string>("excess_mileage"), reality = se.Field<string>("reality"), make_model = se.Field<string>("make_model") }).ToList();
            ThisConnection.Close();
            int k = MainListRate.Count;
            for (int i = 0; i < k; i++)
            {
                MainListRate[i].num = i + 1;
            }
        }

        #endregion

        #region Команда вызывающая окно "Редактирование тарифа" : OpenEditRateWindowCommand

        public ICommand OpenEditRateWindowCommand { get; }

        private bool CanOpenEditRateWindowCommandExecute(object p) => SelectedRate > -1;

        private void OnOpenEditRateWindowCommandExecuted(object p)
        {
            ListRateStatic.id_rate = MainListRate[SelectedRate].id_rate;
            ListRateStatic.id_vehicle = MainListRate[SelectedRate].id_vehicle;
            ListRateStatic.make_model = MainListRate[SelectedRate].make_model;
            ListRateStatic.Rate1_3 = MainListRate[SelectedRate].Rate1_3;
            ListRateStatic.Rate4_9 = MainListRate[SelectedRate].Rate4_9;
            ListRateStatic.Rate10_29 = MainListRate[SelectedRate].Rate10_29;
            ListRateStatic.Rate30 = MainListRate[SelectedRate].Rate30;
            ListRateStatic.Deposit = MainListRate[SelectedRate].Deposit;
            ListRateStatic.excess_mileage = MainListRate[SelectedRate].excess_mileage;

            EditRateWindow editRateWindow = new EditRateWindow();
            editRateWindow.ShowDialog();

            RepeatRateWindowCommand.Execute(null);
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
            column.ColumnName = "Автомобиль";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "1-3 дня, сутки";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "4-9 дня, сутки";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "10-29 дней, сутки";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "от 30 дней, сутки";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Залог";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Стоимость перепробега 1 км";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            #endregion

            #region Заполнение ExcelDataTable

            for (int i = 0; i < MainListRate.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["№"] = MainListRate[i].num;
                row["Автомобиль"] = MainListRate[i].make_model;
                row["1-3 дня, сутки"] = MainListRate[i].Rate1_3;
                row["4-9 дня, сутки"] = MainListRate[i].Rate4_9;
                row["10-29 дней, сутки"] = MainListRate[i].Rate10_29;
                row["от 30 дней, сутки"] = MainListRate[i].Rate30;
                row["Залог"] = MainListRate[i].Deposit;
                row["Стоимость перепробега 1 км"] = MainListRate[i].excess_mileage;
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
                    string b = Convert.ToString(ExcelDataTable.Rows[j][i]);
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

        public RateWindowViewModel()
        {
            #region Команды

            OpenEditRateWindowCommand = new LamdaCommand(OnOpenEditRateWindowCommandExecuted, CanOpenEditRateWindowCommandExecute);

            RepeatRateWindowCommand = new LamdaCommand(OnRepeatRateWindowCommandExecuted, CanRepeatRateWindowCommandExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select vehicle.id_vehicle,vehicle.make_model,rate.id_rate,rate.[1-3_day],rate.[4-9_day],rate.[10-29_day],rate.[30_day],rate.Deposit,rate.excess_mileage,rate.reality from rate,vehicle where vehicle.id_vehicle=rate.id_vehicle and rate.reality='Актуально' and vehicle.status='Свободен'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListRate = dt.AsEnumerable().Select(se => new ListRateInformation() { id_vehicle = se.Field<int>("id_vehicle"), id_rate = se.Field<int>("id_rate"), Rate1_3 = se.Field<string>("1-3_day"), Rate4_9 = se.Field<string>("4-9_day"), Rate10_29 = se.Field<string>("10-29_day"), Rate30 = se.Field<string>("30_day"), Deposit = se.Field<string>("Deposit"), excess_mileage = se.Field<string>("excess_mileage"), reality = se.Field<string>("reality"), make_model = se.Field<string>("make_model") }).ToList(); 
            ThisConnection.Close();
            int k = MainListRate.Count;
            for (int i = 0; i < k; i++)
            {
                MainListRate[i].num = i + 1;
            }

            #endregion
        }
    }
}
