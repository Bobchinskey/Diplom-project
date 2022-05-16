using Partner.Infrastructure.Commands;
using Partner.Models.Rental;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.Rental.AddRental;
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
    class AddRentalWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Заголовок : Title

        private string _Title = DataStaticRental.Title;

        /// <summary>Title</summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Заказчик : Client

        private string _Client;

        /// <summary>Client</summary>

        public string Client
        {
            get => _Client;
            set => Set(ref _Client, value);
        }

        #endregion

        #region Транспортное средство : Vehicle

        private string _Vehicle;

        /// <summary>Vehicle</summary>

        public string Vehicle
        {
            get => _Vehicle;
            set => Set(ref _Vehicle, value);
        }

        #endregion

        #region Дата начала срока Аренды : StartDateRental

        private DateTime _StartDateRental = DataStaticRental.StartDateRental;

        /// <summary>StartDateRental</summary>

        public DateTime StartDateRental
        {
            get => _StartDateRental;
            set => Set(ref _StartDateRental, value);
        }

        #endregion

        #region Дата окончания срока Аренды : EndDateRental

        private DateTime _EndDateRental = DataStaticRental.EndDateRental;

        /// <summary>EndDateRental</summary>

        public DateTime EndDateRental
        {
            get => _EndDateRental;
            set => Set(ref _EndDateRental, value);
        }

        #endregion

        #region Итоговая стоимость : Cost

        private string _Cost;

        /// <summary>Cost</summary>

        public string Cost
        {
            get => _Cost;
            set => Set(ref _Cost, value);
        }

        int CostST;

        int day;

        #endregion

        #region Залог : Deposit

        private string _Deposit;

        /// <summary>Deposit</summary>

        public string Deposit
        {
            get => _Deposit;
            set => Set(ref _Deposit, value);
        }

        #endregion

        #region Данные дополнительных услуг : AdditionalRateRental

        private DataTable _AdditionalRateRental;

        /// <summary>AdditionalRateRental</summary>
        public DataTable AdditionalRateRental
        {
            get => _AdditionalRateRental;
            set => Set(ref _AdditionalRateRental, value);
        }

        public DataTable AdditionalRateRentalDelivery = new DataTable("AdditionalRateRentalDelivery");
        DataColumn column;
        DataRow row;

        #endregion

        #region Выбранная дополнительная услуга : SelectAdditionalRateRental

        private int _SelectAdditionalRateRental = -1;

        /// <summary>SelectAdditionalRateRental</summary>
        public int SelectAdditionalRateRental
        {
            get => _SelectAdditionalRateRental;
            set => Set(ref _SelectAdditionalRateRental, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Вызывающая окно добавления дополнительных услуг : AddAdditionalRatesRentalWindow

        public ICommand OpenAddAdditionalRatesRentalCommand { get; }

        private bool CanOpenAddAdditionalRatesRentalCommandExecute(object p) => true;

        private void OnOpenAddAdditionalRatesRentalCommandExecuted(object p)
        {
            AddAdditionalRatesRentalWindow addAdditionalRatesRentalWindow = new AddAdditionalRatesRentalWindow();
            addAdditionalRatesRentalWindow.ShowDialog();

            if (DataAdditionalServices.Status == "Прошло успешно")
            {
                row = AdditionalRateRentalDelivery.NewRow();
                row["ID"] = DataAdditionalServices.IDAdditionalServices;
                row["NameRental"] = DataAdditionalServices.NameAdditionalServices;
                row["TypePayment"] = DataAdditionalServices.TypePayment;
                row["CostRental"] = DataAdditionalServices.CostAdditionalServices;
                AdditionalRateRentalDelivery.Rows.Add(row);

                AdditionalRateRental = AdditionalRateRentalDelivery;

                UpdateCostCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Удаления дополнительной услуги : DropAdditionalServicesCommand

        public ICommand DropAdditionalServicesCommand { get; }

        private bool CanDropAdditionalServicesCommandExecute(object p) => SelectAdditionalRateRental > -1;

        private void OnDropAdditionalServicesCommandExecuted(object p)
        {
            DataRow b = AdditionalRateRental.Rows[SelectAdditionalRateRental];
            AdditionalRateRentalDelivery.Rows.Remove(b);
            AdditionalRateRental = AdditionalRateRentalDelivery;

            UpdateCostCommand.Execute(null);
        }

        #endregion

        #region Команда Обновления итоговой стоимости : DropAdditionalServicesCommand

        public ICommand UpdateCostCommand { get; }

        private bool CanUpdateCostCommandExecute(object p) => true;

        private void OnUpdateCostCommandExecuted(object p)
        {
           if (AdditionalRateRental.Rows.Count > 0)
            {
                int CostSD = CostST;
                int k = 0;

                for (int i = 1; i <= AdditionalRateRental.Rows.Count; i++)
                {
                    if (Convert.ToString(AdditionalRateRental.Rows[k][2]) == "Разовый")
                    {
                        CostSD += Convert.ToInt32(AdditionalRateRental.Rows[k][3]);
                    }
                    else
                    {
                        CostSD = (Convert.ToInt32(AdditionalRateRental.Rows[k][3]) * day) + CostSD;
                    }
                    k++;
                }

                Cost = Convert.ToString(CostSD);
            }
            else
            {
                Cost = Convert.ToString(CostST);
            }
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddRentalWindowViewModel()
        {
            #region Команды

            OpenAddAdditionalRatesRentalCommand = new LamdaCommand(OnOpenAddAdditionalRatesRentalCommandExecuted, CanOpenAddAdditionalRatesRentalCommandExecute);

            DropAdditionalServicesCommand = new LamdaCommand(OnDropAdditionalServicesCommandExecuted, CanDropAdditionalServicesCommandExecute);

            UpdateCostCommand = new LamdaCommand(OnUpdateCostCommandExecuted, CanUpdateCostCommandExecute);

            #endregion

            #region Данные

            int CostRate = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();

            if (DataStaticRental.Type == "Физическое лицо")
            {
                thisCommand.CommandText = "Select [surname] + ' ' + [name] + ' ' + patronymic as name from natural_person  where id_natural_person=" + DataStaticRental.IDClient ;
            }
            else 
            {
                thisCommand.CommandText = "Select name_organization as name from legal_entity  where id_legal_entity=" + DataStaticRental.IDClient;
            }
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Client = thisReader["name"].ToString();
            thisReader.Close();
            thisCommand.CommandText = "Select make_model + ' (' + vehicle.state_number + ')' as make_model from vehicle  where id_vehicle=" +  VehicleDataModel.id_vehicle;
            thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Vehicle = thisReader["make_model"].ToString();
            thisReader.Close();

            day = (EndDateRental - StartDateRental).Days + 1;

            if (day <= 3)
            {
                thisCommand.CommandText = "Select [1-3_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }
            else if (day <= 9)
            {
                thisCommand.CommandText = "Select [4-9_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }
            else if (day <= 29)
            {
                thisCommand.CommandText = "Select [10-29_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }
            else
            {
                thisCommand.CommandText = "Select [30_day] as CostRate from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
                thisReader = thisCommand.ExecuteReader();
                thisReader.Read();
                CostRate = Convert.ToInt32(thisReader["CostRate"].ToString());
                thisReader.Close();
            }

            thisCommand.CommandText = "Select Deposit from rate where rate.id_vehicle=" + VehicleDataModel.id_vehicle;
            thisReader = thisCommand.ExecuteReader();
            thisReader.Read();
            Deposit = thisReader["Deposit"].ToString();
            thisReader.Close();

            ThisConnection.Close();

            Cost = Convert.ToString(CostRate * day);
            CostST = CostRate * day;

            #endregion

            #region Создание колонок таблицы AdditionalRateRental

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NameRental";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TypePayment";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CostRental";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalRateRentalDelivery.Columns.Add(column);

            #endregion

        }
    }
}
