using Partner.Infrastructure.Commands;
using Partner.Models.Rate.AdditionalServices;
using Partner.Models.Rental;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental.AddRental
{
    class AddAdditionalRatesRentalWindowViewModel : ViewModelBase
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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавляющая дополнительный тариф : AddAdditionalRatesRentalCommand

        public ICommand AddAdditionalRatesRentalCommand { get; }

        private bool CanAddAdditionalRatesRentalCommandExecute(object p) => SelectedAdditionalServices > -1;

        private void OnAddAdditionalRatesRentalCommandExecuted(object p)
        {
            DataAdditionalServices.Status = "Прошло успешно";
            DataAdditionalServices.IDAdditionalServices = MainListAdditionalServices[SelectedAdditionalServices].id_additional_services;
            DataAdditionalServices.TypePayment = MainListAdditionalServices[SelectedAdditionalServices].type_additional_services;
            DataAdditionalServices.NameAdditionalServices = MainListAdditionalServices[SelectedAdditionalServices].name_additional_services;
            DataAdditionalServices.CostAdditionalServices = MainListAdditionalServices[SelectedAdditionalServices].cost_additional_services;

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

        public AddAdditionalRatesRentalWindowViewModel()
        {
            #region Команды

            AddAdditionalRatesRentalCommand = new LamdaCommand(OnAddAdditionalRatesRentalCommandExecuted, CanAddAdditionalRatesRentalCommandExecute);

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
