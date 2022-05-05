﻿using Partner.Infrastructure.Commands;
using Partner.Models.Insurances;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Insurances;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Insurances
{
    class InsurancesInformationWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных стахования : MainListInsurance

        private List<ListMainInsuranceData> _MainListInsurance;

        public List<ListMainInsuranceData> MainListInsurance
        {
            get => _MainListInsurance;
            set => Set(ref _MainListInsurance, value);
        }

        #endregion

        #region Выбранный элемент таблицы страхование : SelectedInsurance

        private int _SelectedInsurance = 0;

        /// <summary>SelectedInsurance</summary>
        public int SelectedInsurance
        {
            get => _SelectedInsurance;
            set => Set(ref _SelectedInsurance, value);
        }

        #endregion

        #region Выбранный элемент Combobox свойства страхования : SelectedInsuranceProperty

        private string _SelectedInsuranceProperty = "Актуально";

        /// <summary>SelectedInsuranceProperty</summary>
        public string SelectedInsuranceProperty
        {
            get => _SelectedInsuranceProperty;
            set => Set(ref _SelectedInsuranceProperty, value);
        }

        #endregion

        #region Массив данных фильтрующих элементов : status

        private string[] _status = { "Актуально", "ОСАГО", "КАСКО", "Архив", "Все" };

        /// <summary>status</summary>

        public string[] status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда фильтрации страховок по их статусу

        public ICommand FilterInsurancesStatusCommand { get; }

        private bool CanFilterInsurancesStatusCommandExecute(object p) => true;

        private void OnFilterInsurancesStatusCommandExecuted(object p)
        {
            if (SelectedInsuranceProperty == "Актуально")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_osago,OSAGO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')' as make_model, series, number,start_date,end_date, reality, 'Осаго' as 'type' from OSAGO,vehicle where reality != 'Архив' and OSAGO.id_vehicle=vehicle.id_vehicle) Union (Select id_kasko, KASKO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')'  as make_model, series, number, start_date, end_date, reality, 'Каско' as 'type' from KASKO, vehicle where reality != 'Архив' and KASKO.id_vehicle = vehicle.id_vehicle) ORDER BY type DESC";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListInsurance = dt.AsEnumerable().Select(se => new ListMainInsuranceData() { id_osago = se.Field<int>("id_osago"), id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), series = se.Field<string>("series"), number = se.Field<string>("number"), start_date = se.Field<DateTime>("start_date"), end_date = se.Field<DateTime>("end_date"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListInsurance.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListInsurance[i].num = i + 1;
                }
            }
            else if (SelectedInsuranceProperty == "ОСАГО")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_osago,OSAGO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')' as make_model, series, number,start_date,end_date, reality, 'Осаго' as 'type' from OSAGO,vehicle where reality != 'Архив' and OSAGO.id_vehicle=vehicle.id_vehicle";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListInsurance = dt.AsEnumerable().Select(se => new ListMainInsuranceData() { id_osago = se.Field<int>("id_osago"), id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), series = se.Field<string>("series"), number = se.Field<string>("number"), start_date = se.Field<DateTime>("start_date"), end_date = se.Field<DateTime>("end_date"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListInsurance.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListInsurance[i].num = i + 1;
                }
            }
            else if (SelectedInsuranceProperty == "КАСКО")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select id_kasko, KASKO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')'  as make_model, series, number, start_date, end_date, reality, 'Каско' as 'type' from KASKO, vehicle where reality != 'Архив' and KASKO.id_vehicle = vehicle.id_vehicle";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListInsurance = dt.AsEnumerable().Select(se => new ListMainInsuranceData() { id_osago = se.Field<int>("id_kasko"), id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), series = se.Field<string>("series"), number = se.Field<string>("number"), start_date = se.Field<DateTime>("start_date"), end_date = se.Field<DateTime>("end_date"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListInsurance.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListInsurance[i].num = i + 1;
                }
            }
            else if (SelectedInsuranceProperty == "Архив")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_osago,OSAGO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')' as make_model, series, number,start_date,end_date, reality, 'Осаго' as 'type' from OSAGO,vehicle where reality = 'Архив' and OSAGO.id_vehicle=vehicle.id_vehicle) Union (Select id_kasko, KASKO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')'  as make_model, series, number, start_date, end_date, reality, 'Каско' as 'type' from KASKO, vehicle where reality = 'Архив' and KASKO.id_vehicle = vehicle.id_vehicle) ORDER BY type DESC";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListInsurance = dt.AsEnumerable().Select(se => new ListMainInsuranceData() { id_osago = se.Field<int>("id_osago"), id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), series = se.Field<string>("series"), number = se.Field<string>("number"), start_date = se.Field<DateTime>("start_date"), end_date = se.Field<DateTime>("end_date"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListInsurance.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListInsurance[i].num = i + 1;
                }
            }
            else if (SelectedInsuranceProperty == "Все")
            {
                DataTable dt = new DataTable();

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "(Select id_osago,OSAGO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')' as make_model, series, number,start_date,end_date, reality, 'Осаго' as 'type' from OSAGO,vehicle where OSAGO.id_vehicle=vehicle.id_vehicle) Union (Select id_kasko, KASKO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')'  as make_model, series, number, start_date, end_date, reality, 'Каско' as 'type' from KASKO, vehicle where KASKO.id_vehicle = vehicle.id_vehicle) ORDER BY type DESC";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);
                MainListInsurance = dt.AsEnumerable().Select(se => new ListMainInsuranceData() { id_osago = se.Field<int>("id_osago"), id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), series = se.Field<string>("series"), number = se.Field<string>("number"), start_date = se.Field<DateTime>("start_date"), end_date = se.Field<DateTime>("end_date"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
                ThisConnection.Close();
                int k = MainListInsurance.Count;
                for (int i = 0; i < k; i++)
                {
                    MainListInsurance[i].num = i + 1;
                }
            }
        }

        #endregion

        #region Команда вызова окна "Добавление ОСАГО"

        public ICommand OpenAddOSAGOWindowCommand { get; }

        private bool CanOpenAddOSAGOWindowCommandExecute(object p) => true;

        private void OnOpenAddOSAGOWindowCommandExecuted(object p)
        {
            EditOrAddInsurancesModel.Title = "Добавление страховки";
            EditOrAddInsurancesModel.Type = "ОСАГО";
            EditOrAddInsurancesModel.Series = "";
            EditOrAddInsurancesModel.Number = "";
            EditOrAddInsurancesModel.make_model = "";
            EditOrAddInsurancesModel.start_date = DateTime.Today;
            EditOrAddInsurancesModel.end_dete = DateTime.Today;

            OSAGOWindow oSAGOWindow = new OSAGOWindow();
            oSAGOWindow.ShowDialog();

            FilterInsurancesStatusCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Добавление КАСКО"

        public ICommand OpenAddKASKOWindowCommand { get; }

        private bool CanOpenAddKASKOWindowCommandExecute(object p) => true;

        private void OnOpenAddKASKOWindowCommandExecuted(object p)
        {
            EditOrAddInsurancesModel.Title = "Добавление страховки";
            EditOrAddInsurancesModel.Type = "КАСКО";
            EditOrAddInsurancesModel.Series = "";
            EditOrAddInsurancesModel.Number = "";
            EditOrAddInsurancesModel.make_model = "";
            EditOrAddInsurancesModel.start_date = DateTime.Today;
            EditOrAddInsurancesModel.end_dete = DateTime.Today;

            OSAGOWindow oSAGOWindow = new OSAGOWindow();
            oSAGOWindow.ShowDialog();

            FilterInsurancesStatusCommand.Execute(null);
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public InsurancesInformationWindowViewModel()
        {
            #region Команды

            FilterInsurancesStatusCommand = new LamdaCommand(OnFilterInsurancesStatusCommandExecuted, CanFilterInsurancesStatusCommandExecute);

            OpenAddOSAGOWindowCommand = new LamdaCommand(OnOpenAddOSAGOWindowCommandExecuted, CanOpenAddOSAGOWindowCommandExecute);

            OpenAddKASKOWindowCommand = new LamdaCommand(OnOpenAddKASKOWindowCommandExecuted, CanOpenAddKASKOWindowCommandExecute);

            #endregion

            #region Проверка и замена устаревших данных

            DataTable dt1 = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand1 = ThisConnection.CreateCommand();
            thisCommand1.CommandText = "Select id_osago from OSAGO where end_date<GetDate() and reality!='Архив'";
            SqlDataReader thisReader1 = thisCommand1.ExecuteReader();
            dt1.Load(thisReader1);
            ThisConnection.Close();

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; dt1.Rows.Count > i; i++)
                {
                    ThisConnection.Open();
                    var command = ThisConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Edit_OSAGO";
                    command.Parameters.AddWithValue("@id_osago", dt1.Rows[i][0]);
                    command.ExecuteNonQuery();
                    ThisConnection.Close();
                }
            }

            #endregion

            #region Заполнение данных

            DataTable dt = new DataTable();

            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_osago,OSAGO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')' as make_model, series, number,start_date,end_date, reality, 'Осаго' as 'type' from OSAGO,vehicle where reality != 'Архив' and OSAGO.id_vehicle=vehicle.id_vehicle) Union (Select id_kasko, KASKO.id_vehicle, vehicle.make_model + ' (' + vehicle.state_number + ')'  as make_model, series, number, start_date, end_date, reality, 'Каско' as 'type' from KASKO, vehicle where reality != 'Архив' and KASKO.id_vehicle = vehicle.id_vehicle) ORDER BY type DESC";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            MainListInsurance = dt.AsEnumerable().Select(se => new ListMainInsuranceData() { id_osago = se.Field<int>("id_osago"), id_vehicle = se.Field<int>("id_vehicle"), make_model = se.Field<string>("make_model"), series = se.Field<string>("series"), number = se.Field<string>("number"), start_date = se.Field<DateTime>("start_date"), end_date = se.Field<DateTime>("end_date"), reality = se.Field<string>("reality"), type = se.Field<string>("type") }).ToList();
            ThisConnection.Close();
            int k = MainListInsurance.Count;
            for (int i = 0; i < k; i++)
            {
                MainListInsurance[i].num = i + 1;
            }

            #endregion
        }
    }
}
