using Partner.Infrastructure.Commands;
using Partner.Models.Maintenances;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Maintenances.Additional;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Maintenances
{
    class ListMaintenanceWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных Технических обслуживаний : MainListMaintenance

        
        private DataTable _MainListMaintenance;

        /// <summary>MainListMaintenance</summary>
        public DataTable MainListMaintenance
        {
            get => _MainListMaintenance;
            set => Set(ref _MainListMaintenance, value);
        }

        public DataTable MainListMaintenanceDelivery = new DataTable("MainListMaintenanceDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда обновления окна "ListMaintenanceWindow" : ReturnListMaintenanceWindowCommand

        public ICommand ReturnListMaintenanceWindowCommand { get; }

        private bool CanReturnListMaintenanceWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnReturnListMaintenanceWindowCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_maintenance,name_maintenance,start_date_maintenance,end_date_maintenance from maintenance where id_vehicle = '" + VehicleDataModel.id_vehicle + "'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            MainListMaintenanceDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListMaintenanceDelivery.Columns.Add(column);

            int k = MainListMaintenanceDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListMaintenanceDelivery.Rows[i]["num"] = i + 1;
            }

            MainListMaintenance = MainListMaintenanceDelivery;

        }

        #endregion

        #region Команда вызова окна "AddMaintenanceWindow" : AddMaintenanceWindowCommand

        public ICommand AddMaintenanceWindowCommand { get; }

        private bool CanAddMaintenanceWindowCommandExecute(object p) => true;

        private void OnAddMaintenanceWindowCommandExecuted(object p)
        {
            AddMaintenanceWindow addMaintenanceWindow = new AddMaintenanceWindow();
            addMaintenanceWindow.ShowDialog();

            ReturnListMaintenanceWindowCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "AddMaintenanceWindow" : AddMaintenanceWindowCommand

        public ICommand EditMaintenanceWindowCommand { get; }

        private bool CanEditMaintenanceWindowCommandExecute(object p) => SelectedVehicle > -1;

        private void OnEditMaintenanceWindowCommandExecuted(object p)
        {
            EditMaintenancesData.IDMaintenances = Convert.ToInt32(MainListMaintenanceDelivery.Rows[SelectedVehicle][0]);

            EditMaintenanceWindow editMaintenanceWindow = new EditMaintenanceWindow();
            editMaintenanceWindow.ShowDialog();

            ReturnListMaintenanceWindowCommand.Execute(null);
        }

        #endregion

        #region Команда Удаления Технического обслуживания : DropMaintenanceCommand

        public ICommand DropMaintenanceCommand { get; }

        private bool CanDropMaintenanceCommandExecute(object p) => SelectedVehicle > -1;

        private void OnDropMaintenanceCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_maintenance";
                command.Parameters.AddWithValue("@id_maintenance", MainListMaintenance.Rows[SelectedVehicle][0]);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnListMaintenanceWindowCommand.Execute(null);
            }
        }

        #endregion

        #region Команда экспорт информации о страховках в Excel : ExcelCommand
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
            
            for (int i = 0; i < MainListMaintenance.Rows.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["Наименование"] = MainListMaintenance.Rows[i][1];
                row["Дата начала"] = MainListMaintenance.Rows[i][2];
                row["Дата окончания"] = MainListMaintenance.Rows[i][3];
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

                    if (i == 0)
                    {
                        b = Convert.ToString(ExcelDataTable.Rows[j][i]);
                    }
                    else
                    {
                        if (Convert.ToString(ExcelDataTable.Rows[j][i]) == "")
                            b = Convert.ToString(ExcelDataTable.Rows[j][i]);
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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListMaintenanceWindowViewModel()
        {
            #region Команды

            ReturnListMaintenanceWindowCommand = new LamdaCommand(OnReturnListMaintenanceWindowCommandExecuted, CanReturnListMaintenanceWindowCommandExecute);

            AddMaintenanceWindowCommand = new LamdaCommand(OnAddMaintenanceWindowCommandExecuted, CanAddMaintenanceWindowCommandExecute);

            DropMaintenanceCommand = new LamdaCommand(OnDropMaintenanceCommandExecuted, CanDropMaintenanceCommandExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            EditMaintenanceWindowCommand = new LamdaCommand(OnEditMaintenanceWindowCommandExecuted, CanEditMaintenanceWindowCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_maintenance,name_maintenance,start_date_maintenance,end_date_maintenance from maintenance where id_vehicle = '" + VehicleDataModel.id_vehicle + "'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();

            MainListMaintenanceDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListMaintenanceDelivery.Columns.Add(column);

            int k = MainListMaintenanceDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListMaintenanceDelivery.Rows[i]["num"] = i + 1;
            }

            MainListMaintenance = MainListMaintenanceDelivery;

            #endregion
        }
    }
}
