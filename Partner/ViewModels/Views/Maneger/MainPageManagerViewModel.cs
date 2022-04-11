using Partner.ViewModels.Base;
using Partner.Views.Views.Manager.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.ViewModels.Views.Maneger
{
    class MainPageManagerViewModel : ViewModelBase
    {

        #region Начальная страница : UserDataModel.page

        private object _StartPage = new MainPageRental();

        /// <summary>StartPage</summary>
        public object StartPage
        {
            get => _StartPage;
            set => Set(ref _StartPage, value);
        }

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public MainPageManagerViewModel()
        {

        }
    }
}
