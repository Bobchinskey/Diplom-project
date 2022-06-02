using Partner.Infrastructure.Commands;
using Partner.Models.Fine;
using Partner.Models.Vehicle;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.Fine
{
    internal class AddFineWindowViewModel : ViewModelBase
    {
        #region Данные

        #region Заголовок окна : Title

        private string _Title = DataFineModel.Title;

        /// <summary>Title</summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Наименование штрафа : NameFine

        private string _NameFine = "";

        /// <summary>NameFine</summary>

        public string NameFine
        {
            get => _NameFine;
            set => Set(ref _NameFine, value);
        }

        #endregion

        #region Дата получения штрафа : DateIssuedFine

        private DateTime _DateIssuedFine;

        /// <summary>DateIssuedFine</summary>

        public DateTime DateIssuedFine
        {
            get => _DateIssuedFine;
            set => Set(ref _DateIssuedFine, value);
        }

        #endregion

        #region Дата оплаты штрафа : DatePaymentsFine

        private DateTime _DatePaymentsFine;

        /// <summary>DatePaymentsFine</summary>

        public DateTime DatePaymentsFine
        {
            get => _DatePaymentsFine;
            set => Set(ref _DatePaymentsFine, value);
        }

        #endregion

        #region К оплате : Cost

        private string _Cost = "";

        /// <summary>Cost</summary>

        public string Cost
        {
            get => _Cost;
            set => Set(ref _Cost, value);
        }

        #endregion

        #region Оплата штрафа : IsCheck 

        private bool _IsCheck = false;

        /// <summary>IsCheck</summary>
        public bool IsCheck
        {
            get => _IsCheck;
            set => Set(ref _IsCheck, value);
        }

        #endregion

        #region Видимость даты оплаты штрафа : Visible 

        private string _Visible = "Hidden";

        /// <summary>Visible</summary>
        public string Visible
        {
            get => _Visible;
            set => Set(ref _Visible, value);
        }

        #endregion

        #region Переменные : IDRental

        int IDRental;

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Отображения даты окончания технического обслуживания : VisibleCommand

        public ICommand VisibleCommand { get; }

        private bool CanVisibleCommandExecute(object p) => true;

        private void OnVisibleCommandExecuted(object p)
        {
            if (IsCheck == true)
                Visible = "Visible";
            else
                Visible = "Hidden";
        }

        #endregion

        #region Команда Добавления штрафа : AddFineCommand

        public ICommand AddFineCommand { get; }

        private bool CanAddFineCommandExecute(object p) => true;

        private void OnAddFineCommandExecuted(object p)
        {
            if (NameFine != "")
            {
                if (Cost != "")
                {
                    if (DataFineModel.Title == "Добавление штрафа")
                    {
                        if (IsCheck == true)
                        {
                            if (DatePaymentsFine >= DateIssuedFine)
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                SqlConnection ThisConnection = new SqlConnection(connectionString);
                                ThisConnection.Open();
                                SqlCommand thisCommand = ThisConnection.CreateCommand();
                                thisCommand.CommandText = "Select id_rental from rental where start_date_rental>='" + DateIssuedFine + "' and end_date_rental<='" + DateIssuedFine + "' and id_vehicle=" + VehicleDataModel.id_vehicle;
                                SqlDataReader thisReader = thisCommand.ExecuteReader();
                                thisReader.Read();

                                if (thisReader.HasRows)
                                {
                                    IDRental = Convert.ToInt32(thisReader["id_rental"].ToString());

                                    thisReader.Close();

                                    var command = ThisConnection.CreateCommand();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "Add_fine";
                                    command.Parameters.AddWithValue("@id_rental", IDRental);
                                    command.Parameters.AddWithValue("@name_fine", NameFine);
                                    command.Parameters.AddWithValue("@cost_fine", Cost);
                                    command.Parameters.AddWithValue("@date_issued_fine", DateIssuedFine);
                                    command.Parameters.AddWithValue("@date_payments_fine", DatePaymentsFine);
                                    command.ExecuteNonQuery();

                                    ThisConnection.Close();

                                    MessageBox.Show("Данные добавленны");

                                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                                    {
                                        if (window.DataContext == this)
                                        {
                                            window.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("На выбранную дату не мог поступить штраф.");
                                }
                                thisReader.Close();

                                ThisConnection.Close();
                            }
                            else MessageBox.Show("Проверьте правильность введенных дат");
                        }
                        else
                        {
                            int IDRental;

                            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                            SqlConnection ThisConnection = new SqlConnection(connectionString);
                            ThisConnection.Open();
                            SqlCommand thisCommand = ThisConnection.CreateCommand();
                            thisCommand.CommandText = "Select id_rental from rental where start_date_rental>='" + DateIssuedFine + "' and end_date_rental<='" + DateIssuedFine + "' and id_vehicle=" + VehicleDataModel.id_vehicle;
                            SqlDataReader thisReader = thisCommand.ExecuteReader();
                            thisReader.Read();

                            if (thisReader.HasRows)
                            {
                                IDRental = Convert.ToInt32(thisReader["id_rental"].ToString());

                                thisReader.Close();

                                var command = ThisConnection.CreateCommand();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "Add_NULL_fine";
                                command.Parameters.AddWithValue("@id_rental", IDRental);
                                command.Parameters.AddWithValue("@name_fine", NameFine);
                                command.Parameters.AddWithValue("@cost_fine", Cost);
                                command.Parameters.AddWithValue("@date_issued_fine", DateIssuedFine);
                                command.ExecuteNonQuery();

                                ThisConnection.Close();

                                MessageBox.Show("Данные добавленны");

                                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                                {
                                    if (window.DataContext == this)
                                    {
                                        window.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("На выбранную дату не мог поступить штраф.");
                            }
                            thisReader.Close();

                            ThisConnection.Close();
                        }
                    }
                    else if(DataFineModel.Title == "Редактирование штрафа")
                    {
                        if (IsCheck == true)
                        {
                            if (DateIssuedFine <= DatePaymentsFine)
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                SqlConnection ThisConnection = new SqlConnection(connectionString);
                                ThisConnection.Open();
                                var command = ThisConnection.CreateCommand();
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "Edit_fine";
                                command.Parameters.AddWithValue("@id_fine", DataFineModel.IdFine);
                                command.Parameters.AddWithValue("@date_payments_fine", DatePaymentsFine);
                                command.ExecuteNonQuery();

                                ThisConnection.Close();

                                MessageBox.Show("Данные изменены");

                                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                                {
                                    if (window.DataContext == this)
                                    {
                                        window.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Даты указаны не верно");
                            }
                        }
                        else
                        {
                            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                            {
                                if (window.DataContext == this)
                                {
                                    window.Close();
                                }
                            }
                        }
                    }
                }
                else MessageBox.Show("Введи сумму штрафа");
            }
            else MessageBox.Show("Введите наименование штрафа");
        }

        #endregion


        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddFineWindowViewModel()
        {
            #region Команды

            AddFineCommand = new LamdaCommand(OnAddFineCommandExecuted, CanAddFineCommandExecute);

            VisibleCommand = new LamdaCommand(OnVisibleCommandExecuted, CanVisibleCommandExecute);

            #endregion

            #region Данные

            if (DataFineModel.Title == "Добавление штрафа")
            {
                DateIssuedFine = DateTime.Today;
                DatePaymentsFine = DateTime.Today;
            }
            else if (DataFineModel.Title == "Редактирование штрафа")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                SqlConnection ThisConnection = new SqlConnection(connectionString);
                ThisConnection.Open();
                SqlCommand thisCommand = ThisConnection.CreateCommand();
                thisCommand.CommandText = "Select * from fine where id_fine = " + DataFineModel.IdFine;
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                thisReader.Read();

                IDRental = Convert.ToInt32(thisReader["id_rental"].ToString());
                NameFine = thisReader["name_fine"].ToString();
                Cost = thisReader["cost_fine"].ToString();
                DateIssuedFine = Convert.ToDateTime(thisReader["date_issued_fine"].ToString());
                DatePaymentsFine = DateTime.Today;

                thisReader.Close();
                ThisConnection.Close();
            }

            #endregion
        }
    }
}
