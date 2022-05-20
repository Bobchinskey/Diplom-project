using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Booking
{
    internal class ListRentalInformationWindowViewModel : ViewModelBase
    {
        #region Данные

        #region DataTable данных Аренд : MainListRental

        private DataTable _MainListRental;

        /// <summary>MainListRental</summary>
        public DataTable MainListRental
        {
            get => _MainListRental;
            set => Set(ref _MainListRental, value);
        }

        public DataTable MainListRentalDelivery = new DataTable("MainListRentalDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListRentalInformationWindowViewModel()
        {
            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select id_contract,vehicle.id_vehicle,contract_natural_person.id_natural_person,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,rental.start_date_rental,rental.end_date_rental,rental.condition,'Физическое лицо' as type from contract_natural_person, rental,natural_person,vehicle where rental.id_rental = contract_natural_person.id_rental and contract_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') Union (Select id_contract, vehicle.id_vehicle, contract_legal_entity.id_legal_entity, legal_entity.name_organization as name, rental.start_date_rental, rental.end_date_rental, rental.condition, 'Юридическое лицо' as type from contract_legal_entity, rental, legal_entity, vehicle where rental.id_rental = contract_legal_entity.id_rental and contract_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = rental.id_vehicle and rental.id_vehicle = '" + VehicleDataModel.id_vehicle + "' and rental.condition!='Завершенна') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            MainListRentalDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListRentalDelivery.Columns.Add(column);

            int k = MainListRentalDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListRentalDelivery.Rows[i]["num"] = i + 1;
            }

            MainListRental = MainListRentalDelivery;

            #endregion
        }

    }
}
