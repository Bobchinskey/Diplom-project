using Partner.Models.PersonalData;
using Partner.ViewModels.Base;

namespace Partner.ViewModels.Windows
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Начальная страница : UserDataModel.page

        private object _StartPageRoles = UserDataModel.page;

        /// <summary>Login</summary>
        public object StartPageRoles
        {
            get => _StartPageRoles;
            set => Set(ref _StartPageRoles, value);
        }

        #endregion
    }
}
