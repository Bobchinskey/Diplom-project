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
    internal class ListImportantInformationWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Коллекция блока важной информации : List<ImportantInformationModel> ImportantInformation

        private List<ImportantInformationModel> _ImportantInformation;

        public List<ImportantInformationModel> ImportantInformation
        {
            get => _ImportantInformation;
            set => Set(ref _ImportantInformation, value);
        }

        #endregion

        #region Выбранный элемент : List<NewsModel> SelectedImportantInformation

        private int _SelectedImportantInformation;

        public int SelectedImportantInformation
        {
            get => _SelectedImportantInformation;
            set => Set(ref _SelectedImportantInformation, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Фильтрации Важной информации : FilterImportantInformationCommand

        public ICommand FilterImportantInformationCommand { get; }

        private bool CanFilterImportantInformationCommandExecute(object p) => true;

        private void OnFilterImportantInformationCommandExecuted(object p)
        {
            DataTable importantInformation = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();

            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select [id_important_information],[date_publication],[heading],[important_information] from [Important_information] ORDER BY [id_important_information] desc";
            SqlDataReader thisReader2 = thisCommand.ExecuteReader();
            importantInformation.Load(thisReader2);
            ImportantInformation = importantInformation.AsEnumerable().Select(se => new ImportantInformationModel() { id_important_information = se.Field<int>("id_important_information"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), important_information = se.Field<string>("important_information") }).ToList();
            
            ThisConnection.Close();
        }

        #endregion

        #region Команда Удаления Важной информации : DropImportantInformationCommand

        public ICommand DropImportantInformationCommand { get; }

        private bool CanDropImportantInformationCommandExecute(object p) => SelectedImportantInformation > -1;

        private void OnDropImportantInformationCommandExecuted(object p)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данную запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();

                var command2 = ThisConnection.CreateCommand();
                command2.CommandType = CommandType.StoredProcedure;
                command2.CommandText = "Drop_important_information";
                command2.Parameters.AddWithValue("@id_important_information", ImportantInformation[SelectedImportantInformation].id_important_information);
                command2.ExecuteNonQuery();

                ThisConnection.Close();
            }

            FilterImportantInformationCommand.Execute(null);
        }

        #endregion

        #region Команда Вызова окна "Добавление Важной информации" : OpenAddImportantInformationWindowCommand

        public ICommand OpenAddImportantInformationWindowCommand { get; }

        private bool CanOpenAddImportantInformationWindowCommandExecute(object p) => true;

        private void OnOpenAddImportantInformationWindowCommandExecuted(object p)
        {
            DataNewsModel.Title = "Добавление важной информации";

            AddNewsWindow addNewsWindow = new AddNewsWindow();
            addNewsWindow.ShowDialog();

            FilterImportantInformationCommand.Execute(null);
        }

        #endregion

        #region Команда Вызова окна "Редактирование Важной информации" : OpenEditImportantInformationWindowCommand

        public ICommand OpenEditImportantInformationWindowCommand { get; }

        private bool CanOpenEditImportantInformationWindowCommandExecute(object p) => SelectedImportantInformation > -1;

        private void OnOpenEditImportantInformationWindowCommandExecuted(object p)
        {
            DataNewsModel.Id = ImportantInformation[SelectedImportantInformation].id_important_information;
            DataNewsModel.Title = "Редактирование важной информации";

            AddNewsWindow addNewsWindow = new AddNewsWindow();
            addNewsWindow.ShowDialog();

            FilterImportantInformationCommand.Execute(null);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public ListImportantInformationWindowViewModel()
        {
            #region Команды

            OpenEditImportantInformationWindowCommand = new LamdaCommand(OnOpenEditImportantInformationWindowCommandExecuted, CanOpenEditImportantInformationWindowCommandExecute);

            OpenAddImportantInformationWindowCommand = new LamdaCommand(OnOpenAddImportantInformationWindowCommandExecuted, CanOpenAddImportantInformationWindowCommandExecute);

            DropImportantInformationCommand = new LamdaCommand(OnDropImportantInformationCommandExecuted, CanDropImportantInformationCommandExecute);

            FilterImportantInformationCommand = new LamdaCommand(OnFilterImportantInformationCommandExecuted, CanFilterImportantInformationCommandExecute);

            #endregion

            #region Данные

            DataTable importantInformation = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select [id_important_information],[date_publication],[heading],[important_information] from [Important_information] ORDER BY [id_important_information] desc";
            SqlDataReader thisReader2 = thisCommand.ExecuteReader();
            importantInformation.Load(thisReader2);
            ImportantInformation = importantInformation.AsEnumerable().Select(se => new ImportantInformationModel() { id_important_information = se.Field<int>("id_important_information"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), important_information = se.Field<string>("important_information") }).ToList();
            
            ThisConnection.Close();

            #endregion
        }
    }
}
