using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.ClientWindow
{
    class RepresentativesOrganizationsWindowViewModel : ViewModelBase
    {

        #region Данные

        string Gender;

        #region Заголовок окна : Title 

        private string _Title = ListRepresentativesOrganizations.editoradd;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Фамилия : Surname 

        private string _Surname = ListRepresentativesOrganizations.Surname;

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя : Name 

        private string _Name = ListRepresentativesOrganizations.Name;

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Номер телефона : PhoneNumber 

        private string _PhoneNumber = ListRepresentativesOrganizations.PhoneNumber;

        /// <summary>PhoneNumber</summary>
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set => Set(ref _PhoneNumber, value);
        }

        #endregion

        #region Заголовок окна : Patronymic 

        private string _Patronymic = ListRepresentativesOrganizations.Patronymic;

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region Пол мужской : IsCheckedMan 

        private bool _IsCheckedMan = ListRepresentativesOrganizations.genderMan;

        /// <summary>IsCheckedMan</summary>
        public bool IsCheckedMan
        {
            get => _IsCheckedMan;
            set => Set(ref _IsCheckedMan, value);
        }

        #endregion

        #region Пол мужской : IsCheckedWoman 

        private bool _IsCheckedWoman = ListRepresentativesOrganizations.genderWoman;

        /// <summary>IsCheckedMan</summary>
        public bool IsCheckedWoman
        {
            get => _IsCheckedWoman;
            set => Set(ref _IsCheckedWoman, value);
        }

        #endregion

        #region Должность : Post 

        private string _Post = ListRepresentativesOrganizations.Post;

        /// <summary>Title</summary>
        public string Post
        {
            get => _Post;
            set => Set(ref _Post, value);
        }

        #endregion

        #region Серия паспорта : SeriesPassport 

        private string _SeriesPassport = ListRepresentativesOrganizations.SeriesPassport;

        /// <summary>SeriesPassport</summary>
        public string SeriesPassport
        {
            get => _SeriesPassport;
            set => Set(ref _SeriesPassport, value);
        }

        #endregion

        #region Номер паспорта : NumberPassport 

        private string _NumberPassport = ListRepresentativesOrganizations.NumberPassport;

        /// <summary>NumberPassport</summary>
        public string NumberPassport
        {
            get => _NumberPassport;
            set => Set(ref _NumberPassport, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавления представителя организации

        public ICommand AddAdditionalDataCommand { get; }

        private bool CanAddAdditionalDataCommandExecute(object p)
        {
            if ((Surname != "") && (Name != "") && (Patronymic != "") && (Post != "") && (Gender != "") && (SeriesPassport != "") && (SeriesPassport.Length == 4) && (NumberPassport != "") && (NumberPassport.Length == 6) && (PhoneNumber != "") && (PhoneNumber.Length == 11)) 
                return true;
            else return false;
        }

        private void OnAddAdditionalDataCommandExecuted(object p)
        {
            if (IsCheckedMan)
                Gender = "Мужской";
            else Gender = "Женский";
                
            ListRepresentativesOrganizations.editoradd = "Прошло успешно";
            ListRepresentativesOrganizations.Surname = Surname;
            ListRepresentativesOrganizations.Name = Name;
            ListRepresentativesOrganizations.Patronymic = Patronymic;
            ListRepresentativesOrganizations.FIO = Surname + " " + Name + " " + Patronymic;
            ListRepresentativesOrganizations.Post = Post;
            ListRepresentativesOrganizations.Gender = Gender;
            ListRepresentativesOrganizations.SeriesPassport = SeriesPassport;
            ListRepresentativesOrganizations.NumberPassport = NumberPassport;
            ListRepresentativesOrganizations.PhoneNumber = PhoneNumber;

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

        public RepresentativesOrganizationsWindowViewModel()
        {
            #region Команды

            AddAdditionalDataCommand = new LamdaCommand(OnAddAdditionalDataCommandExecuted, CanAddAdditionalDataCommandExecute);

            #endregion
        }
    }
}
