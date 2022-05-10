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
    class AddLegalEntityViewModel : ViewModelBase
    {

        #region Данные

        #region Полное наименование организации : FullNameOrganization 

        private string _FullNameOrganization = "";

        /// <summary>FullNameOrganization</summary>
        public string FullNameOrganization
        {
            get => _FullNameOrganization;
            set => Set(ref _FullNameOrganization, value);
        }

        #endregion

        #region Сокращенное наименование организации : AbbreviatedNameOrganization 

        private string _AbbreviatedNameOrganization = "";

        /// <summary>AbbreviatedNameOrganization</summary>
        public string AbbreviatedNameOrganization
        {
            get => _AbbreviatedNameOrganization;
            set => Set(ref _AbbreviatedNameOrganization, value);
        }

        #endregion

        #region Фамилия руководителя : Surname 

        private string _Surname = "";

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя руководителя : Name 

        private string _Name = "";

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Отчество руководителя : Patronymic 

        private string _Patronymic = "";

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region Юридический адрес : LegalAddress 

        private string _LegalAddress = "";

        /// <summary>LegalAddress</summary>
        public string LegalAddress
        {
            get => _LegalAddress;
            set => Set(ref _LegalAddress, value);
        }

        #endregion

        #region Фактический адрес : ActualLegalAddress 

        private string _ActualLegalAddress = "";

        /// <summary>ActualLegalAddress</summary>
        public string ActualLegalAddress
        {
            get => _ActualLegalAddress;
            set => Set(ref _ActualLegalAddress, value);
        }

        #endregion

        #region ИНН : INN 

        private string _INN = "";

        /// <summary>INN</summary>
        public string INN
        {
            get => _INN;
            set => Set(ref _INN, value);
        }

        #endregion

        #region КПП : KPP 

        private string _KPP = "";

        /// <summary>KPP</summary>
        public string KPP
        {
            get => _KPP;
            set => Set(ref _KPP, value);
        }

        #endregion

        #region ОГРН : OGRN 

        private string _OGRN = "";

        /// <summary>OGRN</summary>
        public string OGRN
        {
            get => _OGRN;
            set => Set(ref _OGRN, value);
        }

        #endregion

        #region Расчетный счет : PaymentAccount 

        private string _PaymentAccount = "";

        /// <summary>PaymentAccount</summary>
        public string PaymentAccount
        {
            get => _PaymentAccount;
            set => Set(ref _PaymentAccount, value);
        }

        #endregion

        #region БИК : BIK 

        private string _BIK = "";

        /// <summary>BIK</summary>
        public string BIK
        {
            get => _BIK;
            set => Set(ref _BIK, value);
        }

        #endregion

        #region Email : Email 

        private string _Email = "";

        /// <summary>Email</summary>
        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

        #endregion

        #region Факс : Fax 

        private string _Fax = "";

        /// <summary>Fax</summary>
        public string Fax
        {
            get => _Fax;
            set => Set(ref _Fax, value);
        }

        #endregion

        #region Номер телефона : PhoneNumber 

        private string _PhoneNumber = "";

        /// <summary>PhoneNumber</summary>
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set => Set(ref _PhoneNumber, value);
        }

        #endregion

        #region Сайт организации : Website 

        private string _Website = "";

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

        #region Переменные row, column

        DataColumn column;
        DataRow row;

        #endregion

        #region Данные дополнительных телефонных номеров : AdditionalDataLegalEntityPhoneNumber

        private DataTable _AdditionalDataLegalEntityPhoneNumber;

        /// <summary>AdditionalDataLegalEntityPhoneNumber</summary>
        public DataTable AdditionalDataLegalEntityPhoneNumber
        {
            get => _AdditionalDataLegalEntityPhoneNumber;
            set => Set(ref _AdditionalDataLegalEntityPhoneNumber, value);
        }

        public DataTable AdditionalDataLegalEntityPhoneNumberDelivery = new DataTable("AdditionalDataClientPhoneNumberDelivery");

        #endregion

        #region Данные представителей организации : RepresentativesOrganizations

        private DataTable _RepresentativesOrganizations;

        /// <summary>RepresentativesOrganizations</summary>
        public DataTable RepresentativesOrganizations
        {
            get => _RepresentativesOrganizations;
            set => Set(ref _RepresentativesOrganizations, value);
        }

        public DataTable RepresentativesOrganizationsDelivery = new DataTable("RepresentativesOrganizationsDelivery");

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

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавления юридического лица

        public ICommand AddLegalEntityCommand { get; }

        private bool CanAddLegalEntityCommandExecute(object p)
        {
            if ((FullNameOrganization != "") && (AbbreviatedNameOrganization != "") && (Name != "") && (Surname != "") && (Patronymic != "") && (LegalAddress != "") && (ActualLegalAddress != "") && (Email != "") && (Fax != "") && (PhoneNumber != "") && (Website != "") && (INN != "") && (KPP != "") && (OGRN != "") && (PaymentAccount != "") && (BIK != "")
                && (INN.Length == 10) && (KPP.Length == 9) && (OGRN.Length == 15) && (PaymentAccount.Length == 20) && (BIK.Length == 9))
                return true;
            else return false;
        }

        private void OnAddLegalEntityCommandExecuted(object p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            var command = ThisConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Add_legal_entity";
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
            command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
            command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
            command.ExecuteNonQuery();
            ThisConnection.Close();

            int LegalEntity;

            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select id_legal_entity from legal_entity where INN = '" + INN + "' and KPP = '" + KPP + "' and OGRN = '" + OGRN + "'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();

            LegalEntity = Convert.ToInt32(thisReader["id_legal_entity"].ToString());

            thisReader.Close();
            ThisConnection.Close();

            ThisConnection.Open();
            
            if (AdditionalDataLegalEntityPhoneNumberDelivery.Rows.Count >= 1)
            {
                for (int i = 1; i <= AdditionalDataLegalEntityPhoneNumberDelivery.Rows.Count; i++)
                {
                    var command2 = ThisConnection.CreateCommand();
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Add_additional_phone_numbers_legal_entity";
                    command2.Parameters.AddWithValue("@id_legal_entity", LegalEntity);
                    command2.Parameters.AddWithValue("@phone_number", Convert.ToString(AdditionalDataLegalEntityPhoneNumberDelivery.Rows[i - 1]["phone_number"]));
                    command2.Parameters.AddWithValue("@description", Convert.ToString(AdditionalDataLegalEntityPhoneNumberDelivery.Rows[i - 1]["other"]));
                    command2.ExecuteNonQuery();
                }
            }

            if (RepresentativesOrganizationsDelivery.Rows.Count >= 1)
            {
                for (int i = 1; i <= RepresentativesOrganizationsDelivery.Rows.Count; i++)
                {
                    var command2 = ThisConnection.CreateCommand();
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.CommandText = "Add_representatives_organizations";
                    command2.Parameters.AddWithValue("@id_legal_entity", LegalEntity);
                    command2.Parameters.AddWithValue("@surname", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["Surname"]));
                    command2.Parameters.AddWithValue("@name", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["Name"]));
                    command2.Parameters.AddWithValue("@patronymic", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["Patronymic"]));
                    command2.Parameters.AddWithValue("@post", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["Post"]));
                    command2.Parameters.AddWithValue("@gender", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["Gender"]));
                    command2.Parameters.AddWithValue("@series_passport", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["SeriesPassport"]));
                    command2.Parameters.AddWithValue("@number_passport", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["NumberPassport"]));
                    command2.Parameters.AddWithValue("@phone_number", Convert.ToString(RepresentativesOrganizations.Rows[i - 1]["PhoneNumber"]));
                    command2.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                    command2.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                    command2.ExecuteNonQuery();
                }
            }

            ThisConnection.Close();

            MessageBox.Show("Даннае добавлены");

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
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
                row = AdditionalDataLegalEntityPhoneNumberDelivery.NewRow();
                row["phone_number"] = EditorAdd.phone_number;
                row["other"] = EditorAdd.other;
                AdditionalDataLegalEntityPhoneNumberDelivery.Rows.Add(row);
            }

            AdditionalDataLegalEntityPhoneNumber = AdditionalDataLegalEntityPhoneNumberDelivery;
        }

        #endregion

        #region Команда Удаления дополнительного телефона

        public ICommand DropAdditionalDataCommand { get; }

        private bool CanDropAdditionalDataCommandExecute(object p) => SelectAdditionalPhoneNumber >= 0;

        private void OnDropAdditionalDataCommandExecuted(object p)
        {
            DataRow b = AdditionalDataLegalEntityPhoneNumber.Rows[SelectAdditionalPhoneNumber];
            AdditionalDataLegalEntityPhoneNumberDelivery.Rows.Remove(b);
            AdditionalDataLegalEntityPhoneNumber = AdditionalDataLegalEntityPhoneNumberDelivery;
        }

        #endregion

        #region Команда Редактирования дополнительного телефона

        public ICommand EditAdditionalDataCommand { get; }

        private bool CanEditAdditionalDataCommandExecute(object p) => SelectAdditionalPhoneNumber >= 0;

        private void OnEditAdditionalDataCommandExecuted(object p)
        {
            EditorAdd.editoradd = "Редактирование номера телефона";
            EditorAdd.phone_number = Convert.ToString(AdditionalDataLegalEntityPhoneNumber.Rows[SelectAdditionalPhoneNumber]["phone_number"]);
            EditorAdd.other = Convert.ToString(AdditionalDataLegalEntityPhoneNumber.Rows[SelectAdditionalPhoneNumber]["other"]);

            AdditionalDataClientWindow additionalDataClientWindow = new AdditionalDataClientWindow();
            additionalDataClientWindow.ShowDialog();

            if (EditorAdd.editoradd == "Прошло успешно")
            {
                AdditionalDataLegalEntityPhoneNumberDelivery.Rows[SelectAdditionalPhoneNumber]["phone_number"] = EditorAdd.phone_number;
                AdditionalDataLegalEntityPhoneNumberDelivery.Rows[SelectAdditionalPhoneNumber]["other"] = EditorAdd.other;
            }

            AdditionalDataLegalEntityPhoneNumber = AdditionalDataLegalEntityPhoneNumberDelivery;
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
                row = RepresentativesOrganizationsDelivery.NewRow();
                row["FIO"] = ListRepresentativesOrganizations.FIO;
                row["Surname"] = ListRepresentativesOrganizations.Surname;
                row["Name"] = ListRepresentativesOrganizations.Name;
                row["Patronymic"] = ListRepresentativesOrganizations.Patronymic;
                row["Post"] = ListRepresentativesOrganizations.Post;
                row["Gender"] = ListRepresentativesOrganizations.Gender;
                row["SeriesPassport"] = ListRepresentativesOrganizations.SeriesPassport;
                row["NumberPassport"] = ListRepresentativesOrganizations.NumberPassport;
                row["PhoneNumber"] = ListRepresentativesOrganizations.PhoneNumber;
                RepresentativesOrganizationsDelivery.Rows.Add(row);

                RepresentativesOrganizations = RepresentativesOrganizationsDelivery;
            }
        }

        #endregion

        #region Команда Редактирования представителя организаии

        public ICommand EditRepresentativesOrganizationsCommand { get; }

        private bool CanEditRepresentativesOrganizationsCommandExecute(object p) => SelectRepresentativesOrganizations >= 0;

        private void OnEditRepresentativesOrganizationsCommandExecuted(object p)
        {
            ListRepresentativesOrganizations.editoradd = "Редактирование представителя организации";
            ListRepresentativesOrganizations.Name = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["Name"]);
            ListRepresentativesOrganizations.Surname = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["Surname"]);
            ListRepresentativesOrganizations.Patronymic = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["Patronymic"]); ;
            ListRepresentativesOrganizations.Post = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["Post"]);
            ListRepresentativesOrganizations.Gender = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["Gender"]);
            ListRepresentativesOrganizations.SeriesPassport = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["SeriesPassport"]);
            ListRepresentativesOrganizations.NumberPassport = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["NumberPassport"]);
            ListRepresentativesOrganizations.PhoneNumber = Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["PhoneNumber"]); ;
            if (Convert.ToString(RepresentativesOrganizations.Rows[SelectRepresentativesOrganizations]["Gender"]) == "Мужской")
            { ListRepresentativesOrganizations.genderMan = true; ListRepresentativesOrganizations.genderWoman = false; }
            else { ListRepresentativesOrganizations.genderMan = false; ListRepresentativesOrganizations.genderWoman = true; }

            RepresentativesOrganizationsWindow representativesOrganizationsWindow = new RepresentativesOrganizationsWindow();
            representativesOrganizationsWindow.ShowDialog();

            if (ListRepresentativesOrganizations.editoradd == "Прошло успешно")
            {
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["Name"] = ListRepresentativesOrganizations.Name;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["Surname"] = ListRepresentativesOrganizations.Surname;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["Patronymic"] = ListRepresentativesOrganizations.Patronymic;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["Post"] = ListRepresentativesOrganizations.Post;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["Gender"] = ListRepresentativesOrganizations.Gender;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["SeriesPassport"] = ListRepresentativesOrganizations.SeriesPassport;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["NumberPassport"] = ListRepresentativesOrganizations.NumberPassport;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["PhoneNumber"] = ListRepresentativesOrganizations.PhoneNumber;
                RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations]["FIO"] = ListRepresentativesOrganizations.Surname + " " + ListRepresentativesOrganizations.Name + " " + ListRepresentativesOrganizations.Patronymic;
                RepresentativesOrganizations = RepresentativesOrganizationsDelivery;
            }
        }

        #endregion

        #region Команда Удаления представителя организации
        public ICommand DropRepresentativesOrganizationsCommand { get; }

        private bool CanDropRepresentativesOrganizationsCommandExecute(object p) => SelectRepresentativesOrganizations >= 0;

        private void OnDropRepresentativesOrganizationsCommandExecuted(object p)
        {
            DataRow b = RepresentativesOrganizationsDelivery.Rows[SelectRepresentativesOrganizations];
            RepresentativesOrganizationsDelivery.Rows.Remove(b);
            RepresentativesOrganizations = RepresentativesOrganizationsDelivery;
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddLegalEntityViewModel()
        {

            #region Команды

            AddLegalEntityCommand = new LamdaCommand(OnAddLegalEntityCommandExecuted, CanAddLegalEntityCommandExecute);

            AddAdditionalDataCommand = new LamdaCommand(OnAddAdditionalDataCommandExecuted, CanAddAdditionalDataCommandExecute);

            DropAdditionalDataCommand = new LamdaCommand(OnDropAdditionalDataCommandExecuted, CanDropAdditionalDataCommandExecute);

            EditAdditionalDataCommand = new LamdaCommand(OnEditAdditionalDataCommandExecuted, CanEditAdditionalDataCommandExecute);

            AddRepresentativesOrganizationsCommand = new LamdaCommand(OnAddRepresentativesOrganizationsCommandExecuted, CanAddRepresentativesOrganizationsCommandExecute);

            EditRepresentativesOrganizationsCommand = new LamdaCommand(OnEditRepresentativesOrganizationsCommandExecuted, CanEditRepresentativesOrganizationsCommandExecute);

            DropRepresentativesOrganizationsCommand = new LamdaCommand(OnDropRepresentativesOrganizationsCommandExecuted, CanDropRepresentativesOrganizationsCommandExecute);

            #endregion

            #region Создание колонок таблицы AdditionalDataLegalEntityPhoneNumberDelivery

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "phone_number";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalDataLegalEntityPhoneNumberDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "other";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalDataLegalEntityPhoneNumberDelivery.Columns.Add(column);

            #endregion

            #region Создание колонок таблицы RepresentativesOrganizationsDelivery

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FIO";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Surname";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Patronymic";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Post";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Gender";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "SeriesPassport";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NumberPassport";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PhoneNumber";
            column.ReadOnly = false;
            column.Unique = false;
            RepresentativesOrganizationsDelivery.Columns.Add(column);

            #endregion

        }
    }
}
