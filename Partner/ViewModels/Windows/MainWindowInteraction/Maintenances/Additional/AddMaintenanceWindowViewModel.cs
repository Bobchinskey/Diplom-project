using Partner.Infrastructure.Commands;
using Partner.Models.Maintenances;
using Partner.Models.PersonalData;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Maintenances.Additional
{
    class AddMaintenanceWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Наименование технического обслуживания : NameMaintenance 

        private string _NameMaintenance = "";

        /// <summary>NameMaintenance</summary>
        public string NameMaintenance
        {
            get => _NameMaintenance;
            set => Set(ref _NameMaintenance, value);
        }

        #endregion

        #region Дата начала технического обслуживания : StartDateMaintenance 

        private DateTime _StartDateMaintenance = DateTime.Today;

        /// <summary>StartDateMaintenance</summary>
        public DateTime StartDateMaintenance
        {
            get => _StartDateMaintenance;
            set => Set(ref _StartDateMaintenance, value);
        }

        #endregion

        #region Дата окончания технического обслуживания : EndDateMaintenance 

        private DateTime _EndDateMaintenance = DateTime.Today;

        /// <summary>EndDateMaintenance</summary>
        public DateTime EndDateMaintenance
        {
            get => _EndDateMaintenance;
            set => Set(ref _EndDateMaintenance, value);
        }

        #endregion

        #region Данные выполненных работ : DataCompletedWorks

        private DataTable _DataCompletedWorks;

        /// <summary>DataCompletedWorks</summary>
        public DataTable DataCompletedWorks
        {
            get => _DataCompletedWorks;
            set => Set(ref _DataCompletedWorks, value);
        }

        public DataTable DataCompletedWorksDelivery = new DataTable("DataCompletedWorksDelivery");

        #endregion

        #region Данные закупленных запчастей : DataSpareParts

        private DataTable _DataSpareParts;

        /// <summary>DataSpareParts</summary>
        public DataTable DataSpareParts
        {
            get => _DataSpareParts;
            set => Set(ref _DataSpareParts, value);
        }

        public DataTable DataSparePartsDelivery = new DataTable("DataSparePartsDelivery");

        #endregion

        #region Выбранный номер работ : SelectCompletedWorks

        private int _SelectCompletedWorks = -1;

        /// <summary>SelectCompletedWorks</summary>
        public int SelectCompletedWorks
        {
            get => _SelectCompletedWorks;
            set => Set(ref _SelectCompletedWorks, value);
        }

        #endregion

        #region Выбранный номер запчастей : SelectSpareParts 

        private int _SelectSpareParts = -1;

        /// <summary>SelectSpareParts</summary>
        public int SelectSpareParts
        {
            get => _SelectSpareParts;
            set => Set(ref _SelectSpareParts, value);
        }

        #endregion

        #region Переменные row, column

        DataColumn column;
        DataRow row;

        #endregion

        #region Завершено Техническое обслуживание : IsCheck 

        private bool _IsCheck = false;

        /// <summary>IsCheck</summary>
        public bool IsCheck
        {
            get => _IsCheck;
            set => Set(ref _IsCheck, value);
        }

        #endregion

        #region Видимость даты окончания технического обслуживания : Visible 

        private string _Visible = "Hidden";

        /// <summary>Visible</summary>
        public string Visible
        {
            get => _Visible;
            set => Set(ref _Visible, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавления выполненных работ : AddCompletedWorksCommand

        public ICommand AddCompletedWorksCommand { get; }

        private bool CanAddCompletedWorksCommandExecute(object p) => true;

        private void OnAddCompletedWorksCommandExecuted(object p)
        {
            CompletedWorks.Title = "Добавление выполненных работ";
            CompletedWorks.NameWork = "";
            CompletedWorks.Cost = "";

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                row = DataCompletedWorksDelivery.NewRow();
                row["Name"] = CompletedWorks.NameWork;
                row["Cost"] = CompletedWorks.Cost;
                DataCompletedWorksDelivery.Rows.Add(row);
            }

            DataCompletedWorks = DataCompletedWorksDelivery;
        }

        #endregion

        #region Команда Редактирования выполненных работ : EditCompletedWorksCommand

        public ICommand EditCompletedWorksCommand { get; }

        private bool CanEditCompletedWorksCommandExecute(object p) => SelectCompletedWorks > -1;

        private void OnEditCompletedWorksCommandExecuted(object p)
        {
            CompletedWorks.Title = "Редактирование выполненных работ";
            CompletedWorks.NameWork = Convert.ToString(DataCompletedWorks.Rows[SelectCompletedWorks]["Name"]);
            CompletedWorks.Cost = Convert.ToString(DataCompletedWorks.Rows[SelectCompletedWorks]["Cost"]);

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                DataCompletedWorksDelivery.Rows[SelectCompletedWorks]["Name"] = CompletedWorks.NameWork;
                DataCompletedWorksDelivery.Rows[SelectCompletedWorks]["Cost"] = CompletedWorks.Cost;
            }

            DataCompletedWorks = DataCompletedWorksDelivery;
        }

        #endregion

        #region Команда Удаления выполненных работ : DropCompletedWorksCommand

        public ICommand DropCompletedWorksCommand { get; }

        private bool CanDropCompletedWorksCommandExecute(object p) => SelectCompletedWorks > -1;

        private void OnDropCompletedWorksCommandExecuted(object p)
        {
            DataRow b = DataCompletedWorks.Rows[SelectCompletedWorks];
            DataCompletedWorksDelivery.Rows.Remove(b);
            DataCompletedWorks = DataCompletedWorksDelivery;
        }

        #endregion

        #region Команда Добавления закупленных запчастей : AddDataSparePartsCommand
        public ICommand AddDataSparePartsCommand { get; }

        private bool CanAddDataSparePartsCommandExecute(object p) => true;

        private void OnAddDataSparePartsCommandExecuted(object p)
        {
            CompletedWorks.Title = "Добавление запчастей";
            CompletedWorks.NameWork = "";
            CompletedWorks.Cost = "";

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                row = DataSparePartsDelivery.NewRow();
                row["Name"] = CompletedWorks.NameWork;
                row["Cost"] = CompletedWorks.Cost;
                DataSparePartsDelivery.Rows.Add(row);
            }

            DataSpareParts = DataSparePartsDelivery;
        }

        #endregion

        #region Команда Редактирования закупленных запчастей : EditDataSparePartsCommand

        public ICommand EditDataSparePartsCommand { get; }

        private bool CanEditDataSparePartsCommandExecute(object p) => SelectSpareParts > -1;

        private void OnEditDataSparePartsCommandExecuted(object p)
        {
            CompletedWorks.Title = "Редактирование запчастей";
            CompletedWorks.NameWork = Convert.ToString(DataSpareParts.Rows[SelectSpareParts]["Name"]);
            CompletedWorks.Cost = Convert.ToString(DataSpareParts.Rows[SelectSpareParts]["Cost"]);

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                DataSparePartsDelivery.Rows[SelectSpareParts]["Name"] = CompletedWorks.NameWork;
                DataSparePartsDelivery.Rows[SelectSpareParts]["Cost"] = CompletedWorks.Cost;
            }

            DataSpareParts = DataSparePartsDelivery;
        }

        #endregion

        #region Команда Удаления закупленных запчастей : DropDataSparePartsCommand

        public ICommand DropDataSparePartsCommand { get; }

        private bool CanDropDataSparePartsCommandExecute(object p) => SelectSpareParts > -1;

        private void OnDropDataSparePartsCommandExecuted(object p)
        {
            DataRow b = DataSpareParts.Rows[SelectSpareParts];
            DataSparePartsDelivery.Rows.Remove(b);
            DataSpareParts = DataSparePartsDelivery;
        }

        #endregion

        #region Команда Отображения даты окончания технического обслуживания : VisibleCommand

        public ICommand VisibleCommand { get; }

        private bool CanVisibleCommandExecute(object p) => true;

        private void OnVisibleCommandExecuted(object p)
        {
            if (IsCheck == true)
                Visible = "Visible";
            else
                Visible = "Hidden";
        }

        #endregion

        #region Команда Добавления Технического обслуживания : AddMaintenanceCommand

        public ICommand AddMaintenanceCommand { get; }

        private bool CanAddMaintenanceCommandExecute(object p) => NameMaintenance != "";

        private void OnAddMaintenanceCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            var command = ThisConnection.CreateCommand();
            if (IsCheck == true)
            {
                if (EndDateMaintenance > StartDateMaintenance)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Add_maintenance";
                    command.Parameters.AddWithValue("@id_vehicle", VehicleDataModel.id_vehicle);
                    command.Parameters.AddWithValue("@name_maintenance", NameMaintenance);
                    command.Parameters.AddWithValue("@start_date_maintenance", StartDateMaintenance);
                    command.Parameters.AddWithValue("@end_date_maintenance", EndDateMaintenance);
                    command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                    command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                    command.ExecuteNonQuery();

                    ThisConnection.Close();

                    int IdMaintenance;

                    ThisConnection.Open();
                    SqlCommand thisCommand = ThisConnection.CreateCommand();
                    thisCommand.CommandText = "select max(id_maintenance) as id_maintenance from maintenance";
                    SqlDataReader thisReader = thisCommand.ExecuteReader();
                    thisReader.Read();

                    IdMaintenance = Convert.ToInt32(thisReader["id_maintenance"].ToString());

                    thisReader.Close();
                    ThisConnection.Close();

                    ThisConnection.Open();

                    if (DataCompletedWorksDelivery.Rows.Count >= 1)
                    {
                        for (int i = 1; i <= DataCompletedWorksDelivery.Rows.Count; i++)
                        {
                            var command2 = ThisConnection.CreateCommand();
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.CommandText = "Add_work";
                            command2.Parameters.AddWithValue("@id_maintenance", IdMaintenance);
                            command2.Parameters.AddWithValue("@name_work", Convert.ToString(DataCompletedWorksDelivery.Rows[i - 1][0]));
                            command2.Parameters.AddWithValue("@cost_work", Convert.ToString(DataCompletedWorksDelivery.Rows[i - 1][1]));
                            command2.ExecuteNonQuery();
                        }
                    }

                    if (DataSparePartsDelivery.Rows.Count >= 1)
                    {
                        for (int i = 1; i <= DataSparePartsDelivery.Rows.Count; i++)
                        {
                            var command2 = ThisConnection.CreateCommand();
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.CommandText = "Add_spare_part";
                            command2.Parameters.AddWithValue("@id_maintenance", IdMaintenance);
                            command2.Parameters.AddWithValue("@name_spare_part", Convert.ToString(DataSparePartsDelivery.Rows[i - 1][0]));
                            command2.Parameters.AddWithValue("@cost_spare_part", Convert.ToString(DataSparePartsDelivery.Rows[i - 1][1]));
                            command2.ExecuteNonQuery();
                        }
                    }

                    ThisConnection.Close();

                    MessageBox.Show("Даннае добавлены");

                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.DataContext == this)
                        {
                            window.Close();
                        }
                    }
                }
                else MessageBox.Show("Даты технического обслуживания указаны неверно");
            }
            else
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_NULL_maintenance";
                command.Parameters.AddWithValue("@id_vehicle", VehicleDataModel.id_vehicle);
                command.Parameters.AddWithValue("@name_maintenance", NameMaintenance);
                command.Parameters.AddWithValue("@start_date_maintenance", StartDateMaintenance);
                command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                command.ExecuteNonQuery();

                ThisConnection.Close();

                int IdMaintenance;

                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "select max(id_maintenance) as id_maintenance from maintenance";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                thisReader.Read();

                IdMaintenance = Convert.ToInt32(thisReader["id_maintenance"].ToString());

                thisReader.Close();
                ThisConnection.Close();

                ThisConnection.Open();

                if (DataCompletedWorksDelivery.Rows.Count >= 1)
                {
                    for (int i = 1; i <= DataCompletedWorksDelivery.Rows.Count; i++)
                    {
                        var command2 = ThisConnection.CreateCommand();
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.CommandText = "Add_work";
                        command2.Parameters.AddWithValue("@id_maintenance", IdMaintenance);
                        command2.Parameters.AddWithValue("@name_work", Convert.ToString(DataCompletedWorksDelivery.Rows[i - 1][0]));
                        command2.Parameters.AddWithValue("@cost_work", Convert.ToString(DataCompletedWorksDelivery.Rows[i - 1][1]));
                        command2.ExecuteNonQuery();
                    }
                }

                if (DataSparePartsDelivery.Rows.Count >= 1)
                {
                    for (int i = 1; i <= DataSparePartsDelivery.Rows.Count; i++)
                    {
                        var command2 = ThisConnection.CreateCommand();
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.CommandText = "Add_spare_part";
                        command2.Parameters.AddWithValue("@id_maintenance", IdMaintenance);
                        command2.Parameters.AddWithValue("@name_spare_part", Convert.ToString(DataSparePartsDelivery.Rows[i - 1][0]));
                        command2.Parameters.AddWithValue("@cost_spare_part", Convert.ToString(DataSparePartsDelivery.Rows[i - 1][1]));
                        command2.ExecuteNonQuery();
                    }
                }

                ThisConnection.Close();

                MessageBox.Show("Даннае добавлены");

                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
                    }
                }
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddMaintenanceWindowViewModel()
        {
            #region Команды

            AddCompletedWorksCommand = new LamdaCommand(OnAddCompletedWorksCommandExecuted, CanAddCompletedWorksCommandExecute);

            EditCompletedWorksCommand = new LamdaCommand(OnEditCompletedWorksCommandExecuted, CanEditCompletedWorksCommandExecute);

            DropCompletedWorksCommand = new LamdaCommand(OnDropCompletedWorksCommandExecuted, CanDropCompletedWorksCommandExecute);

            AddDataSparePartsCommand = new LamdaCommand(OnAddDataSparePartsCommandExecuted, CanAddDataSparePartsCommandExecute);

            EditDataSparePartsCommand = new LamdaCommand(OnEditDataSparePartsCommandExecuted, CanEditDataSparePartsCommandExecute);

            DropDataSparePartsCommand = new LamdaCommand(OnDropDataSparePartsCommandExecuted, CanDropDataSparePartsCommandExecute);

            AddMaintenanceCommand = new LamdaCommand(OnAddMaintenanceCommandExecuted, CanAddMaintenanceCommandExecute);

            VisibleCommand = new LamdaCommand(OnVisibleCommandExecuted, CanVisibleCommandExecute);

            #endregion

            #region Создание колонок таблиц доставки

            #region Создание колонок таблицы DataCompletedWorksDelivery

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = false;
            column.Unique = false;
            DataCompletedWorksDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cost";
            column.ReadOnly = false;
            column.Unique = false;
            DataCompletedWorksDelivery.Columns.Add(column);

            #endregion

            #region Создание колонок таблицы DataSparePartsDelivery

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = false;
            column.Unique = false;
            DataSparePartsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cost";
            column.ReadOnly = false;
            column.Unique = false;
            DataSparePartsDelivery.Columns.Add(column);

            #endregion

            #endregion
        }
    }
}
