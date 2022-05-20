using Partner.Infrastructure.Commands;
using Partner.Models.Rate.AdditionalServices;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Rates.AdditionalServices;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rates.AdditionalServices
{
    class AdditionalServicesViewModel : ViewModelBase
    {
        #region Данные

        #region List данных дополнительных услуг : MainListAdditionalServices

        private List<MainListAdditionalServices> _MainListAdditionalServices;

        public List<MainListAdditionalServices> MainListAdditionalServices
        {
            get => _MainListAdditionalServices;
            set => Set(ref _MainListAdditionalServices, value);
        }

        #endregion

        #region Выбранный элемент таблицы клиентов : SelectedAdditionalServices

        private int _SelectedAdditionalServices = 0;

        /// <summary>SelectedAdditionalServices</summary>
        public int SelectedAdditionalServices
        {
            get => _SelectedAdditionalServices;
            set => Set(ref _SelectedAdditionalServices, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства дополнительных услуг : SelectedAdditionalServicesProperty

        private string _SelectedAdditionalServicesProperty = "Актуально";

        /// <summary>SelectedAdditionalServicesProperty</summary>
        public string SelectedAdditionalServicesProperty
        {
            get => _SelectedAdditionalServicesProperty;
            set => Set(ref _SelectedAdditionalServicesProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Актуально", "Архив", "Все" };

        /// <summary>status</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда фильтрации дополнительных услуг по их статусу : FilterAdditionalServicesStatusCommand

        public ICommand FilterAdditionalServicesStatusCommand { get; }

        private bool CanFilterAdditionalServicesStatusCommandExecute(object p) => true;

        private void OnFilterAdditionalServicesStatusCommandExecuted(object p)
        {
            if (SelectedAdditionalServicesProperty == "Актуально")
            {
                VisibleRecoverButton = "Hidden";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from additional_services where reality='Актуально'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListAdditionalServices = dt.AsEnumerable().Select(se => new MainListAdditionalServices() { id_additional_services = se.Field<int>("id_additional_services"), name_additional_services = se.Field<string>("name_additional_services"), cost_additional_services = se.Field<string>("cost_additional_services"), type_additional_services = se.Field<string>("type_additional_services"), reality = se.Field<string>("reality") }).ToList();
                ThisConnection.Close();
                int k = MainListAdditionalServices.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListAdditionalServices[i].num = i + 1;
                }
            }
            else if (SelectedAdditionalServicesProperty == "Архив")
            {
                VisibleRecoverButton = "Visible";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from additional_services where reality='Архив'";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListAdditionalServices = dt.AsEnumerable().Select(se => new MainListAdditionalServices() { id_additional_services = se.Field<int>("id_additional_services"), name_additional_services = se.Field<string>("name_additional_services"), cost_additional_services = se.Field<string>("cost_additional_services"), type_additional_services = se.Field<string>("type_additional_services"), reality = se.Field<string>("reality") }).ToList();
                ThisConnection.Close();
                int k = MainListAdditionalServices.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListAdditionalServices[i].num = i + 1;
                }
            }
            else if (SelectedAdditionalServicesProperty == "Все")
            {
                VisibleRecoverButton = "Hidden";

                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from additional_services";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListAdditionalServices = dt.AsEnumerable().Select(se => new MainListAdditionalServices() { id_additional_services = se.Field<int>("id_additional_services"), name_additional_services = se.Field<string>("name_additional_services"), cost_additional_services = se.Field<string>("cost_additional_services"), type_additional_services = se.Field<string>("type_additional_services"), reality = se.Field<string>("reality") }).ToList();
                ThisConnection.Close();
                int k = MainListAdditionalServices.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListAdditionalServices[i].num = i + 1;
                }
            }
        }

        #endregion

        #region Команда удаления дополнительных услуг : DropAdditionalServicesCommand
        public ICommand DropAdditionalServicesCommand { get; }

        private bool CanDropAdditionalServicesCommandExecute(object p)
        {
            if ((SelectedAdditionalServices > -1) && (SelectedAdditionalServicesProperty != "Архив") && (MainListAdditionalServices[SelectedAdditionalServices].reality != "Архив"))
                return true;
            else return false;
        }

        private void OnDropAdditionalServicesCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_additional_services";
                command.Parameters.AddWithValue("@id_additional_services", MainListAdditionalServices[SelectedAdditionalServices].id_additional_services);
                command.Parameters.AddWithValue("@reality", "Архив");
                command.ExecuteNonQuery();
                ThisConnection.Close();
                
                FilterAdditionalServicesStatusCommand.Execute(null);
            }
        }

        #endregion

        #region Команда востановление дополнительных услуг : RepeatAdditionalServicesCommand
        public ICommand RepeatAdditionalServicesCommand { get; }

        private bool CanRepeatAdditionalServicesCommandExecute(object p)
        {
            if ((SelectedAdditionalServices > -1) && (SelectedAdditionalServicesProperty == "Архив"))
                return true;
            else return false;
        }

        private void OnRepeatAdditionalServicesCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите востановить данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_additional_services";
                command.Parameters.AddWithValue("@id_additional_services", MainListAdditionalServices[SelectedAdditionalServices].id_additional_services);
                command.Parameters.AddWithValue("@reality", "Актуально");
                command.ExecuteNonQuery();
                ThisConnection.Close();

                FilterAdditionalServicesStatusCommand.Execute(null);
            }
        }

        #endregion

        #region Команда добавление дополнительных услуг : AddAdditionalServicesCommand
        public ICommand AddAdditionalServicesCommand { get; }

        private bool CanAddAdditionalServicesCommandExecute(object p) => true;

        private void OnAddAdditionalServicesCommandExecuted(object p)
        {
            AddListData.title = "Добавление дополнительной услуги";
            AddListData.name_additional_services = "";
            AddListData.type_additional_services = "";
            AddListData.cost_additional_services = "";

            AddAdditionalServicesWindow addAdditionalServicesWindow = new AddAdditionalServicesWindow();
            addAdditionalServicesWindow.ShowDialog();

            FilterAdditionalServicesStatusCommand.Execute(null);
        }

        /*------------------------------------------------------------------------------------------------*/

        #endregion Команды

        #region Команда редактирование дополнительных услуг : EditAdditionalServicesCommand
        public ICommand EditAdditionalServicesCommand { get; }

        private bool CanEditAdditionalServicesCommandExecute(object p) => SelectedAdditionalServices > -1;

        private void OnEditAdditionalServicesCommandExecuted(object p)
        {
            AddListData.title = "Редактирвание дополнительной услуги";
            AddListData.id_additional_services = MainListAdditionalServices[SelectedAdditionalServices].id_additional_services;
            AddListData.name_additional_services = MainListAdditionalServices[SelectedAdditionalServices].name_additional_services;
            AddListData.type_additional_services = MainListAdditionalServices[SelectedAdditionalServices].type_additional_services;
            AddListData.cost_additional_services = MainListAdditionalServices[SelectedAdditionalServices].cost_additional_services;

            AddAdditionalServicesWindow addAdditionalServicesWindow = new AddAdditionalServicesWindow();
            addAdditionalServicesWindow.ShowDialog();

            FilterAdditionalServicesStatusCommand.Execute(null);
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
            column.ColumnName = "Наименование дополнительной услуги";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Тип оплаты";
            column.ReadOnly = false;
            column.Unique = false;
            ExcelDataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Стоимость";
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

            for (int i = 0; i < MainListAdditionalServices.Count; i++)
            {
                row = ExcelDataTable.NewRow();
                row["№"] = MainListAdditionalServices[i].num;
                row["Наименование дополнительной услуги"] = MainListAdditionalServices[i].name_additional_services;
                row["Тип оплаты"] = MainListAdditionalServices[i].type_additional_services;
                row["Стоимость"] = MainListAdditionalServices[i].cost_additional_services;
                row["Статус"] = MainListAdditionalServices[i].reality;
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

        public AdditionalServicesViewModel()
        {
            #region Команды

            FilterAdditionalServicesStatusCommand = new LamdaCommand(OnFilterAdditionalServicesStatusCommandExecuted, CanFilterAdditionalServicesStatusCommandExecute);

            DropAdditionalServicesCommand = new LamdaCommand(OnDropAdditionalServicesCommandExecuted, CanDropAdditionalServicesCommandExecute);

            RepeatAdditionalServicesCommand = new LamdaCommand(OnRepeatAdditionalServicesCommandExecuted, CanRepeatAdditionalServicesCommandExecute);

            AddAdditionalServicesCommand = new LamdaCommand(OnAddAdditionalServicesCommandExecuted, CanAddAdditionalServicesCommandExecute);

            EditAdditionalServicesCommand = new LamdaCommand(OnEditAdditionalServicesCommandExecuted, CanEditAdditionalServicesCommandExecute);

            ExcelCommand = new LamdaCommand(OnExcelCommandExecuted, CanExcelCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select * from additional_services where reality='Актуально'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListAdditionalServices = dt.AsEnumerable().Select(se => new MainListAdditionalServices() { id_additional_services = se.Field<int>("id_additional_services"), name_additional_services = se.Field<string>("name_additional_services"), cost_additional_services = se.Field<string>("cost_additional_services"), type_additional_services = se.Field<string>("type_additional_services"), reality = se.Field<string>("reality") }).ToList();
            ThisConnection.Close();
            int k = MainListAdditionalServices.Count;
            for (int i = 0; i < k; i++)
            {
                MainListAdditionalServices[i].num = i + 1;
            }

            #endregion
        }
    }
}
