using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Partner.ViewModels.Windows.MainWindowInteraction.ClientWindow
{
    class EditLegalEntityViewModel : ViewModelBase
    {

        #region Данные

        #region Полное наименование организации : FullNameOrganization 

        private string _FullNameOrganization = ListLegalEntity.name_organization;

        /// <summary>FullNameOrganization</summary>
        public string FullNameOrganization
        {
            get => _FullNameOrganization;
            set => Set(ref _FullNameOrganization, value);
        }

        #endregion

        #region Сокращенное наименование организации : AbbreviatedNameOrganization 

        private string _AbbreviatedNameOrganization = ListLegalEntity.abbreviated_name_organization;

        /// <summary>AbbreviatedNameOrganization</summary>
        public string AbbreviatedNameOrganization
        {
            get => _AbbreviatedNameOrganization;
            set => Set(ref _AbbreviatedNameOrganization, value);
        }

        #endregion

        #region Фамилия руководителя : Surname 

        private string _Surname = ListLegalEntity.surname_director;

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя руководителя : Name 

        private string _Name = ListLegalEntity.name_director;

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Отчество руководителя : Patronymic 

        private string _Patronymic = ListLegalEntity.patronymic_director;

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region Юридический адрес : LegalAddress 

        private string _LegalAddress = ListLegalEntity.legal_address;

        /// <summary>LegalAddress</summary>
        public string LegalAddress
        {
            get => _LegalAddress;
            set => Set(ref _LegalAddress, value);
        }

        #endregion

        #region Фактический адрес : ActualLegalAddress 

        private string _ActualLegalAddress = ListLegalEntity.actual_legal_address;

        /// <summary>ActualLegalAddress</summary>
        public string ActualLegalAddress
        {
            get => _ActualLegalAddress;
            set => Set(ref _ActualLegalAddress, value);
        }

        #endregion

        #region ИНН : INN 

        private string _INN = ListLegalEntity.INN;

        /// <summary>INN</summary>
        public string INN
        {
            get => _INN;
            set => Set(ref _INN, value);
        }

        #endregion

        #region КПП : KPP 

        private string _KPP = ListLegalEntity.KPP;

        /// <summary>KPP</summary>
        public string KPP
        {
            get => _KPP;
            set => Set(ref _KPP, value);
        }

        #endregion

        #region ОГРН : OGRN 

        private string _OGRN = ListLegalEntity.OGRN;

        /// <summary>OGRN</summary>
        public string OGRN
        {
            get => _OGRN;
            set => Set(ref _OGRN, value);
        }

        #endregion

        #region Расчетный счет : PaymentAccount 

        private string _PaymentAccount = ListLegalEntity.payment_account;

        /// <summary>PaymentAccount</summary>
        public string PaymentAccount
        {
            get => _PaymentAccount;
            set => Set(ref _PaymentAccount, value);
        }

        #endregion

        #region БИК : BIK 

        private string _BIK = ListLegalEntity.BIK;

        /// <summary>BIK</summary>
        public string BIK
        {
            get => _BIK;
            set => Set(ref _BIK, value);
        }

        #endregion

        #region Email : Email 

        private string _Email = ListLegalEntity.email;

        /// <summary>Email</summary>
        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

        #endregion

        #region Факс : Fax 

        private string _Fax = ListLegalEntity.fax;

        /// <summary>Fax</summary>
        public string Fax
        {
            get => _Fax;
            set => Set(ref _Fax, value);
        }

        #endregion

        #region Номер телефона : PhoneNumber 

        private string _PhoneNumber = ListLegalEntity.phone_number;

        /// <summary>PhoneNumber</summary>
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set => Set(ref _PhoneNumber, value);
        }

        #endregion

        #region Сайт организации : Website 

        private string _Website = ListLegalEntity.website;

        /// <summary>Website</summary>
        public string Website
        {
            get => _Website;
            set => Set(ref _Website, value);
        }

        #endregion

        #region Изображение клиента : Image 

        private object _Image = null;

        /// <summary>Image</summary>
        public object Image
        {
            get => _Image;
            set => Set(ref _Image, value);
        }

        #endregion

        #region Выбранный дополнительный номер : SelectAdditionalPhoneNumber 

        private int _SelectAdditionalPhoneNumber = -1;

        /// <summary>SelectAdditionalPhoneNumber</summary>
        public int SelectAdditionalPhoneNumber
        {
            get => _SelectAdditionalPhoneNumber;
            set => Set(ref _SelectAdditionalPhoneNumber, value);
        }

        #endregion

        #region Выбранный Представитель организации : SelectRepresentativesOrganizations 

        private int _SelectRepresentativesOrganizations = -1;

        /// <summary>SelectRepresentativesOrganizations</summary>
        public int SelectRepresentativesOrganizations
        {
            get => _SelectRepresentativesOrganizations;
            set => Set(ref _SelectRepresentativesOrganizations, value);
        }

        #endregion

        #region List данных дополнительных номеров клиенотов : ListAdditionalData

        private List<ListNaturalPersonAdditionalData> _ListAdditionalData;

        public List<ListNaturalPersonAdditionalData> ListAdditionalData
        {
            get => _ListAdditionalData;
            set => Set(ref _ListAdditionalData, value);
        }

        #endregion

        #region List данных представителей организации : ListRepresentativesOrganization

        private List<ListRepresentativesOrganizationsData> _ListRepresentativesOrganization;

        public List<ListRepresentativesOrganizationsData> ListRepresentativesOrganization
        {
            get => _ListRepresentativesOrganization;
            set => Set(ref _ListRepresentativesOrganization, value);
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Редактирования юридического лица

        public ICommand EditLegalEntityCommand { get; }

        private bool CanEditLegalEntityCommandExecute(object p)
        {
            if ((FullNameOrganization != "") && (AbbreviatedNameOrganization != "") && (Name != "") && (Surname != "") && (Patronymic != "") && (LegalAddress != "") && (ActualLegalAddress != "") && (Email != "") && (Fax != "") && (PhoneNumber != "") && (Website != "") && (INN != "") && (KPP != "") && (OGRN != "") && (PaymentAccount != "") && (BIK != "")
                && (INN.Length == 10) && (KPP.Length == 9) && (OGRN.Length == 15) && (PaymentAccount.Length == 20) && (BIK.Length == 9))
                return true;
            else return false;
        }

        private void OnEditLegalEntityCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            var command = ThisConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Edit_legal_entity";
            command.Parameters.AddWithValue("@id_legal_entity", ListLegalEntity.id_legal_entity);
            command.Parameters.AddWithValue("@name_organization", FullNameOrganization);
            command.Parameters.AddWithValue("@abbreviated_name_organization", AbbreviatedNameOrganization);
            command.Parameters.AddWithValue("@surname_director", Surname);
            command.Parameters.AddWithValue("@name_director", Name);
            command.Parameters.AddWithValue("@patronymic_director", Patronymic);
            command.Parameters.AddWithValue("@legal_address", LegalAddress);
            command.Parameters.AddWithValue("@actual_legal_address", ActualLegalAddress);
            command.Parameters.AddWithValue("@INN", INN);
            command.Parameters.AddWithValue("@KPP", KPP);
            command.Parameters.AddWithValue("@OGRN", OGRN);
            command.Parameters.AddWithValue("@payment_account", PaymentAccount);
            command.Parameters.AddWithValue("@BIK", BIK);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@fax", Fax);
            command.Parameters.AddWithValue("@phone_number", PhoneNumber);
            command.Parameters.AddWithValue("@website", Website);
            command.Parameters.AddWithValue("@image", "NULL");
            command.ExecuteNonQuery();
            ThisConnection.Close();

            MessageBox.Show("Даннае обновлены");

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #region Команда обновления листа дополнительных телефонов

        public ICommand ReturnAdditionalDataCommand { get; }

        private bool CanReturnAdditionalDataCommandExecute(object p) => true;

        private void OnReturnAdditionalDataCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select * from [contact_details_legal_entity] where [id_legal_entity]=" + ListClientProcedure.id_natural_person;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ListAdditionalData = dt.AsEnumerable().Select(se => new ListNaturalPersonAdditionalData() { id_additional_phone_numbers = se.Field<int>("id_contact_details"), phone_number = se.Field<string>("phone_number"), other = se.Field<string>("description") }).ToList();
            ThisConnection.Close();
        }

        #endregion

        #region Команда обновления листа представителей организации

        public ICommand ReturnRepresentativesOrganizationsCommand { get; }

        private bool CanReturnRepresentativesOrganizationsCommandExecute(object p) => true;

        private void OnReturnRepresentativesOrganizationsCommandExecuted(object p)
        {
            DataTable dt2 = new DataTable();

            string connectionString2 = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection2 = new SqlConnection(connectionString2);
            ThisConnection2.Open();
            SqlCommand thisCommand2 = ThisConnection2.CreateCommand();
            thisCommand2.CommandText = "Select * from [representatives_organizations] where [id_legal_entity]=" + ListClientProcedure.id_natural_person;
            SqlDataReader thisReader2 = thisCommand2.ExecuteReader();
            dt2.Load(thisReader2);
            ListRepresentativesOrganization = dt2.AsEnumerable().Select(se => new ListRepresentativesOrganizationsData() { id_representatives_organizations = se.Field<int>("id_representatives_organizations"), Surname = se.Field<string>("surname"), Name = se.Field<string>("name"), Patronymic = se.Field<string>("patronymic"), Post = se.Field<string>("post"), Gender = se.Field<string>("gender"), SeriesPassport = se.Field<string>("series_passport"), NumberPassport = se.Field<string>("number_passport"), PhoneNumber = se.Field<string>("phone_number") }).ToList();
            ThisConnection2.Close();
            for (int i = 0; ListRepresentativesOrganization.Count > i; i++)
            {
                ListRepresentativesOrganization[i].FIO = ListRepresentativesOrganization[i].Surname + " " + ListRepresentativesOrganization[i].Name + " " + ListRepresentativesOrganization[i].Patronymic;
            }
        }

        #endregion

        #region Команда Добавления дополнительного телефона

        public ICommand AddAdditionalDataCommand { get; }

        private bool CanAddAdditionalDataCommandExecute(object p) => true;

        private void OnAddAdditionalDataCommandExecuted(object p)
        {
            EditorAdd.editoradd = "Добавление номера телефона";
            EditorAdd.phone_number = "";
            EditorAdd.other = "";

            AdditionalDataClientWindow additionalDataClientWindow = new AdditionalDataClientWindow();
            additionalDataClientWindow.ShowDialog();

            if (EditorAdd.editoradd == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_additional_phone_numbers_legal_entity";
                command.Parameters.AddWithValue("@id_legal_entity", ListLegalEntity.id_legal_entity);
                command.Parameters.AddWithValue("@phone_number", EditorAdd.phone_number);
                command.Parameters.AddWithValue("@description", EditorAdd.other);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnAdditionalDataCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Удаления дополнительного телефона

        public ICommand DropAdditionalDataCommand { get; }

        private bool CanDropAdditionalDataCommandExecute(object p) => SelectAdditionalPhoneNumber >= 0;

        private void OnDropAdditionalDataCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_additional_phone_numbers_legal_entity";
                command.Parameters.AddWithValue("@id_contact_details", ListAdditionalData[SelectAdditionalPhoneNumber].id_additional_phone_numbers);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnAdditionalDataCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Редактирования дополнительного телефона

        public ICommand EditAdditionalDataCommand { get; }

        private bool CanEditAdditionalDataCommandExecute(object p) => SelectAdditionalPhoneNumber >= 0;

        private void OnEditAdditionalDataCommandExecuted(object p)
        {
            EditorAdd.editoradd = "Редактирование номера телефона";
            EditorAdd.phone_number = Convert.ToString(ListAdditionalData[SelectAdditionalPhoneNumber].phone_number);
            EditorAdd.other = Convert.ToString(ListAdditionalData[SelectAdditionalPhoneNumber].other);
            int ID = ListAdditionalData[SelectAdditionalPhoneNumber].id_additional_phone_numbers;

            AdditionalDataClientWindow additionalDataClientWindow = new AdditionalDataClientWindow();
            additionalDataClientWindow.ShowDialog();

            if (EditorAdd.editoradd == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Edit_additional_phone_numbers_legal_entity";
                command2.Parameters.AddWithValue("@id_contact_details", ID);
                command2.Parameters.AddWithValue("@phone_number", EditorAdd.phone_number);
                command2.Parameters.AddWithValue("@description", EditorAdd.other);
                command2.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnAdditionalDataCommand.Execute(null);
            }
        }

        #endregion
 
        #region Команд Добавления представителя организации

        public ICommand AddRepresentativesOrganizationsCommand { get; }

        private bool CanAddRepresentativesOrganizationsCommandExecute(object p) => true;

        private void OnAddRepresentativesOrganizationsCommandExecuted(object p)
        {
            ListRepresentativesOrganizations.genderMan = true;
            ListRepresentativesOrganizations.editoradd = "Добавление представителя организации";
            ListRepresentativesOrganizations.Name = "";
            ListRepresentativesOrganizations.Surname = "";
            ListRepresentativesOrganizations.Patronymic = "";
            ListRepresentativesOrganizations.Post = "";
            ListRepresentativesOrganizations.Gender = "";
            ListRepresentativesOrganizations.SeriesPassport = "";
            ListRepresentativesOrganizations.NumberPassport = "";
            ListRepresentativesOrganizations.PhoneNumber = "";

            RepresentativesOrganizationsWindow representativesOrganizationsWindow = new RepresentativesOrganizationsWindow();
            representativesOrganizationsWindow.ShowDialog();

            if (ListRepresentativesOrganizations.editoradd == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();;
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Add_representatives_organizations";
                command2.Parameters.AddWithValue("@id_legal_entity", ListClientProcedure.id_natural_person);
                command2.Parameters.AddWithValue("@surname", ListRepresentativesOrganizations.Surname);
                command2.Parameters.AddWithValue("@name", ListRepresentativesOrganizations.Name);
                command2.Parameters.AddWithValue("@patronymic", ListRepresentativesOrganizations.Patronymic);
                command2.Parameters.AddWithValue("@post", ListRepresentativesOrganizations.Post);
                command2.Parameters.AddWithValue("@gender", ListRepresentativesOrganizations.Gender);
                command2.Parameters.AddWithValue("@series_passport", ListRepresentativesOrganizations.SeriesPassport);
                command2.Parameters.AddWithValue("@number_passport", ListRepresentativesOrganizations.NumberPassport);
                command2.Parameters.AddWithValue("@phone_number", ListRepresentativesOrganizations.PhoneNumber);
                command2.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                command2.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                command2.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnRepresentativesOrganizationsCommand.Execute(null);
            }
        }

        #endregion

        #region Команда Редактирования представителя организаии

        public ICommand EditRepresentativesOrganizationsCommand { get; }

        private bool CanEditRepresentativesOrganizationsCommandExecute(object p) => SelectRepresentativesOrganizations >= 0;

        private void OnEditRepresentativesOrganizationsCommandExecuted(object p)
        {
            ListRepresentativesOrganizations.editoradd = "Редактирование представителя организации";
            ListRepresentativesOrganizations.Name = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Name;
            ListRepresentativesOrganizations.Surname = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Surname;
            ListRepresentativesOrganizations.Patronymic = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Patronymic;
            ListRepresentativesOrganizations.Post = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Post;
            ListRepresentativesOrganizations.Gender = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Gender;
            ListRepresentativesOrganizations.SeriesPassport = ListRepresentativesOrganization[SelectRepresentativesOrganizations].SeriesPassport;
            ListRepresentativesOrganizations.NumberPassport = ListRepresentativesOrganization[SelectRepresentativesOrganizations].NumberPassport;
            ListRepresentativesOrganizations.PhoneNumber = ListRepresentativesOrganization[SelectRepresentativesOrganizations].PhoneNumber;
            int ID = ListRepresentativesOrganization[SelectRepresentativesOrganizations].id_representatives_organizations;

            if (ListRepresentativesOrganization[SelectRepresentativesOrganizations].Gender == "Мужской")
            { ListRepresentativesOrganizations.genderMan = true; ListRepresentativesOrganizations.genderWoman = false; }
            else { ListRepresentativesOrganizations.genderMan = false; ListRepresentativesOrganizations.genderWoman = true; }

            RepresentativesOrganizationsWindow representativesOrganizationsWindow = new RepresentativesOrganizationsWindow();
            representativesOrganizationsWindow.ShowDialog();

            if (ListRepresentativesOrganizations.editoradd == "Прошло успешно")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand(); ;
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Edit_representatives_organizations";
                command2.Parameters.AddWithValue("@id_representatives_organizations", ID);
                command2.Parameters.AddWithValue("@surname", ListRepresentativesOrganizations.Surname);
                command2.Parameters.AddWithValue("@name", ListRepresentativesOrganizations.Name);
                command2.Parameters.AddWithValue("@patronymic", ListRepresentativesOrganizations.Patronymic);
                command2.Parameters.AddWithValue("@post", ListRepresentativesOrganizations.Post);
                command2.Parameters.AddWithValue("@gender", ListRepresentativesOrganizations.Gender);
                command2.Parameters.AddWithValue("@series_passport", ListRepresentativesOrganizations.SeriesPassport);
                command2.Parameters.AddWithValue("@number_passport", ListRepresentativesOrganizations.NumberPassport);
                command2.Parameters.AddWithValue("@phone_number", ListRepresentativesOrganizations.PhoneNumber);
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }

            ReturnRepresentativesOrganizationsCommand.Execute(null);
        }

        #endregion

        #region Команда Удаления представителя организации
        public ICommand DropRepresentativesOrganizationsCommand { get; }

        private bool CanDropRepresentativesOrganizationsCommandExecute(object p) => SelectRepresentativesOrganizations >= 0;

        private void OnDropRepresentativesOrganizationsCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить безвозвратно данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Drop_representatives_organizations";
                command.Parameters.AddWithValue("@id_representatives_organizations", ListRepresentativesOrganization[SelectRepresentativesOrganizations].id_representatives_organizations);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                ReturnRepresentativesOrganizationsCommand.Execute(null);
            }
        }

        #endregion    

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public EditLegalEntityViewModel()
        {
            #region Команды

            EditLegalEntityCommand = new LamdaCommand(OnEditLegalEntityCommandExecuted, CanEditLegalEntityCommandExecute);

            ReturnAdditionalDataCommand = new LamdaCommand(OnReturnAdditionalDataCommandExecuted, CanReturnAdditionalDataCommandExecute);

            DropAdditionalDataCommand = new LamdaCommand(OnDropAdditionalDataCommandExecuted, CanDropAdditionalDataCommandExecute);

            EditAdditionalDataCommand = new LamdaCommand(OnEditAdditionalDataCommandExecuted, CanEditAdditionalDataCommandExecute);

            DropRepresentativesOrganizationsCommand = new LamdaCommand(OnDropRepresentativesOrganizationsCommandExecuted, CanDropRepresentativesOrganizationsCommandExecute);

            EditRepresentativesOrganizationsCommand = new LamdaCommand(OnEditRepresentativesOrganizationsCommandExecuted, CanEditRepresentativesOrganizationsCommandExecute);

            AddRepresentativesOrganizationsCommand = new LamdaCommand(OnAddRepresentativesOrganizationsCommandExecuted, CanAddRepresentativesOrganizationsCommandExecute);

            AddAdditionalDataCommand = new LamdaCommand(OnAddAdditionalDataCommandExecuted, CanAddAdditionalDataCommandExecute);

            ReturnRepresentativesOrganizationsCommand = new LamdaCommand(OnReturnRepresentativesOrganizationsCommandExecuted, CanReturnRepresentativesOrganizationsCommandExecute);

            #endregion

            #region Загрузка данных

            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select * from [contact_details_legal_entity] where [id_legal_entity]=" + ListClientProcedure.id_natural_person;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ListAdditionalData = dt.AsEnumerable().Select(se => new ListNaturalPersonAdditionalData() { id_additional_phone_numbers = se.Field<int>("id_contact_details"), phone_number = se.Field<string>("phone_number"), other = se.Field<string>("description") }).ToList();
            ThisConnection.Close();

            DataTable dt2 = new DataTable();

            string connectionString2 = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection2 = new SqlConnection(connectionString2);
            ThisConnection2.Open();
            SqlCommand thisCommand2 = ThisConnection2.CreateCommand();
            thisCommand2.CommandText = "Select * from [representatives_organizations] where [id_legal_entity]=" + ListClientProcedure.id_natural_person;
            SqlDataReader thisReader2 = thisCommand2.ExecuteReader();
            dt2.Load(thisReader2);
            ListRepresentativesOrganization = dt2.AsEnumerable().Select(se => new ListRepresentativesOrganizationsData() { id_representatives_organizations = se.Field<int>("id_representatives_organizations"), Surname = se.Field<string>("surname"), Name = se.Field<string>("name"), Patronymic = se.Field<string>("patronymic"), Post = se.Field<string>("post"), Gender = se.Field<string>("gender"), SeriesPassport = se.Field<string>("series_passport"), NumberPassport = se.Field<string>("number_passport"), PhoneNumber = se.Field<string>("phone_number") }).ToList();
            ThisConnection2.Close();
            for (int i = 0; ListRepresentativesOrganization.Count > i; i++)
            {
                ListRepresentativesOrganization[i].FIO = ListRepresentativesOrganization[i].Surname + " " + ListRepresentativesOrganization[i].Name + " " + ListRepresentativesOrganization[i].Patronymic;
            }

            #endregion

        }
    }
}
