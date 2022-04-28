using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.ClientWindow
{
    class AdditionalDataClientWindowViewModel : ViewModelBase
    {

        #region Данные

        #region Заголовок окна : Title 

        private string _Title = EditorAdd.editoradd;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Номер телефона : Numberphone 

        private string _Numberphone = EditorAdd.phone_number;

        /// <summary>Numberphone</summary>
        public string Numberphone
        {
            get => _Numberphone;
            set => Set(ref _Numberphone, value);
        }

        #endregion

        #region Описание : Other 

        private string _Other = EditorAdd.other;

        /// <summary>Other</summary>
        public string Other
        {
            get => _Other;
            set => Set(ref _Other, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавления дополнительного телефона

        public ICommand AddAdditionalDataCommand { get; }

        private bool CanAddAdditionalDataCommandExecute(object p)
        {
            if (Numberphone != "") return true;
            else return false;
        }

        private void OnAddAdditionalDataCommandExecuted(object p)
        {
            EditorAdd.editoradd = "Прошло успешно";
            EditorAdd.phone_number = Numberphone;
            EditorAdd.other = Other;

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

        public AdditionalDataClientWindowViewModel()
        {
            #region Команды

            AddAdditionalDataCommand = new LamdaCommand(OnAddAdditionalDataCommandExecuted, CanAddAdditionalDataCommandExecute);

            #endregion
        }
    }
}
