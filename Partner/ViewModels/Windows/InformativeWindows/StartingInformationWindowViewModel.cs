using Partner.Models.InformativeWindowModels;
using Partner.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.ViewModels.Windows.InformativeWindows
{
    class StartingInformationWindowViewModel : ViewModelBase
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

        #region Коллекция блока важной информации : List<ImportantInformationModel> ImportantInformation

        private List<ImportantInformationModel> _ImportantInformation;

        public List<ImportantInformationModel> ImportantInformation
        {
            get => _ImportantInformation;
            set => Set(ref _ImportantInformation, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public StartingInformationWindowViewModel()
        {
            DataTable news = new DataTable();
            DataTable importantInformation = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select Top(5) [id_news],[date_publication],[heading],[news] from [News] ORDER BY [id_news] desc";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            news.Load(thisReader);
            News = news.AsEnumerable().Select(se => new NewsModel() { id_news = se.Field<int>("id_news"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), news = se.Field<string>("News")}).ToList();
            thisCommand.CommandText = "select Top(5) [id_important_information],[date_publication],[heading],[important_information] from [Important_information] ORDER BY [id_important_information] desc";
            SqlDataReader thisReader2 = thisCommand.ExecuteReader();
            importantInformation.Load(thisReader2);
            ImportantInformation = importantInformation.AsEnumerable().Select(se => new ImportantInformationModel() { id_important_information = se.Field<int>("id_important_information"), date_publication = se.Field<DateTime>("date_publication"), heading = se.Field<string>("heading"), important_information = se.Field<string>("important_information")}).ToList();
            ThisConnection.Close();
        }
    }
}
