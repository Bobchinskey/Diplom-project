using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental
{
    class ListRentalWindowVeiwModel : ViewModelBase
    {

        #region Данные

        #region List данных Аредн : MainListRental


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

        #region Выбранный элемент таблицы "Аренды" : SelectedRental

        private int _SelectedRental = 0;

        /// <summary>SelectedRental</summary>
        public int SelectedRental
        {
            get => _SelectedRental;
            set => Set(ref _SelectedRental, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListRentalWindowVeiwModel()
        {
            #region Команды

            #endregion

            #region Данные



            #endregion
        }
    }
}
