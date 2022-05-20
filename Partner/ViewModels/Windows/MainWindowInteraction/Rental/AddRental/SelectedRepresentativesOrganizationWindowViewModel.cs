using Partner.Infrastructure.Commands;
using Partner.Models.Client;
using Partner.Models.PersonalData;
using Partner.Models.Rental;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.ClientWindow;
using Partner.Views.Windows.MainWindowInteraction.Rental.AddRental;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Rental.AddRental
{
    internal class SelectedRepresentativesOrganizationWindowViewModel : ViewModelBase
    {
        #region Данные

        #region List данных представителей организации : ListRepresentativesOrganization

        private List<ListRepresentativesOrganizationsData> _ListRepresentativesOrganization;

        public List<ListRepresentativesOrganizationsData> ListRepresentativesOrganization
        {
            get => _ListRepresentativesOrganization;
            set => Set(ref _ListRepresentativesOrganization, value);
        }

        #endregion

        #region Выбранный Представитель организации : SelectRepresentativesOrganizations 

        private int _SelectRepresentativesOrganizations = 0;

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

        #region Команда вызывающая окно "Добавление аренды" : OpenAddRentalWindowCommand

        public ICommand OpenAddRentalWindowCommand { get; }

        private bool CanOpenAddRentalWindowCommandExecute(object p) => SelectRepresentativesOrganizations > -1;

        private void OnOpenAddRentalWindowCommandExecute(object p)
        {
            if (DataStaticRental.Edit == "Выбор представителя для подписания акта выдачи")
            {
                DataStaticRental.Edit = "Прошло успешно";
                DataStaticRental.IDRepresentativesOrganization = ListRepresentativesOrganization[SelectRepresentativesOrganizations].id_representatives_organizations;
                DataStaticRental.FIORepresentativesOrganization = ListRepresentativesOrganization[SelectRepresentativesOrganizations].FIO;
                DataStaticRental.PostRepresentativesOrganization = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Post;
            }
            else
            {
                DataStaticRental.IDRepresentativesOrganization = ListRepresentativesOrganization[SelectRepresentativesOrganizations].id_representatives_organizations;
                DataStaticRental.FIORepresentativesOrganization = ListRepresentativesOrganization[SelectRepresentativesOrganizations].FIO;
                DataStaticRental.PostRepresentativesOrganization = ListRepresentativesOrganization[SelectRepresentativesOrganizations].Post;

                AddRentalWindow addRentalWindow = new AddRentalWindow();
                addRentalWindow.Show();
            }
            
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        #endregion

        #region Команд Добавления представителя организации : AddRepresentativesOrganizationsCommand

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
                var command2 = ThisConnection.CreateCommand(); ;
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Add_representatives_organizations";
                command2.Parameters.AddWithValue("@id_legal_entity", DataStaticRental.IDClient);
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

        #region Команд Обновления данных представителей организации : ReturnRepresentativesOrganizationsCommand

        public ICommand ReturnRepresentativesOrganizationsCommand { get; }

        private bool CanReturnRepresentativesOrganizationsCommandExecute(object p) => true;

        private void OnReturnRepresentativesOrganizationsCommandExecuted(object p)
        {
            DataTable dt2 = new DataTable();

            string connectionString2 = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection2 = new SqlConnection(connectionString2);
            ThisConnection2.Open();
            SqlCommand thisCommand2 = ThisConnection2.CreateCommand();
            thisCommand2.CommandText = "Select * from [representatives_organizations] where [id_legal_entity]=" + DataStaticRental.IDClient;
            SqlDataReader thisReader2 = thisCommand2.ExecuteReader();
            dt2.Load(thisReader2);
            ListRepresentativesOrganization = dt2.AsEnumerable().Select(se => new ListRepresentativesOrganizationsData() { id_representatives_organizations = se.Field<int>("id_representatives_organizations"), Surname = se.Field<string>("surname"), Name = se.Field<string>("name"), Patronymic = se.Field<string>("patronymic"), Post = se.Field<string>("post"), Gender = se.Field<string>("gender"), SeriesPassport = se.Field<string>("series_passport"), NumberPassport = se.Field<string>("number_passport"), PhoneNumber = se.Field<string>("phone_number") }).ToList();
            ThisConnection2.Close();
            for (int i = 0; ListRepresentativesOrganization.Count > i; i++)
            {
                ListRepresentativesOrganization[i].num = i + 1;
                ListRepresentativesOrganization[i].FIO = ListRepresentativesOrganization[i].Surname + " " + ListRepresentativesOrganization[i].Name + " " + ListRepresentativesOrganization[i].Patronymic;
            }
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public SelectedRepresentativesOrganizationWindowViewModel()
        {
            #region Команды

            OpenAddRentalWindowCommand = new LamdaCommand(OnOpenAddRentalWindowCommandExecute, CanOpenAddRentalWindowCommandExecute);

            AddRepresentativesOrganizationsCommand = new LamdaCommand(OnAddRepresentativesOrganizationsCommandExecuted, CanAddRepresentativesOrganizationsCommandExecute);

            ReturnRepresentativesOrganizationsCommand = new LamdaCommand(OnReturnRepresentativesOrganizationsCommandExecuted, CanReturnRepresentativesOrganizationsCommandExecute);

            #endregion

            #region Данные

            DataTable dt2 = new DataTable();

            string connectionString2 = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection2 = new SqlConnection(connectionString2);
            ThisConnection2.Open();
            SqlCommand thisCommand2 = ThisConnection2.CreateCommand();
            thisCommand2.CommandText = "(Select id_representatives_organizations,surname,name,patronymic,post,phone_number from [representatives_organizations] where  [representatives_organizations].[id_legal_entity] = " + DataStaticRental.IDClient + ") UNION (Select - 1, surname_director, name_director, patronymic_director, 'Директор' as post, phone_number from legal_entity where [id_legal_entity] = " + DataStaticRental.IDClient + ")";
            SqlDataReader thisReader2 = thisCommand2.ExecuteReader();
            dt2.Load(thisReader2);
            ListRepresentativesOrganization = dt2.AsEnumerable().Select(se => new ListRepresentativesOrganizationsData() { id_representatives_organizations = se.Field<int>("id_representatives_organizations"), Surname = se.Field<string>("surname"), Name = se.Field<string>("name"), Patronymic = se.Field<string>("patronymic"), Post = se.Field<string>("post"),PhoneNumber = se.Field<string>("phone_number") }).ToList();
            ThisConnection2.Close();

            for (int i = 0; ListRepresentativesOrganization.Count > i; i++)
            {
                ListRepresentativesOrganization[i].num = i + 1;
                ListRepresentativesOrganization[i].FIO = ListRepresentativesOrganization[i].Surname + " " + ListRepresentativesOrganization[i].Name + " " + ListRepresentativesOrganization[i].Patronymic;
            }

            #endregion
        }
    }
}
