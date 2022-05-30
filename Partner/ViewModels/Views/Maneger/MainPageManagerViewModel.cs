using Partner.Infrastructure.Commands;
using Partner.Models.PersonalData;
using Partner.Models.Reports;
using Partner.Models.Vehicle;
using Partner.ViewModels.Base;
using Partner.ViewModels.Windows;
using Partner.Views.Views.Manager;
using Partner.Views.Views.Manager.Pages;
using Partner.Views.Windows;
using Partner.Views.Windows.InformativeWindows;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
using Partner.Views.Windows.MainWindowInteraction.Insurances;
using Partner.Views.Windows.MainWindowInteraction.Maintenances;
using Partner.Views.Windows.MainWindowInteraction.Rates;
using Partner.Views.Windows.MainWindowInteraction.Rates.AdditionalServices;
using Partner.Views.Windows.MainWindowInteraction.Rental;
using Partner.Views.Windows.MainWindowInteraction.Reports;
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
        #region Данные

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда смены пользователя : ChangeUserCommand

        public ICommand ChangeUserCommand { get; }

        private bool CanChangeUserCommandExecute(object p) => true;

        private void OnChangeUserCommandExecuted(object p)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion

        #region Команда обновления данных : UpdatePageCommand

        public ICommand UpdatePageCommand { get; }

        private bool CanUpdatePageCommandExecute(object p) => true;

        private void OnUpdatePageCommandExecuted(object p)
        {
            Application.Current.Resources["Content"] = new MainPageRental();
        }

        #endregion

        #region Команда вызова окна "О программе" : OpenAboutProgramCommand

        public ICommand OpenAboutProgramCommand { get; }

        private bool CanAboutProgramCommandExecute(object p) => true;

        private void OnAboutProgramCommandExecuted(object p)
        {
            AboutPprogram aboutPprogram = new AboutPprogram();
            aboutPprogram.Show();
        }

        #endregion

        #region Команда вызова окна "Пользовательские настройки" : OpenUserSettingsWindowCommand

        public ICommand OpenUserSettingsWindowCommand { get; }

        private bool CanOpenUserSettingsWindowCommandExecute(object p) => true;

        private void OnOpenUserSettingsWindowCommandExecuted(object p)
        {
            UserSettingsWindow userSettingsWindow = new UserSettingsWindow();
            userSettingsWindow.ShowDialog();
        }

        #endregion

        #region Команда вызова окна "Автомобили" : OpenListVehicleWindowCommand

        public ICommand OpenListVehicleWindowCommand { get; }

        private bool CanOpenListVehicleWindowCommandExecute(object p) => true;

        private void OnOpenListVehicleWindowCommandExecuted(object p)
        {
            ListVehicleWindow listVehicleWindow = new ListVehicleWindow();
            listVehicleWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Клиенты" : OpenListClientWindowCommand

        public ICommand OpenListClientWindowCommand { get; }

        private bool CanOpenListClientWindowCommandExecute(object p) => true;

        private void OnOpenListClientWindowCommandExecuted(object p)
        {
            ListClientWindow listClientWindow = new ListClientWindow();
            listClientWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Тарифы" : OpenListRateWindowCommand

        public ICommand OpenListRateWindowCommand { get; }

        private bool CanOpenListRateWindowCommandExecute(object p) => true;

        private void OnOpenListRateWindowCommandExecuted(object p)
        {
            RateWindow rateWindow = new RateWindow();
            rateWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Дополнительные услуги" : OpenListAdditionalServicesWindowCommand

        public ICommand OpenListAdditionalServicesWindowCommand { get; }

        private bool CanOpenListAdditionalServicesWindowCommandExecute(object p) => true;

        private void OnOpenListAdditionalServicesWindowCommandExecuted(object p)
        {
            AdditionalServicesWindow additionalServicesWindow = new AdditionalServicesWindow();
            additionalServicesWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Страхование" : OpenInsurancesInformationWindowCommand

        public ICommand OpenInsurancesInformationWindowCommand { get; }

        private bool CanOpenInsurancesInformationWindowCommandExecute(object p) => true;

        private void OnOpenInsurancesInformationWindowCommandExecuted(object p)
        {
            InsurancesInformationWindow insurancesInformationWindow = new InsurancesInformationWindow();
            insurancesInformationWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Техническое обслуживание" : OpeMaintenanceWindowCommand

        public ICommand OpeMaintenanceWindowCommand { get; }

        private bool CanOpenMaintenanceWindowCommandExecute(object p) => true;

        private void OnOpenMaintenanceWindowCommandExecuted(object p)
        {
            SelectVehicleMaintenanceWindow selectVehicleMaintenanceWindow = new SelectVehicleMaintenanceWindow();
            selectVehicleMaintenanceWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Аренды" : OpenRentalWindowCommand

        public ICommand OpenRentalWindowCommand { get; }

        private bool CanOpenRentalWindowCommandExecute(object p) => true;

        private void OnOpenRentalWindowCommandExecuted(object p)
        {
            VehicleDataModel.EditOrAdd = "Аренда";

            SelectVehicleRentalWindow selectVehicleRentalWindow = new SelectVehicleRentalWindow();
            selectVehicleRentalWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Бронирование" : OpenListBookingWindowCommand

        public ICommand OpenListBookingWindowCommand { get; }

        private bool CanOpenListBookingWindowCommandExecute(object p) => true;

        private void OnOpenListBookingWindowCommandExecuted(object p)
        {
            VehicleDataModel.EditOrAdd = "Бронирование";

            SelectVehicleRentalWindow selectVehicleRentalWindow = new SelectVehicleRentalWindow();
            selectVehicleRentalWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #region Команда вызова окна "Отчеты", Отчет по колличеству аренд : OpenListImportantInformationWindowCommand

        public ICommand OpenReportNumberRentsWindowCommand { get; }

        private bool CanOpenReportNumberRentsWindowCommandExecute(object p) => true;

        private void OnOpenReportNumberRentsWindowCommandExecuted(object p)
        {
            ReportDataModel.Title = "Отчет по количеству аренд";

            ReportWindow reportWindow = new ReportWindow();
            reportWindow.ShowDialog();

            UpdatePageCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public MainPageManagerViewModel()
        {
            #region Команды

            OpenReportNumberRentsWindowCommand = new LamdaCommand(OnOpenReportNumberRentsWindowCommandExecuted, CanOpenReportNumberRentsWindowCommandExecute);

            ChangeUserCommand = new LamdaCommand(OnChangeUserCommandExecuted, CanChangeUserCommandExecute);

            UpdatePageCommand = new LamdaCommand(OnUpdatePageCommandExecuted, CanUpdatePageCommandExecute);

            OpenAboutProgramCommand = new LamdaCommand(OnAboutProgramCommandExecuted, CanAboutProgramCommandExecute);

            OpenUserSettingsWindowCommand = new LamdaCommand(OnOpenUserSettingsWindowCommandExecuted, CanOpenUserSettingsWindowCommandExecute);

            OpenListVehicleWindowCommand = new LamdaCommand(OnOpenListVehicleWindowCommandExecuted, CanOpenListVehicleWindowCommandExecute);

            OpenListClientWindowCommand = new LamdaCommand(OnOpenListClientWindowCommandExecuted, CanOpenListClientWindowCommandExecute);

            OpenListRateWindowCommand = new LamdaCommand(OnOpenListRateWindowCommandExecuted, CanOpenListRateWindowCommandExecute);

            OpenListAdditionalServicesWindowCommand = new LamdaCommand(OnOpenListAdditionalServicesWindowCommandExecuted, CanOpenListAdditionalServicesWindowCommandExecute);

            OpenInsurancesInformationWindowCommand = new LamdaCommand(OnOpenInsurancesInformationWindowCommandExecuted, CanOpenInsurancesInformationWindowCommandExecute);

            OpeMaintenanceWindowCommand = new LamdaCommand(OnOpenMaintenanceWindowCommandExecuted, CanOpenMaintenanceWindowCommandExecute);

            OpenRentalWindowCommand = new LamdaCommand(OnOpenRentalWindowCommandExecuted, CanOpenRentalWindowCommandExecute);

            OpenListBookingWindowCommand = new LamdaCommand(OnOpenListBookingWindowCommandExecuted, CanOpenListBookingWindowCommandExecute);

            #endregion

            #region Данные

            Application.Current.Resources["Content"] = new MainPageRental();

            #endregion
        }
    }
}
