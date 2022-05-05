using Partner.Infrastructure.Commands;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
using Partner.ViewModels.Windows;
using Partner.Views.Views.Manager;
using Partner.Views.Views.Manager.Pages;
using Partner.Views.Windows;
using Partner.Views.Windows.InformativeWindows;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
using Partner.Views.Windows.MainWindowInteraction.Insurances;
using Partner.Views.Windows.MainWindowInteraction.Rates;
using Partner.Views.Windows.MainWindowInteraction.Rates.AdditionalServices;
using Partner.Views.Windows.MainWindowInteraction.VehicleWindow;
using Partner.Views.Windows.Settings;
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

        #region Команда вызова окна "О программе"

        public ICommand OpenAboutProgramCommand { get; }

        private bool CanAboutProgramCommandExecute(object p) => true;

        private void OnAboutProgramCommandExecuted(object p)
        {
            AboutPprogram aboutPprogram = new AboutPprogram();
            aboutPprogram.Show();
        }

        #endregion

        #region Команда вызова окна "Пользовательские настройки"

        public ICommand OpenUserSettingsWindowCommand { get; }

        private bool CanOpenUserSettingsWindowCommandExecute(object p) => true;

        private void OnOpenUserSettingsWindowCommandExecuted(object p)
        {
            UserSettingsWindow userSettingsWindow = new UserSettingsWindow();
            userSettingsWindow.Show();
        }

        #endregion

        #region Команда вызова окна "Автомобили"

        public ICommand OpenListVehicleWindowCommand { get; }

        private bool CanOpenListVehicleWindowCommandExecute(object p) => true;

        private void OnOpenListVehicleWindowCommandExecuted(object p)
        {
            ListVehicleWindow listVehicleWindow = new ListVehicleWindow();
            listVehicleWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Клиенты"

        public ICommand OpenListClientWindowCommand { get; }

        private bool CanOpenListClientWindowCommandExecute(object p) => true;

        private void OnOpenListClientWindowCommandExecuted(object p)
        {
            ListClientWindow listClientWindow = new ListClientWindow();
            listClientWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Тарифы"

        public ICommand OpenListRateWindowCommand { get; }

        private bool CanOpenListRateWindowCommandExecute(object p) => true;

        private void OnOpenListRateWindowCommandExecuted(object p)
        {
            RateWindow rateWindow = new RateWindow();
            rateWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Тарифы"

        public ICommand OpenListAdditionalServicesWindowCommand { get; }

        private bool CanOpenListAdditionalServicesWindowCommandExecute(object p) => true;

        private void OnOpenListAdditionalServicesWindowCommandExecuted(object p)
        {
            AdditionalServicesWindow additionalServicesWindow = new AdditionalServicesWindow();
            additionalServicesWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Страхование"
        
        public ICommand OpenInsurancesInformationWindowCommand { get; }

        private bool CanOpenInsurancesInformationWindowCommandExecute(object p) => true;

        private void OnOpenInsurancesInformationWindowCommandExecuted(object p)
        {
            InsurancesInformationWindow insurancesInformationWindow = new InsurancesInformationWindow();
            insurancesInformationWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
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

            OpenAboutProgramCommand = new LamdaCommand(OnAboutProgramCommandExecuted, CanAboutProgramCommandExecute);

            OpenUserSettingsWindowCommand = new LamdaCommand(OnOpenUserSettingsWindowCommandExecuted, CanOpenUserSettingsWindowCommandExecute);

            OpenListVehicleWindowCommand = new LamdaCommand(OnOpenListVehicleWindowCommandExecuted, CanOpenListVehicleWindowCommandExecute);

            OpenListClientWindowCommand = new LamdaCommand(OnOpenListClientWindowCommandExecuted, CanOpenListClientWindowCommandExecute);

            OpenListRateWindowCommand = new LamdaCommand(OnOpenListRateWindowCommandExecuted, CanOpenListRateWindowCommandExecute);

            OpenListAdditionalServicesWindowCommand = new LamdaCommand(OnOpenListAdditionalServicesWindowCommandExecuted, CanOpenListAdditionalServicesWindowCommandExecute);

            OpenInsurancesInformationWindowCommand = new LamdaCommand(OnOpenInsurancesInformationWindowCommandExecuted, CanOpenInsurancesInformationWindowCommandExecute);

            #endregion
        }
    }
}
