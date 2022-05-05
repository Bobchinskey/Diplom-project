using Partner.Infrastructure.Commands;
using Partner.Models.Insurances;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Insurances
{
    class OSAGOWindowViewModel : ViewModelBase
    {

        #region Данные

        #region Заголовок окна : Title 

        private string _Title = EditOrAddInsurancesModel.Title;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Тип страхования : Type

        private string _Type = EditOrAddInsurancesModel.Type;

        /// <summary>Type</summary>
        public string Type
        {
            get => _Type;
            set => Set(ref _Type, value);
        }

        #endregion

        #region Выбранный элемент Combobox "Автомобиль" : SelectedVehicleProperty

        private string _SelectedVehicleProperty = EditOrAddInsurancesModel.make_model;

        /// <summary>SelectedVehicleProperty</summary>
        public string SelectedVehicleProperty
        {
            get => _SelectedVehicleProperty;
            set => Set(ref _SelectedVehicleProperty, value);
        }

        #endregion

        #region Массив данных автомобили : vehicle

        private DataTable _vehicle;

        /// <summary>vehicle</summary>

        public DataTable vehicle
        {
            get => _vehicle;
            set => Set(ref _vehicle, value);
        }
        public DataTable vehicleDelivery = new DataTable("vehicleDelivery");

        #endregion

        #region Серия страхования : Series 

        private string _Series = EditOrAddInsurancesModel.Series;

        /// <summary>Series</summary>
        public string Series
        {
            get => _Series;
            set => Set(ref _Series, value);
        }

        #endregion

        #region Номер страхования : Number 

        private string _Number = EditOrAddInsurancesModel.Number;

        /// <summary>Number</summary>
        public string Number
        {
            get => _Number;
            set => Set(ref _Number, value);
        }

        #endregion

        #region Дата начала страхования : StartDate 

        private DateTime _StartDate = EditOrAddInsurancesModel.start_date;

        /// <summary>StartDate</summary>
        public DateTime StartDate
        {
            get => _StartDate;
            set => Set(ref _StartDate, value);
        }

        #endregion

        #region Дата окончания страхования : EndDate 

        private DateTime _EndDate = EditOrAddInsurancesModel.end_dete;

        /// <summary>EndDate</summary>
        public DateTime EndDate
        {
            get => _EndDate;
            set => Set(ref _EndDate, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status;

        /// <summary>status</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region Активность Combobox : ActionCombobox 

        private bool _ActionCombobox = EditOrAddInsurancesModel.ActionCombobox;

        /// <summary>ActionCombobox</summary>
        public bool ActionCombobox
        {
            get => _ActionCombobox;
            set => Set(ref _ActionCombobox, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Сохранения данных

        public ICommand SaveCommand { get; }

        private bool CanSaveCommandExecute(object p) => true;

        private void OnSaveCommandExecuted(object p)
        {
            if ((Series.Length >= 3) && (Number.Length == 6))
            {
                if ((StartDate < EndDate) && (EndDate > DateTime.Today))
                {
                    if (SelectedVehicleProperty != "")
                    {
                        if (Title == "Добавление страховки")
                        {

                            int id = 0;
                            for (int i = 0; vehicle.Rows.Count >= i + 1; i++)
                            {
                                if (status[i] == SelectedVehicleProperty)
                                {
                                    id = Convert.ToInt32(vehicle.Rows[i][0]);
                                }
                            }
                            if (Type == "ОСАГО")
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                SqlConnection ThisConnection = new SqlConnection(connectionString);
                                ThisConnection.Open();
                                var command = ThisConnection.CreateCommand();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "Add_OSAGO";
                                command.Parameters.AddWithValue("@id_vehicle", id);
                                command.Parameters.AddWithValue("@series", Series);
                                command.Parameters.AddWithValue("@number", Number);
                                command.Parameters.AddWithValue("@start_date", StartDate);
                                command.Parameters.AddWithValue("@end_date", EndDate);
                                command.ExecuteNonQuery();
                                ThisConnection.Close();
                            }
                            else
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                SqlConnection ThisConnection = new SqlConnection(connectionString);
                                ThisConnection.Open();
                                var command = ThisConnection.CreateCommand();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "Add_KASKO"; 
                                command.Parameters.AddWithValue("@id_vehicle", id);
                                command.Parameters.AddWithValue("@series", Series);
                                command.Parameters.AddWithValue("@number", Number);
                                command.Parameters.AddWithValue("@start_date", StartDate);
                                command.Parameters.AddWithValue("@end_date", EndDate);
                                command.ExecuteNonQuery();
                                ThisConnection.Close();
                            }
                        }
                        else
                        {
                            if (Type == "ОСАГО")
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                SqlConnection ThisConnection = new SqlConnection(connectionString);
                                ThisConnection.Open();
                                var command = ThisConnection.CreateCommand();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "Edit_OSAGO";
                                command.Parameters.AddWithValue("@id_osago", EditOrAddInsurancesModel.ID);
                                command.Parameters.AddWithValue("@series", Series);
                                command.Parameters.AddWithValue("@number", Number);
                                command.Parameters.AddWithValue("@start_date", StartDate);
                                command.Parameters.AddWithValue("@end_date", EndDate);
                                command.ExecuteNonQuery();
                                ThisConnection.Close();
                            }
                            else
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                SqlConnection ThisConnection = new SqlConnection(connectionString);
                                ThisConnection.Open();
                                var command = ThisConnection.CreateCommand();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "Edit_KASKO";
                                command.Parameters.AddWithValue("@id_kasko", EditOrAddInsurancesModel.ID);
                                command.Parameters.AddWithValue("@series", Series);
                                command.Parameters.AddWithValue("@number", Number);
                                command.Parameters.AddWithValue("@start_date", StartDate);
                                command.Parameters.AddWithValue("@end_date", EndDate);
                                command.ExecuteNonQuery();
                                ThisConnection.Close();
                            }
                        }

                        MessageBox.Show("Даннае добавлены");

                        foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                        {
                            if (window.DataContext == this)
                            {
                                window.Close();
                            }
                        }
                    }
                    else MessageBox.Show("Выберите автомобиль");
                }
                else MessageBox.Show("Проверьте правильность введенных дат");
            }
            else MessageBox.Show("Серия или номер страхования введены не верно");
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public OSAGOWindowViewModel()
        {

            #region Команды

            SaveCommand = new LamdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);

            #endregion

            #region Заполнение данных "Автомобили"

            if (EditOrAddInsurancesModel.Title == "Добавление страховки")
            {
                if (EditOrAddInsurancesModel.Type == "ОСАГО")
                {
                    DataTable dt = new DataTable();

                    string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                    SqlConnection ThisConnection = new SqlConnection(connectionString);
                    ThisConnection.Open();
                    SqlCommand thisCommand = ThisConnection.CreateCommand();
                    thisCommand.CommandText = "Select id_vehicle, make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle where status!='Архив' EXCEPT Select vehicle.id_vehicle, make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle, OSAGO, KASKO where vehicle.id_vehicle = OSAGO.id_vehicle and OSAGO.reality != 'Архив'";
                    SqlDataReader thisReader = thisCommand.ExecuteReader();
                    dt.Load(thisReader);
                    ThisConnection.Close();

                    string[] ds = new string[dt.Rows.Count];

                    for (int i = 1; dt.Rows.Count >= i; i++)
                    {
                        ds[i - 1] = Convert.ToString(dt.Rows[i - 1][1]);
                    }

                    status = ds;

                    vehicle = dt;
                }
                else
                {
                    DataTable dt = new DataTable();

                    string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                    SqlConnection ThisConnection = new SqlConnection(connectionString);
                    ThisConnection.Open();
                    SqlCommand thisCommand = ThisConnection.CreateCommand();
                    thisCommand.CommandText = "Select id_vehicle, make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle where status!='Архив' EXCEPT Select vehicle.id_vehicle, make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle, KASKO where vehicle.id_vehicle = KASKO.id_vehicle and KASKO.reality != 'Архив'";
                    SqlDataReader thisReader = thisCommand.ExecuteReader();
                    dt.Load(thisReader);
                    ThisConnection.Close();

                    string[] ds = new string[dt.Rows.Count];

                    for (int i = 1; dt.Rows.Count >= i; i++)
                    {
                        ds[i - 1] = Convert.ToString(dt.Rows[i - 1][1]);
                    }

                    status = ds;

                    vehicle = dt;
                }
            }
            else
            {
                string[] ds = { EditOrAddInsurancesModel.make_model };
                status = ds;
            }
            
            #endregion
        }
    }
}
