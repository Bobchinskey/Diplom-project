using Microsoft.Office.Interop.Excel;
using Partner.Data;
using Partner.Infrastructure.Commands;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.VehicleWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.VehicleWindow
{
    class ListVehicleWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных автомобилей : MainListVehicle

        private List<ListVehicle> _MainListVehicle;

        public List<ListVehicle> MainListVehicle
        {
            get => _MainListVehicle;
            set => Set(ref _MainListVehicle, value);
        }

        #endregion

        #region Выбранный элемент таблицы автомобилей : SelectedVehicle

        private int _SelectedVehicle = 0;

        /// <summary>SelectedVehicle</summary>
        public int SelectedVehicle
        {
            get => _SelectedVehicle;
            set => Set(ref _SelectedVehicle, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства автомобилей : SelectedVehicleProperty

        private string _SelectedVehicleProperty = "В автопарке";

        /// <summary>SelectedVehicleProperty</summary>
        public string SelectedVehicleProperty
        {
            get => _SelectedVehicleProperty;
            set => Set(ref _SelectedVehicleProperty, value);
        }

        #endregion

        #region Видимость кнопки "Востановить удаленныю запись" : VisibleRecoverButton

        private string _VisibleRecoverButton = "Hidden";

        /// <summary>VisibleRecoverButton</summary>
        public string VisibleRecoverButton
        {
            get => _VisibleRecoverButton;
            set => Set(ref _VisibleRecoverButton, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "В автопарке", "Архив", "Все" };

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

        #region Команда фильтрации автомобилей по их статусу : FilterVehicleStatusCommand

        public ICommand FilterVehicleStatusCommand { get; }

        private bool CanFilterVehicleStatusCommandExecute(object p) => true;

        private void OnFilterVehicleStatusCommandExecuted(object p)
        {
            if (SelectedVehicleProperty == "В автопарке")
            {
                VisibleRecoverButton = "Hidden";

                System.Data.DataTable dt = new System.Data.DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from vehicle where status!='Архив'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), MakeModel = se.Field<string>("make_model"), VIN = se.Field<string>("vin"), StateNumber = se.Field<string>("state_number"), SelectStateNumber = se.Field<string>("type_vehicle"), YearManufacture = se.Field<string>("year_manufacture"), SelectCategory = se.Field<string>("category"), NumberEngine = se.Field<string>("engine_number"), ChassisNumber = se.Field<string>("сhassis_number"), BodyNumber = se.Field<string>("body_number"), Color = se.Field<string>("color"), EnvironmentalClass = se.Field<string>("environmental_class"), PowerEngine = se.Field<string>("power_engine"), DisplacementEngine = se.Field<string>("displacement_engine"), TypeEngine = se.Field<string>("type_engine"), PermittedMaximumMass = se.Field<string>("permitted_maximum_mass"), WeightWithoutLoad = se.Field<string>("weight_without_load"), SeriesPTS = se.Field<string>("series_PTS"), NumberPTS = se.Field<string>("number_PTS"), WhoIssuedPTS = se.Field<string>("who_issued_PTS"), DateIssuedPTS = se.Field<DateTime>("date_issued_PTS"), SeriesSTS = se.Field<string>("series_STS"), NumberSTS = se.Field<string>("number_STS"), WhoIssuedSTS = se.Field<string>("who_issued_STS"), DateIssuedSTS = se.Field<DateTime>("date_issued_STS"), Image = se.Field<object>("image"), Status = se.Field<string>("status"), WhAddSystem = se.Field<int>("who_add_system"), DateAddSystem = se.Field<DateTime>("date_add_system") }).ToList();
                ThisConnection.Close();
                int k = MainListVehicle.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListVehicle[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Архив")
            {
                VisibleRecoverButton = "Visible";

                System.Data.DataTable dt = new System.Data.DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from vehicle where status='Архив'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), MakeModel = se.Field<string>("make_model"), VIN = se.Field<string>("vin"), StateNumber = se.Field<string>("state_number"), SelectStateNumber = se.Field<string>("type_vehicle"), YearManufacture = se.Field<string>("year_manufacture"), SelectCategory = se.Field<string>("category"), NumberEngine = se.Field<string>("engine_number"), ChassisNumber = se.Field<string>("сhassis_number"), BodyNumber = se.Field<string>("body_number"), Color = se.Field<string>("color"), EnvironmentalClass = se.Field<string>("environmental_class"), PowerEngine = se.Field<string>("power_engine"), DisplacementEngine = se.Field<string>("displacement_engine"), TypeEngine = se.Field<string>("type_engine"), PermittedMaximumMass = se.Field<string>("permitted_maximum_mass"), WeightWithoutLoad = se.Field<string>("weight_without_load"), SeriesPTS = se.Field<string>("series_PTS"), NumberPTS = se.Field<string>("number_PTS"), WhoIssuedPTS = se.Field<string>("who_issued_PTS"), DateIssuedPTS = se.Field<DateTime>("date_issued_PTS"), SeriesSTS = se.Field<string>("series_STS"), NumberSTS = se.Field<string>("number_STS"), WhoIssuedSTS = se.Field<string>("who_issued_STS"), DateIssuedSTS = se.Field<DateTime>("date_issued_STS"), Image = se.Field<object>("image"), Status = se.Field<string>("status"), WhAddSystem = se.Field<int>("who_add_system"), DateAddSystem = se.Field<DateTime>("date_add_system") }).ToList();
                ThisConnection.Close();
                int k = MainListVehicle.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListVehicle[i].num = i + 1;
                }
            }
            else if (SelectedVehicleProperty == "Все")
            {
                VisibleRecoverButton = "Hidden";

                System.Data.DataTable dt = new System.Data.DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from vehicle";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), MakeModel = se.Field<string>("make_model"), VIN = se.Field<string>("vin"), StateNumber = se.Field<string>("state_number"), SelectStateNumber = se.Field<string>("type_vehicle"), YearManufacture = se.Field<string>("year_manufacture"), SelectCategory = se.Field<string>("category"), NumberEngine = se.Field<string>("engine_number"), ChassisNumber = se.Field<string>("сhassis_number"), BodyNumber = se.Field<string>("body_number"), Color = se.Field<string>("color"), EnvironmentalClass = se.Field<string>("environmental_class"), PowerEngine = se.Field<string>("power_engine"), DisplacementEngine = se.Field<string>("displacement_engine"), TypeEngine = se.Field<string>("type_engine"), PermittedMaximumMass = se.Field<string>("permitted_maximum_mass"), WeightWithoutLoad = se.Field<string>("weight_without_load"), SeriesPTS = se.Field<string>("series_PTS"), NumberPTS = se.Field<string>("number_PTS"), WhoIssuedPTS = se.Field<string>("who_issued_PTS"), DateIssuedPTS = se.Field<DateTime>("date_issued_PTS"), SeriesSTS = se.Field<string>("series_STS"), NumberSTS = se.Field<string>("number_STS"), WhoIssuedSTS = se.Field<string>("who_issued_STS"), DateIssuedSTS = se.Field<DateTime>("date_issued_STS"), Image = se.Field<object>("image"), Status = se.Field<string>("status"), WhAddSystem = se.Field<int>("who_add_system"), DateAddSystem = se.Field<DateTime>("date_add_system") }).ToList();
                ThisConnection.Close();
                int k = MainListVehicle.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListVehicle[i].num = i + 1;
                }
            }
        }

        #endregion

        #region Команда вызывающая окно "Добавить автомобиль" : OpenAddVehicleWindowCommand

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

            FilterVehicleStatusCommand.Execute(null);
        }

        #endregion

        #region Команда вызывающая окно "Редактировать автомобиль" : OpenEditVehicleWindowCommand

        public ICommand OpenEditVehicleWindowCommand { get; }

        private bool CanOpenEditVehicleWindowCommandExecute(object p) => SelectedVehicle >= 0;

        private void OnOpenEditVehicleWindowCommandExecuted(object p)
        {
            VehicleDataModel.id_vehicle = MainListVehicle[SelectedVehicle].id_vehicle;
            VehicleDataModel.MakeModel = MainListVehicle[SelectedVehicle].MakeModel;
            VehicleDataModel.VIN = MainListVehicle[SelectedVehicle].VIN;
            VehicleDataModel.StateNumber = MainListVehicle[SelectedVehicle].StateNumber;
            VehicleDataModel.SelectStateNumber = MainListVehicle[SelectedVehicle].SelectStateNumber;
            VehicleDataModel.YearManufacture = MainListVehicle[SelectedVehicle].YearManufacture;
            VehicleDataModel.SelectCategory = MainListVehicle[SelectedVehicle].SelectCategory;
            VehicleDataModel.NumberEngine = MainListVehicle[SelectedVehicle].NumberEngine;
            VehicleDataModel.ChassisNumber = MainListVehicle[SelectedVehicle].ChassisNumber;
            VehicleDataModel.BodyNumber = MainListVehicle[SelectedVehicle].BodyNumber;
            VehicleDataModel.Color = MainListVehicle[SelectedVehicle].Color;
            VehicleDataModel.EnvironmentalClass = MainListVehicle[SelectedVehicle].EnvironmentalClass;
            VehicleDataModel.PowerEngine = MainListVehicle[SelectedVehicle].PowerEngine;
            VehicleDataModel.DisplacementEngine = MainListVehicle[SelectedVehicle].DisplacementEngine;
            VehicleDataModel.TypeEngine = MainListVehicle[SelectedVehicle].TypeEngine;
            VehicleDataModel.PermittedMaximumMass = MainListVehicle[SelectedVehicle].PermittedMaximumMass;
            VehicleDataModel.WeightWithoutLoad = MainListVehicle[SelectedVehicle].WeightWithoutLoad;
            VehicleDataModel.SeriesPTS = MainListVehicle[SelectedVehicle].SeriesPTS;
            VehicleDataModel.NumberPTS = MainListVehicle[SelectedVehicle].NumberPTS;
            VehicleDataModel.DateIssuedPTS = MainListVehicle[SelectedVehicle].DateIssuedPTS;
            VehicleDataModel.WhoIssuedPTS = MainListVehicle[SelectedVehicle].WhoIssuedPTS;
            VehicleDataModel.SeriesSTS = MainListVehicle[SelectedVehicle].SeriesSTS;
            VehicleDataModel.NumberSTS = MainListVehicle[SelectedVehicle].NumberSTS;
            VehicleDataModel.DateIssuedSTS = MainListVehicle[SelectedVehicle].DateIssuedSTS;
            VehicleDataModel.WhoIssuedSTS = MainListVehicle[SelectedVehicle].WhoIssuedSTS;
            VehicleDataModel.Status = MainListVehicle[SelectedVehicle].Status;
            VehicleDataModel.WhAddSystem = MainListVehicle[SelectedVehicle].WhAddSystem;
            VehicleDataModel.DateAddSystem = MainListVehicle[SelectedVehicle].DateAddSystem;
            VehicleDataModel.Image = null;
            VehicleDataModel.EditOrAdd = "Редактировать автомобиль";
            AddVehicleWindow addVehicleWindow = new AddVehicleWindow();

            addVehicleWindow.ShowDialog();

            FilterVehicleStatusCommand.Execute(null);
        }

        #endregion

        #region Команда удаления автомобиля : DropVehicleCommand
        public ICommand DropVehicleCommand { get; }

        private bool CanDropVehicleCommandExecute(object p) => SelectedVehicle >= 0;

        private void OnDropVehicleCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_vehicle";
                command.Parameters.AddWithValue("@id_vehicle", MainListVehicle[SelectedVehicle].id_vehicle);
                command.Parameters.AddWithValue("@status", "Архив");
                command.ExecuteNonQuery();
                ThisConnection.Close();

                FilterVehicleStatusCommand.Execute(null);
            }
        }

        #endregion

        #region Команда востановление удаленного автомобиля : RepeatVehicleCommand
        public ICommand RepeatVehicleCommand { get; }

        private bool CanRepeatVehicleCommandExecute(object p) => SelectedVehicle >= 0;

        private void OnRepeatVehicleCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_vehicle";
                command.Parameters.AddWithValue("@id_vehicle", MainListVehicle[SelectedVehicle].id_vehicle);
                command.Parameters.AddWithValue("@status", "Свободен");
                command.ExecuteNonQuery();
                ThisConnection.Close();

                FilterVehicleStatusCommand.Execute(null);
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
            column.ColumnName = "Наименование";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Гос. Номер";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Категория";
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

            for (int i = 0; i < MainListVehicle.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["Наименование"] = MainListVehicle[i].MakeModel;
                row["Гос. Номер"] = MainListVehicle[i].StateNumber;
                row["Категория"] = MainListVehicle[i].SelectCategory;
                row["Статус"] = MainListVehicle[i].Status;
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

        public ListVehicleWindowViewModel()
        {
            #region Команды

            FilterVehicleStatusCommand = new LamdaCommand(OnFilterVehicleStatusCommandExecuted, CanFilterVehicleStatusCommandExecute);

            OpenAddVehicleWindowCommand = new LamdaCommand(OnOpenAddVehicleWindowCommandExecuted, CanOpenAddVehicleWindowCommandExecute);

            OpenEditVehicleWindowCommand = new LamdaCommand(OnOpenEditVehicleWindowCommandExecuted, CanOpenEditVehicleWindowCommandExecute);

            DropVehicleCommand = new LamdaCommand(OnDropVehicleCommandExecuted, CanDropVehicleCommandExecute);

            RepeatVehicleCommand = new LamdaCommand(OnRepeatVehicleCommandExecuted, CanRepeatVehicleCommandExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            #endregion

            #region Данные

            System.Data.DataTable dt = new System.Data.DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select * from vehicle where status!='Архив'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListVehicle = dt.AsEnumerable().Select(se => new ListVehicle() { id_vehicle = se.Field<int>("id_vehicle"), MakeModel = se.Field<string>("make_model"), VIN = se.Field<string>("vin"), StateNumber = se.Field<string>("state_number"), SelectStateNumber = se.Field<string>("type_vehicle"), YearManufacture = se.Field<string>("year_manufacture"), SelectCategory = se.Field<string>("category"), NumberEngine = se.Field<string>("engine_number"), ChassisNumber = se.Field<string>("сhassis_number"), BodyNumber = se.Field<string>("body_number"), Color = se.Field<string>("color"), EnvironmentalClass = se.Field<string>("environmental_class"), PowerEngine = se.Field<string>("power_engine"), DisplacementEngine = se.Field<string>("displacement_engine"), TypeEngine = se.Field<string>("type_engine"), PermittedMaximumMass = se.Field<string>("permitted_maximum_mass"), WeightWithoutLoad = se.Field<string>("weight_without_load"), SeriesPTS = se.Field<string>("series_PTS"), NumberPTS = se.Field<string>("number_PTS"), WhoIssuedPTS = se.Field<string>("who_issued_PTS"), DateIssuedPTS = se.Field<DateTime>("date_issued_PTS"), SeriesSTS = se.Field<string>("series_STS"), NumberSTS = se.Field<string>("number_STS"), WhoIssuedSTS = se.Field<string>("who_issued_STS"), DateIssuedSTS = se.Field<DateTime>("date_issued_STS"), Image = se.Field<object>("image"), Status = se.Field<string>("status"), WhAddSystem = se.Field<int>("who_add_system"), DateAddSystem = se.Field<DateTime>("date_add_system") }).ToList();
            ThisConnection.Close();
            int k = MainListVehicle.Count;
            for (int i = 0; i < k; i++)
            {
                MainListVehicle[i].num = i + 1;
            }

            #endregion
        }
    }
}
