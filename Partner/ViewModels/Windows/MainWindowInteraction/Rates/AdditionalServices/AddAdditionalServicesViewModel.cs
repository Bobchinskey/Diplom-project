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
    class AddAdditionalServicesViewModel : ViewModelBase
    {

        #region Данные

        #region Заголовок окна : Title 

        private string _Title = AddListData.title;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region ID дополниетльной услуги : IdAdditionalServices 

        private int _IdAdditionalServices = AddListData.id_additional_services;

        /// <summary>IdAdditionalServices</summary>
        public int IdAdditionalServices
        {
            get => _IdAdditionalServices;
            set => Set(ref _IdAdditionalServices, value);
        }

        #endregion

        #region Наименование дополнительной услуги : NameAdditionalServices 

        private string _NameAdditionalServices = AddListData.name_additional_services;

        /// <summary>NameAdditionalServices</summary>
        public string NameAdditionalServices
        {
            get => _NameAdditionalServices;
            set => Set(ref _NameAdditionalServices, value);
        }

        #endregion

        #region Тип дополнительной услуги : TypeAdditionalServices 

        private string _TypeAdditionalServices = AddListData.type_additional_services;

        /// <summary>TypeAdditionalServices</summary>
        public string TypeAdditionalServices
        {
            get => _TypeAdditionalServices;
            set => Set(ref _TypeAdditionalServices, value);
        }

        #endregion

        #region Стоимость дополнительной услуги : CostAdditionalServices 

        private string _CostAdditionalServices = AddListData.cost_additional_services;

        /// <summary>CostAdditionalServices</summary>
        public string CostAdditionalServices
        {
            get => _CostAdditionalServices;
            set => Set(ref _CostAdditionalServices, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда добавление дополнительных услуг
        public ICommand AddAdditionalServicesCommand { get; }

        private bool CanAddAdditionalServicesCommandExecute(object p)
        {
            if ((NameAdditionalServices != "") && (CostAdditionalServices != "") && (TypeAdditionalServices != ""))
                return true;
            else
                return false;
        }

        private void OnAddAdditionalServicesCommandExecuted(object p)
        {
            if (Title == "Добавление дополнительной услуги")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_additional_services";
                command.Parameters.AddWithValue("@name_additional_services", NameAdditionalServices);
                command.Parameters.AddWithValue("@cost_additional_services", CostAdditionalServices);
                command.Parameters.AddWithValue("@type_additional_services", TypeAdditionalServices);
                command.ExecuteNonQuery();
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
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_additional_services";
                command.Parameters.AddWithValue("@id_additional_services", IdAdditionalServices);
                command.Parameters.AddWithValue("@name_additional_services", NameAdditionalServices);
                command.Parameters.AddWithValue("@cost_additional_services", CostAdditionalServices);
                command.Parameters.AddWithValue("@type_additional_services", TypeAdditionalServices);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае обновлены");

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

        public AddAdditionalServicesViewModel()
        {
            #region Команды

            AddAdditionalServicesCommand = new LamdaCommand(OnAddAdditionalServicesCommandExecuted, CanAddAdditionalServicesCommandExecute);

            #endregion
        }
    }
}
