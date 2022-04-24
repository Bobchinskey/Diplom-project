using Partner.Infrastructure.Commands;
using Partner.Models.Rate;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rates
{
    class EditRateWindowViewModel : ViewModelBase
    {
        #region Данные

        #region ID автомобиля : IdVehicle 

        private int _IdVehicle = ListRateStatic.id_vehicle;

        /// <summary>IdVehicle</summary>
        public int IdVehicle
        {
            get => _IdVehicle;
            set => Set(ref _IdVehicle, value);
        }

        #endregion

        #region Марка и модель автомобиля : MakeModel 

        private string _MakeModel = ListRateStatic.make_model;

        /// <summary>MakeModel</summary>
        public string MakeModel
        {
            get => _MakeModel;
            set => Set(ref _MakeModel, value);
        }

        #endregion

        #region Тариф 1-3 дня(суток) : Rate1_3 

        private string _Rate1_3 = ListRateStatic.Rate1_3;

        /// <summary>Rate1_3</summary>
        public string Rate1_3
        {
            get => _Rate1_3;
            set => Set(ref _Rate1_3, value);
        }

        #endregion

        #region Тариф 4-9 дня(суток) : Rate4_9

        private string _Rate4_9 = ListRateStatic.Rate4_9;

        /// <summary>Rate4_9</summary>
        public string Rate4_9
        {
            get => _Rate4_9;
            set => Set(ref _Rate4_9, value);
        }

        #endregion

        #region Тариф 10-29 дней(суток) : Rate10_29

        private string _Rate10_29 = ListRateStatic.Rate10_29;

        /// <summary>Rate10_29</summary>
        public string Rate10_29
        {
            get => _Rate10_29;
            set => Set(ref _Rate10_29, value);
        }

        #endregion

        #region Тариф от 30 дней(суток) : Rate30

        private string _Rate30 = ListRateStatic.Rate30;

        /// <summary>Rate30</summary>
        public string Rate30
        {
            get => _Rate30;
            set => Set(ref _Rate30, value);
        }

        #endregion

        #region Залог : Deposit

        private string _Deposit = ListRateStatic.Deposit;

        /// <summary>Deposit</summary>
        public string Deposit
        {
            get => _Deposit;
            set => Set(ref _Deposit, value);
        }

        #endregion

        #region Перепробег : ExcessMileage

        private string _ExcessMileage = ListRateStatic.excess_mileage;

        /// <summary>ExcessMileage</summary>
        public string ExcessMileage
        {
            get => _ExcessMileage;
            set => Set(ref _ExcessMileage, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Редактирования тарифов

        public ICommand EditRateCommand { get; }

        private bool CanEditRateCommandExecute(object p) => true;

        private void OnEditRateCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            var command = ThisConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Update_rate";
            command.Parameters.AddWithValue("@id_vehicle", IdVehicle);
            command.Parameters.AddWithValue("@1_3_day", Rate1_3);
            command.Parameters.AddWithValue("@4_9_day", Rate4_9);
            command.Parameters.AddWithValue("@10_29_day", Rate10_29);
            command.Parameters.AddWithValue("@30_day", Rate30);
            command.Parameters.AddWithValue("@Deposit", Deposit);
            command.Parameters.AddWithValue("@excess_mileage", ExcessMileage);
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

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public EditRateWindowViewModel()
        {
            #region Команды

            EditRateCommand = new LamdaCommand(OnEditRateCommandExecuted, CanEditRateCommandExecute);

            #endregion
        }
    }
}
