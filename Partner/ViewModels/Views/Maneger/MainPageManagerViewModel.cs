using Partner.Infrastructure.Commands;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
using Partner.ViewModels.Windows;
using Partner.Views.Views.Manager;
using Partner.Views.Views.Manager.Pages;
using Partner.Views.Windows;
using Partner.Views.Windows.InformativeWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Partner.ViewModels.Views.Maneger
{
    class MainPageManagerViewModel : ViewModelBase
    {

        #region Начальная страница : StartPage

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

        #region Команда смены пользователя

        public ICommand ChangeUserCommand { get; }

        private bool CanChangeUserCommandExecute(object p) => true;

        private void OnChangeUserCommandExecuted(object p)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion

        #region Команда обновления данных

        public ICommand UpdatePageCommand { get; }

        private bool CanUpdatePageCommandExecute(object p) => true;

        private void OnUpdatePageCommandExecuted(object p)
        {
            UserDataModel.page = new MainPageRental();
            StartPage = UserDataModel.page;
        }

        #endregion

        #region Команда вызова новостей

        public ICommand OpenNewsPageCommand { get; }

        private bool CanOpenNewsPageCommandExecute(object p) => true;

        private void OnOpenNewsPageCommandExecuted(object p)
        {
            StartingInformationWindow startingInformationWindow = new StartingInformationWindow();
            startingInformationWindow.Show();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public MainPageManagerViewModel()
        {

            #region Команды

            ChangeUserCommand = new LamdaCommand(OnChangeUserCommandExecuted, CanChangeUserCommandExecute);

            UpdatePageCommand = new LamdaCommand(OnUpdatePageCommandExecuted, CanUpdatePageCommandExecute);

            OpenNewsPageCommand = new LamdaCommand(OnOpenNewsPageCommandExecuted, CanOpenNewsPageCommandExecute);

            #endregion
        }
    }
}
