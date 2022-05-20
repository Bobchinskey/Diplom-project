using Partner.Infrastructure.Commands;
using Partner.Models.Maintenances;
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
    internal class EditMaintenanceWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Наименование технического обслуживания : NameMaintenance 

        private string _NameMaintenance;

        /// <summary>NameMaintenance</summary>
        public string NameMaintenance
        {
            get => _NameMaintenance;
            set => Set(ref _NameMaintenance, value);
        }

        #endregion

        #region Дата начала технического обслуживания : StartDateMaintenance 

        private DateTime _StartDateMaintenance;

        /// <summary>StartDateMaintenance</summary>
        public DateTime StartDateMaintenance
        {
            get => _StartDateMaintenance;
            set => Set(ref _StartDateMaintenance, value);
        }

        #endregion

        #region Дата окончания технического обслуживания : EndDateMaintenance 

        private DateTime _EndDateMaintenance;

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

        #endregion

        #region Данные закупленных запчастей : DataSpareParts

        private DataTable _DataSpareParts;

        /// <summary>DataSpareParts</summary>
        public DataTable DataSpareParts
        {
            get => _DataSpareParts;
            set => Set(ref _DataSpareParts, value);
        }

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

        #region Команда Обновления данных : UpdateCommand

        public ICommand UpdateCommand { get; }

        private bool CanUpdateCommandExecute(object p) => true;

        private void OnUpdateCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_work,name_work,cost_work from work,[maintenance] where maintenance.id_maintenance= work.id_maintenance and work.id_maintenance = " + EditMaintenancesData.IDMaintenances;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            DataCompletedWorks = dt;

            DataTable dt2 = new DataTable();

            thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_spare_part,name_spare_part,cost_spare_part from spare_part,[maintenance] where maintenance.id_maintenance= spare_part.id_maintenance and spare_part.id_maintenance = " + EditMaintenancesData.IDMaintenances;
            thisReader = thisCommand.ExecuteReader();
            dt2.Load(thisReader);
            DataSpareParts = dt2;

            ThisConnection.Close();
        }

        #endregion

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
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Add_work";
                command2.Parameters.AddWithValue("@id_maintenance", EditMaintenancesData.IDMaintenances);
                command2.Parameters.AddWithValue("@name_work", CompletedWorks.NameWork);
                command2.Parameters.AddWithValue("@cost_work", CompletedWorks.Cost);
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }

            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда Редактирования выполненных работ : EditCompletedWorksCommand

        public ICommand EditCompletedWorksCommand { get; }

        private bool CanEditCompletedWorksCommandExecute(object p) => SelectCompletedWorks > -1;

        private void OnEditCompletedWorksCommandExecuted(object p)
        {
            CompletedWorks.Title = "Редактирование выполненных работ";
            CompletedWorks.NameWork = Convert.ToString(DataCompletedWorks.Rows[SelectCompletedWorks]["name_work"]);
            CompletedWorks.Cost = Convert.ToString(DataCompletedWorks.Rows[SelectCompletedWorks]["cost_work"]);

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Edit_work";
                command2.Parameters.AddWithValue("@id_work", Convert.ToInt32(DataCompletedWorks.Rows[SelectCompletedWorks]["id_work"]));
                command2.Parameters.AddWithValue("@name_work", CompletedWorks.NameWork);
                command2.Parameters.AddWithValue("@cost_work", CompletedWorks.Cost);
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }

            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда Удаления выполненных работ : DropCompletedWorksCommand

        public ICommand DropCompletedWorksCommand { get; }

        private bool CanDropCompletedWorksCommandExecute(object p) => SelectCompletedWorks > -1;

        private void OnDropCompletedWorksCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данную запись безвозвратно?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Drop_work";
                command2.Parameters.AddWithValue("@id_work", Convert.ToInt32(DataCompletedWorks.Rows[SelectCompletedWorks]["id_work"]));
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }
            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда Добавления закупленных запчастей : AddDataSparePartsCommand

        public ICommand AddDataSparePartsCommand { get; }

        private bool CanAddDataSparePartsCommandExecute(object p) => true;

        private void OnAddDataSparePartsCommandExecuted(object p)
        {
            CompletedWorks.Title = "Добавление закупленных запчастей";
            CompletedWorks.NameWork = "";
            CompletedWorks.Cost = "";

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Add_spare_part";
                command2.Parameters.AddWithValue("@id_maintenance", EditMaintenancesData.IDMaintenances);
                command2.Parameters.AddWithValue("@name_spare_part", CompletedWorks.NameWork);
                command2.Parameters.AddWithValue("@cost_spare_part", CompletedWorks.Cost);
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }

            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда Редактирования закупленных запчастей : EditDataSparePartsCommand

        public ICommand EditDataSparePartsCommand { get; }

        private bool CanEditDataSparePartsCommandExecute(object p) => SelectSpareParts > -1;

        private void OnEditDataSparePartsCommandExecuted(object p)
        {
            CompletedWorks.Title = "Редактирование закупленный запчастей";
            CompletedWorks.NameWork = Convert.ToString(DataSpareParts.Rows[SelectSpareParts]["name_spare_part"]);
            CompletedWorks.Cost = Convert.ToString(DataSpareParts.Rows[SelectSpareParts]["cost_spare_part"]);

            AddCompletedWorksCommandWindow addCompletedWorksCommandWindow = new AddCompletedWorksCommandWindow();
            addCompletedWorksCommandWindow.ShowDialog();

            if (CompletedWorks.Title == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Edit_spare_part";
                command2.Parameters.AddWithValue("@id_spare_part", Convert.ToInt32(DataSpareParts.Rows[SelectSpareParts]["id_spare_part"]));
                command2.Parameters.AddWithValue("@name_spare_part", CompletedWorks.NameWork);
                command2.Parameters.AddWithValue("@cost_spare_part", CompletedWorks.Cost);
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }

            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда Удаления закупленных запчастей : DropDataSparePartsCommand

        public ICommand DropDataSparePartsCommand { get; }

        private bool CanDropDataSparePartsCommandExecute(object p) => SelectSpareParts > -1;

        private void OnDropDataSparePartsCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данную запись безвозвратно?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Drop_spare_part";
                command2.Parameters.AddWithValue("@id_spare_part", Convert.ToInt32(DataSpareParts.Rows[SelectSpareParts]["id_spare_part"]));
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }
            UpdateCommand.Execute(null);
        }

        #endregion

        #region Команда Редактирования Технического обслуживания : EditMaintenanceCommand

        public ICommand EditMaintenanceCommand { get; }

        private bool CanEditMaintenanceCommandExecute(object p) => NameMaintenance != "";

        private void OnEditMaintenanceCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            if (IsCheck == true)
            {
                if (EndDateMaintenance > StartDateMaintenance)
                {
                    var command2 = ThisConnection.CreateCommand();
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Edit_maintenance";
                    command2.Parameters.AddWithValue("@id_maintenance", EditMaintenancesData.IDMaintenances);
                    command2.Parameters.AddWithValue("@name_maintenance", NameMaintenance);
                    command2.Parameters.AddWithValue("@start_date_maintenance", StartDateMaintenance);
                    command2.Parameters.AddWithValue("@end_date_maintenance", EndDateMaintenance);
                    command2.ExecuteNonQuery();
                    ThisConnection.Close();
                }
            }
            else
            {
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Edit_NULL_maintenance";
                command2.Parameters.AddWithValue("@id_maintenance", EditMaintenancesData.IDMaintenances);
                command2.Parameters.AddWithValue("@name_maintenance", NameMaintenance);
                command2.Parameters.AddWithValue("@start_date_maintenance", StartDateMaintenance);
                command2.ExecuteNonQuery();
                ThisConnection.Close();
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

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public EditMaintenanceWindowViewModel()
        {
            #region Команды

            VisibleCommand = new LamdaCommand(OnVisibleCommandExecuted, CanVisibleCommandExecute);

            UpdateCommand = new LamdaCommand(OnUpdateCommandExecuted, CanUpdateCommandExecute);

            AddCompletedWorksCommand = new LamdaCommand(OnAddCompletedWorksCommandExecuted, CanAddCompletedWorksCommandExecute);

            EditCompletedWorksCommand = new LamdaCommand(OnEditCompletedWorksCommandExecuted, CanEditCompletedWorksCommandExecute);

            DropCompletedWorksCommand = new LamdaCommand(OnDropCompletedWorksCommandExecuted, CanDropCompletedWorksCommandExecute);

            AddDataSparePartsCommand = new LamdaCommand(OnAddDataSparePartsCommandExecuted, CanAddDataSparePartsCommandExecute);

            EditDataSparePartsCommand = new LamdaCommand(OnEditDataSparePartsCommandExecuted, CanEditDataSparePartsCommandExecute);

            DropDataSparePartsCommand = new LamdaCommand(OnDropDataSparePartsCommandExecuted, CanDropDataSparePartsCommandExecute);

            EditMaintenanceCommand = new LamdaCommand(OnEditMaintenanceCommandExecuted, CanEditMaintenanceCommandExecute);

            #endregion

            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_work,name_work,cost_work from work,[maintenance] where maintenance.id_maintenance= work.id_maintenance and work.id_maintenance = " + EditMaintenancesData.IDMaintenances;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            DataCompletedWorks = dt;

            DataTable dt2 = new DataTable();

            thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select id_spare_part,name_spare_part,cost_spare_part from spare_part,[maintenance] where maintenance.id_maintenance= spare_part.id_maintenance and spare_part.id_maintenance = " + EditMaintenancesData.IDMaintenances;
            thisReader = thisCommand.ExecuteReader();
            dt2.Load(thisReader);
            DataSpareParts = dt2;

            thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select name_maintenance,start_date_maintenance,end_date_maintenance from [maintenance] where id_maintenance = " + EditMaintenancesData.IDMaintenances;
            thisReader = thisCommand.ExecuteReader();
            thisReader.Read();

            NameMaintenance = thisReader["name_maintenance"].ToString();
            StartDateMaintenance = Convert.ToDateTime(thisReader["start_date_maintenance"].ToString());
            if (thisReader["end_date_maintenance"].ToString() == "")
            {
                EndDateMaintenance = DateTime.Today;
            }
            else
            {
                EndDateMaintenance = Convert.ToDateTime(thisReader["end_date_maintenance"].ToString());
                IsCheck = true;
                Visible = "true";
            }

            thisReader.Close();

            ThisConnection.Close();
            #endregion
        }
    }
}
