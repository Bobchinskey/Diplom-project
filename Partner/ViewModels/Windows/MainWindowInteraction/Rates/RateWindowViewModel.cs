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

        #region Команда обновления окна

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


        #region Команда вызывающая окно "Редактирование тарифа"

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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public RateWindowViewModel()
        {

            #region Команды

            OpenEditRateWindowCommand = new LamdaCommand(OnOpenEditRateWindowCommandExecuted, CanOpenEditRateWindowCommandExecute);

            RepeatRateWindowCommand = new LamdaCommand(OnRepeatRateWindowCommandExecuted, CanRepeatRateWindowCommandExecute);

            #endregion

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
        }
    }
}
