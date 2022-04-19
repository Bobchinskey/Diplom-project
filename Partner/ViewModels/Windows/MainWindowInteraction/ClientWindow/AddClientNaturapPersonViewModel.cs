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
    class AddClientNaturapPersonViewModel : ViewModelBase
    {

        #region Данные

        #region Заголовок окна : Title 

        private string _Title = ListClientProcedure.EditOrAdd;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Пол мужской : IsCheckedMan 

        private bool _IsCheckedMan = true;

        /// <summary>IsCheckedMan</summary>
        public bool IsCheckedMan
        {
            get => _IsCheckedMan;
            set => Set(ref _IsCheckedMan, value);
        }

        #endregion

        #region Фамилия клиента : Surname 

        private string _Surname = ListClientProcedure.surname;

        /// <summary>Surname</summary>
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        #endregion

        #region Имя клиента : Name 

        private string _Name = ListClientProcedure.name;

        /// <summary>Name</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        #endregion

        #region Отчество клиента : Patronymic 

        private string _Patronymic = ListClientProcedure.patronymic;

        /// <summary>Patronymic</summary>
        public string Patronymic
        {
            get => _Patronymic;
            set => Set(ref _Patronymic, value);
        }

        #endregion

        #region День рождение : Birthday 

        private DateTime _Birthday = ListClientProcedure.birthday;

        /// <summary>Birthday</summary>
        public DateTime Birthday
        {
            get => _Birthday;
            set => Set(ref _Birthday, value);
        }

        #endregion

        #region Место рождения : PlaceBirthday 

        private string _PlaceBirthday = ListClientProcedure.place_birthday;

        /// <summary>PlaceBirthday</summary>
        public string PlaceBirthday
        {
            get => _PlaceBirthday;
            set => Set(ref _PlaceBirthday, value);
        }

        #endregion

        #region ИНН клиента : INN 

        private string _INN = ListClientProcedure.INN;

        /// <summary>INN</summary>
        public string INN
        {
            get => _INN;
            set => Set(ref _INN, value);
        }

        #endregion

        #region Серия паспорта : SeriesPassport 

        private string _SeriesPassport = ListClientProcedure.series_passport;

        /// <summary>SeriesPassport</summary>
        public string SeriesPassport
        {
            get => _SeriesPassport;
            set => Set(ref _SeriesPassport, value);
        }

        #endregion

        #region Номер паспорта : NumberPassport 

        private string _NumberPassport = ListClientProcedure.number_passport;

        /// <summary>NumberPassport</summary>
        public string NumberPassport
        {
            get => _NumberPassport;
            set => Set(ref _NumberPassport, value);
        }

        #endregion

        #region Кем выдан паспорт : WhoIssuedPassport 

        private string _WhoIssuedPassport = ListClientProcedure.who_issued_passport;

        /// <summary>WhoIssuedPassport</summary>
        public string WhoIssuedPassport
        {
            get => _WhoIssuedPassport;
            set => Set(ref _WhoIssuedPassport, value);
        }

        #endregion

        #region Дата выдачи паспорта : DateIssuedPassport 

        private DateTime _DateIssuedPassport = ListClientProcedure.date_issued_passport;

        /// <summary>DateIssuedPassport</summary>
        public DateTime DateIssuedPassport
        {
            get => _DateIssuedPassport;
            set => Set(ref _DateIssuedPassport, value);
        }

        #endregion

        #region Номер банковской карты : NumberCard 

        private string _NumberCard = ListClientProcedure.number_card;

        /// <summary>NumberCard</summary>
        public string NumberCard
        {
            get => _NumberCard;
            set => Set(ref _NumberCard, value);
        }

        #endregion

        #region Дата окончания срока действия банковской карты : ValidityPeriodCard 

        private DateTime _ValidityPeriodCard = ListClientProcedure.validity_period_card;

        /// <summary>ValidityPeriodCard</summary>
        public DateTime ValidityPeriodCard
        {
            get => _ValidityPeriodCard;
            set => Set(ref _ValidityPeriodCard, value);
        }

        #endregion

        #region Код защиты банковской карты : CVC2 

        private string _CVC2 = ListClientProcedure.CVC2;

        /// <summary>CVC2</summary>
        public string CVC2
        {
            get => _CVC2;
            set => Set(ref _CVC2, value);
        }

        #endregion

        #region Прописка : Registration 

        private string _Registration = ListClientProcedure.registration;

        /// <summary>Registration</summary>
        public string Registration
        {
            get => _Registration;
            set => Set(ref _Registration, value);
        }

        #endregion

        #region Фактические место проживания : ActualPlaceResidence 

        private string _ActualPlaceResidence = ListClientProcedure.actual_place_residence;

        /// <summary>ActualPlaceResidence</summary>
        public string ActualPlaceResidence
        {
            get => _ActualPlaceResidence;
            set => Set(ref _ActualPlaceResidence, value);
        }

        #endregion

        #region Номер телефона : PhoneNumber 

        private string _PhoneNumber = ListClientProcedure.phone_number;

        /// <summary>PhoneNumber</summary>
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set => Set(ref _PhoneNumber, value);
        }

        #endregion

        #region Email : Email 

        private string _Email = ListClientProcedure.email;

        /// <summary>Email</summary>
        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
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

        #region Статус клиента : Reality 

        private string _Reality = ListClientProcedure.reality;

        /// <summary>Reality</summary>
        public string Reality
        {
            get => _Reality;
            set => Set(ref _Reality, value);
        }

        #endregion

        #region Данные дополнительных телефонныйх номеров : AdditionalDataClientPhoneNumber

        private DataTable _AdditionalDataClientPhoneNumber;

        /// <summary>AdditionalDataClientPhoneNumber</summary>
        public DataTable AdditionalDataClientPhoneNumber
        {
            get => _AdditionalDataClientPhoneNumber;
            set => Set(ref _AdditionalDataClientPhoneNumber, value);
        }

        public DataTable AdditionalDataClientPhoneNumberDelivery = new DataTable("AdditionalDataClientPhoneNumberDelivery");
        DataColumn column;
        DataRow row;

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


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

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

            row = AdditionalDataClientPhoneNumberDelivery.NewRow();
            row["phone_number"] = EditorAdd.phone_number;
            row["other"] = EditorAdd.other;
            AdditionalDataClientPhoneNumberDelivery.Rows.Add(row);

            AdditionalDataClientPhoneNumber = AdditionalDataClientPhoneNumberDelivery;
        }

        #endregion

        #region Команда Редактирования дополнительного телефона

        public ICommand EditAdditionalDataCommand { get; }

        private bool CanEditAdditionalDataCommandExecute(object p) => SelectAdditionalPhoneNumber >= 0;

        private void OnEditAdditionalDataCommandExecuted(object p)
        {
            EditorAdd.editoradd = "Редактирование номера телефона";
            EditorAdd.phone_number = Convert.ToString(AdditionalDataClientPhoneNumber.Rows[SelectAdditionalPhoneNumber]["phone_number"]);
            EditorAdd.other = Convert.ToString(AdditionalDataClientPhoneNumber.Rows[SelectAdditionalPhoneNumber]["other"]);

            AdditionalDataClientWindow additionalDataClientWindow = new AdditionalDataClientWindow();
            additionalDataClientWindow.ShowDialog();

            AdditionalDataClientPhoneNumber.Rows[SelectAdditionalPhoneNumber]["phone_number"] = EditorAdd.phone_number;
            AdditionalDataClientPhoneNumber.Rows[SelectAdditionalPhoneNumber]["other"] = EditorAdd.other;
            AdditionalDataClientPhoneNumberDelivery.Rows[SelectAdditionalPhoneNumber]["phone_number"] = EditorAdd.phone_number;
            AdditionalDataClientPhoneNumberDelivery.Rows[SelectAdditionalPhoneNumber]["other"] = EditorAdd.other;
        }

        #endregion

        #region Команда Удаления дополнительного телефона

        public ICommand DropAdditionalDataCommand { get; }

        private bool CanDropAdditionalDataCommandExecute(object p) => SelectAdditionalPhoneNumber >= 0;

        private void OnDropAdditionalDataCommandExecuted(object p)
        {
            DataRow b = AdditionalDataClientPhoneNumber.Rows[SelectAdditionalPhoneNumber];
            AdditionalDataClientPhoneNumberDelivery.Rows.Remove(b);
            AdditionalDataClientPhoneNumber.Rows.Remove(b);
        }

        #endregion

        #region Команда Добавления нового клиента

        public ICommand AddClientCommand { get; }

        private bool CanAddClientCommandExecute(object p)
        {
            if ((Surname != "") && (DateIssuedPassport < DateTime.Today) && (Name != "") && (Patronymic != "") && ((DateTime.Today.Year - Birthday.Year) > 18) && (PlaceBirthday != "") && (PhoneNumber != "") && (PhoneNumber.Length == 11) && (Email != "") && (INN != "") && (INN.Length == 12) && (SeriesPassport != "") && (SeriesPassport.Length == 4) && (NumberPassport != "") && (NumberPassport.Length == 6) && (Registration != "") && (ActualPlaceResidence != "") && (NumberCard != "") && (ValidityPeriodCard > DateTime.Today) && (CVC2 != "") && (CVC2.Length == 3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnAddClientCommandExecuted(object p)
        {
            string gender;

            if (IsCheckedMan)
                gender = "Мужской";
            else
                gender = "Женский";

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            var command = ThisConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Add_natural_person";
            command.Parameters.AddWithValue("@surname", Surname);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@patronymic", Patronymic);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@birthday", Birthday);
            command.Parameters.AddWithValue("@place_birthday", PlaceBirthday);
            command.Parameters.AddWithValue("@INN", INN);
            command.Parameters.AddWithValue("@series_passport", SeriesPassport);
            command.Parameters.AddWithValue("@number_passport", NumberPassport);
            command.Parameters.AddWithValue("@who_issued_passport", WhoIssuedPassport);
            command.Parameters.AddWithValue("@date_issued_passport", DateIssuedPassport);
            command.Parameters.AddWithValue("@number_card", NumberCard);
            command.Parameters.AddWithValue("@validity_period_card", ValidityPeriodCard);
            command.Parameters.AddWithValue("@CVC2", CVC2);
            command.Parameters.AddWithValue("@registration", Registration);
            command.Parameters.AddWithValue("@actual_place_residence", ActualPlaceResidence);
            command.Parameters.AddWithValue("@phone_number", PhoneNumber);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@image", "NULL");
            command.Parameters.AddWithValue("@reality", "Актуально");
            command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
            command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
            command.ExecuteNonQuery();
            ThisConnection.Close();

            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select id_natural_person from natural_person where series_passport = '"+ SeriesPassport + "' and number_passport = '"+ NumberPassport +"'";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            thisReader.Read();

            ListClientProcedure.id_natural_person = Convert.ToInt32(thisReader["id_natural_person"].ToString());

            thisReader.Close();
            ThisConnection.Close();

            for (int i = 1; i <= AdditionalDataClientPhoneNumber.Rows.Count; i++)
            {
                ThisConnection.Open();
                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Add_additional_phone_numbers";
                command2.Parameters.AddWithValue("@id_natural_person", ListClientProcedure.id_natural_person);
                command2.Parameters.AddWithValue("@phone_number", Convert.ToString(AdditionalDataClientPhoneNumber.Rows[i - 1]["phone_number"]));
                command2.Parameters.AddWithValue("@other", Convert.ToString(AdditionalDataClientPhoneNumber.Rows[i - 1]["other"]));
                command2.ExecuteNonQuery();
                ThisConnection.Close();
            }

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


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddClientNaturapPersonViewModel()
        {
            #region Команды

            AddAdditionalDataCommand = new LamdaCommand(OnAddAdditionalDataCommandExecuted, CanAddAdditionalDataCommandExecute);

            EditAdditionalDataCommand = new LamdaCommand(OnEditAdditionalDataCommandExecuted, CanEditAdditionalDataCommandExecute);

            DropAdditionalDataCommand = new LamdaCommand(OnDropAdditionalDataCommandExecuted, CanDropAdditionalDataCommandExecute);

            AddClientCommand = new LamdaCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);

            #endregion

            #region Создание колонок таблицы AdditionalDataClientPhoneNumberDelivery

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "phone_number";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalDataClientPhoneNumberDelivery.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "other";
            column.ReadOnly = false;
            column.Unique = false;
            AdditionalDataClientPhoneNumberDelivery.Columns.Add(column);

            #endregion

            if (ListClientProcedure.EditOrAdd == "Редактирование клиента") 
            {
                
            }
        }

    }
}
