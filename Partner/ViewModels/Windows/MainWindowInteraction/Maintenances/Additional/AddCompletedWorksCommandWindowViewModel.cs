using Partner.Infrastructure.Commands;
using Partner.Models.Maintenances;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.Maintenances.Additional
{
    class AddCompletedWorksCommandWindowViewModel : ViewModelBase
    {

        #region Данные

        #region Заголовок окна: Title

        private string _Title = CompletedWorks.Title;

        /// <summary>Title</summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Наименование работы: NameWork

        private string _NameWork = CompletedWorks.NameWork;

        /// <summary>NameWork</summary>

        public string NameWork
        {
            get => _NameWork;
            set => Set(ref _NameWork, value);
        }

        #endregion

        #region Стоимость работы: CostWork

        private string _CostWork = CompletedWorks.Cost;

        /// <summary>CostWork</summary>

        public string CostWork
        {
            get => _CostWork;
            set => Set(ref _CostWork, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда добавления выполненных работ

        public ICommand AddCompletedWorkCommand { get; }

        private bool CanAddCompletedWorkCommandExecute(object p)
        {
            if ((NameWork != "") && (CostWork != ""))
                return true;
            else return false;
        }

        private void OnAddCompletedWorkCommandExecute(object p)
        {
            CompletedWorks.Title = "Прошло успешно";
            CompletedWorks.NameWork = NameWork;
            CompletedWorks.Cost = CostWork;

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddCompletedWorksCommandWindowViewModel()
        {
            #region Команды

            AddCompletedWorkCommand = new LamdaCommand(OnAddCompletedWorkCommandExecute, CanAddCompletedWorkCommandExecute);

            #endregion
        }
    }
}
