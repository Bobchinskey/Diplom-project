using Partner.Infrastructure.Commands;
using Partner.Models.News;
using Partner.Models.PersonalData;
using Partner.ViewModels.Base;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.News
{
    internal class AddNewsWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Заголовок окна : Title 

        private string _Title = DataNewsModel.Title;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Заголовок : Header 

        private string _Header = "";

        /// <summary>Header</summary>
        public string Header
        {
            get => _Header;
            set => Set(ref _Header, value);
        }

        #endregion

        #region Описание : Description 

        private string _Description = "";

        /// <summary>Description</summary>
        public string Description
        {
            get => _Description;
            set => Set(ref _Description, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавления данных в БД : AddNewsDataCommand

        public ICommand AddNewsDataCommand { get; }

        private bool CanAddNewsDataCommandExecute(object p)
        {
            if ((Header != "") && (Description != ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnAddNewsDataCommandExecuted(object p)
        {
            if (DataNewsModel.Title == "Добавление новости")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_news";
                command.Parameters.AddWithValue("@date_publication", DateTime.Today);
                command.Parameters.AddWithValue("@heading", Header);
                command.Parameters.AddWithValue("@news", Description);
                command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае добавлены");
            }
            else if (DataNewsModel.Title == "Редактирование новости")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_news";
                command.Parameters.AddWithValue("@id_news", DataNewsModel.Id);
                command.Parameters.AddWithValue("@heading", Header);
                command.Parameters.AddWithValue("@news", Description);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае Сохранены");
            }
            else if (DataNewsModel.Title == "Добавление важной информации")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Add_important_information";
                command.Parameters.AddWithValue("@date_publication", DateTime.Today);
                command.Parameters.AddWithValue("@heading", Header);
                command.Parameters.AddWithValue("@important_information", Description);
                command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае добавлены");
            }
            else if (DataNewsModel.Title == "Редактирование важной информации")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                var command = ThisConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Edit_important_information";
                command.Parameters.AddWithValue("@id_important_information", DataNewsModel.Id);
                command.Parameters.AddWithValue("@heading", Header);
                command.Parameters.AddWithValue("@important_information", Description);
                command.ExecuteNonQuery();
                ThisConnection.Close();

                MessageBox.Show("Даннае Сохранены");
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


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddNewsWindowViewModel()
        {
            #region Команды

            AddNewsDataCommand = new LamdaCommand(OnAddNewsDataCommandExecuted, CanAddNewsDataCommandExecute);

            #endregion

            #region Данные

            if (DataNewsModel.Title == "Редактирование новости")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from [news] where id_news = " + DataNewsModel.Id;
                SqlDataReader thisReader = thisCommand.ExecuteReader();

                thisReader.Read();

                Header = thisReader["heading"].ToString();
                Description = thisReader["news"].ToString();

                thisReader.Close();

                ThisConnection.Close();
            }
            else if (DataNewsModel.Title == "Редактирование важной информации")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from [important_information] where id_important_information = " + DataNewsModel.Id;
                SqlDataReader thisReader = thisCommand.ExecuteReader();

                thisReader.Read();

                Header = thisReader["heading"].ToString();
                Description = thisReader["important_information"].ToString();

                thisReader.Close();

                ThisConnection.Close();
            }
                
            #endregion
        }
    }
}
