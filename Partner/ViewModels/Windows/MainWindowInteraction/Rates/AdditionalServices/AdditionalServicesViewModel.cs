using Partner.Infrastructure.Commands;
using Partner.Models.Rate.AdditionalServices;
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

        #region Команда фильтрации клиентов по их статусу

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

        #region Команда удаления дополнительных услуг
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

        #region Команда востановление дополнительных услуг
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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AdditionalServicesViewModel()
        {
            #region Команды

            FilterAdditionalServicesStatusCommand = new LamdaCommand(OnFilterAdditionalServicesStatusCommandExecuted, CanFilterAdditionalServicesStatusCommandExecute);

            DropAdditionalServicesCommand = new LamdaCommand(OnDropAdditionalServicesCommandExecuted, CanDropAdditionalServicesCommandExecute);

            RepeatAdditionalServicesCommand = new LamdaCommand(OnRepeatAdditionalServicesCommandExecuted, CanRepeatAdditionalServicesCommandExecute);

            #endregion

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
    }
}
