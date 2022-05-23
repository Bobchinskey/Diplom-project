using Partner.Infrastructure.Commands;
using Partner.Models.InformativeWindowModels;
using Partner.Models.News;
using Partner.ViewModels.Base;
using Partner.Views.Windows.MainWindowInteraction.News;
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
    internal class ListNewsWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Коллекция новостного блока : List<NewsModel> News

        private List<NewsModel> _News;

        public List<NewsModel> News
        {
            get => _News;
            set => Set(ref _News, value);
        }

        #endregion

        #region Выбранный элемент : List<NewsModel> SelectedNews

        private int _SelectedNews;

        public int SelectedNews
        {
            get => _SelectedNews;
            set => Set(ref _SelectedNews, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды 

        #region Команда Фильтрации Новостей : FilterNewsCommand

        public ICommand FilterNewsCommand { get; }

        private bool CanFilterNewsCommandExecute(object p) => true;

        private void OnFilterNewsCommandExecuted(object p)
        {
            DataTable news = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select [id_news],[date_publication],[heading],[news] from [News] ORDER BY [id_news] desc";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            news.Load(thisReader);
            News = news.AsEnumerable().Select(se => new NewsModel() { id_news = se.Field<int>("id_news"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), news = se.Field<string>("News") }).ToList();

            ThisConnection.Close();
        }

        #endregion

        #region Команда Удаления новости : DropNewsCommand

        public ICommand DropNewsCommand { get; }

        private bool CanDropNewsCommandExecute(object p) => SelectedNews > -1;

        private void OnDropNewsCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Drop_news";
                command2.Parameters.AddWithValue("@id_news", News[SelectedNews].id_news);
                command2.ExecuteNonQuery();

                ThisConnection.Close();
            }
            
            FilterNewsCommand.Execute(null);
        }

        #endregion

        #region Команда Вызова окна "Добавление новостей" : OpenAddNewsWindowCommand

        public ICommand OpenAddNewsWindowCommand { get; }

        private bool CanOpenAddNewsWindowCommandExecute(object p) => true;

        private void OnOpenAddNewsWindowCommandExecuted(object p)
        {
            DataNewsModel.Title = "Добавление новости";

            AddNewsWindow addNewsWindow = new AddNewsWindow();
            addNewsWindow.ShowDialog();

            FilterNewsCommand.Execute(null);
        }

        #endregion

        #region Команда Вызова окна "Редактирование новостей" : OpenEditNewsWindowCommand

        public ICommand OpenEditNewsWindowCommand { get; }

        private bool CanOpenEditNewsWindowCommandExecute(object p) => SelectedNews > -1;

        private void OnOpenEditNewsWindowCommandExecuted(object p)
        {
            DataNewsModel.Id = News[SelectedNews].id_news;
            DataNewsModel.Title = "Редактирование новости";

            AddNewsWindow addNewsWindow = new AddNewsWindow();
            addNewsWindow.ShowDialog();

            FilterNewsCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListNewsWindowViewModel()
        {
            #region Команды 

            OpenEditNewsWindowCommand = new LamdaCommand(OnOpenEditNewsWindowCommandExecuted, CanOpenEditNewsWindowCommandExecute);

            OpenAddNewsWindowCommand = new LamdaCommand(OnOpenAddNewsWindowCommandExecuted, CanOpenAddNewsWindowCommandExecute);

            FilterNewsCommand = new LamdaCommand(OnFilterNewsCommandExecuted, CanFilterNewsCommandExecute);

            DropNewsCommand = new LamdaCommand(OnDropNewsCommandExecuted, CanDropNewsCommandExecute);

            #endregion

            #region Данные

            DataTable news = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select [id_news],[date_publication],[heading],[news] from [News] ORDER BY [id_news] desc";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            news.Load(thisReader);
            News = news.AsEnumerable().Select(se => new NewsModel() { id_news = se.Field<int>("id_news"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), news = se.Field<string>("News") }).ToList();
            
            ThisConnection.Close();

            #endregion
        }
    }
}
