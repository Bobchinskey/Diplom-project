using Partner.Models.Reports;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Reports
{
    internal class ReportWindowViewModel : ViewModelBase
    {
        #region Данные 

        #region Заголовок окна : Title

        private string _Title = ReportDataModel.Title;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region DataTable данных Отчета : MainListReport

        private DataTable _MainListReport;

        /// <summary>MainListReport</summary>
        public DataTable MainListReport
        {
            get => _MainListReport;
            set => Set(ref _MainListReport, value);
        }

        public DataTable MainListReportDelivery = new DataTable("MainListReportDelivery");

        #endregion

        #region Переменная column

        DataColumn column;

        #endregion

        #region Выбранный элемент таблицы "Отчеты" : SelectedReport

        private int _SelectedReport = 0;

        /// <summary>SelectedReport</summary>
        public int SelectedReport
        {
            get => _SelectedReport;
            set => Set(ref _SelectedReport, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ReportWindowViewModel()
        {
            #region Команды



            #endregion

            #region Данные

            if (ReportDataModel.Title == "Отчет по количеству аренд")
            {

            }

            #endregion
        }
    }
}
