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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental
{
    class ListBookingWindowViewModel : ViewModelBase
    {
        #region Данные

        #region DataTable данных Броней : MainListBooking

        private DataTable _MainListBooking;

        /// <summary>MainListBooking</summary>
        public DataTable MainListBooking
        {
            get => _MainListBooking;
            set => Set(ref _MainListBooking, value);
        }

        public DataTable MainListBookingDelivery = new DataTable("MainListBookingDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListBookingWindowViewModel()
        {
            #region Данные

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "(Select booking_natural_person.start_date_booking,booking_natural_person.end_date_booking,natural_person.surname + ' '  + natural_person.name + ' ' + natural_person.patronymic as name,'Физическое лицо' as type from booking_natural_person,natural_person,vehicle where booking_natural_person.id_natural_person=natural_person.id_natural_person and vehicle.id_vehicle=booking_natural_person.id_vehicle and booking_natural_person.reality!='Отменено' and booking_natural_person.reality!='Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') Union (Select booking_legal_entity.start_date_booking, booking_legal_entity.end_date_booking, legal_entity.name_organization as name, 'Юридическое лицо' as type  from booking_legal_entity, legal_entity, vehicle where booking_legal_entity.id_legal_entity = legal_entity.id_legal_entity and vehicle.id_vehicle = booking_legal_entity.id_vehicle and booking_legal_entity.reality != 'Отменено' and booking_legal_entity.reality != 'Закрыто' and vehicle.id_vehicle= '" + VehicleDataModel.id_vehicle + "') ORDER BY name";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();
            MainListBookingDelivery = dt;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "num";
            column.ReadOnly = false;
            column.Unique = false;
            MainListBookingDelivery.Columns.Add(column);

            int k = MainListBookingDelivery.Rows.Count;
            for (int i = 0; i < k; i++)
            {
                MainListBookingDelivery.Rows[i]["num"] = i + 1;
            }

            MainListBooking = MainListBookingDelivery;

            #endregion
        }
    }
}
