using Partner.Data.UserData;
using Partner.Infrastructure.Commands;
using Partner.Models.PersonalData;
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

namespace Partner.ViewModels.Windows.MainWindowInteraction.VehicleWindow
{
    class AddVehicleWindowViewModel : ViewModelBase
    {
        #region Данные 

        #region Заголовок окна : Title 

        private string _Title = VehicleDataModel.EditOrAdd;

        /// <summary>Title</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Марка и модель : MakeModel 

        private string _MakeModel = VehicleDataModel.MakeModel;

        /// <summary>MakeModel</summary>
        public string MakeModel
        {
            get => _MakeModel;
            set => Set(ref _MakeModel, value);
        }

        #endregion

        #region VIN номер : VIN 

        private string _VIN = VehicleDataModel.VIN;

        /// <summary>VIN</summary>
        public string VIN
        {
            get => _VIN;
            set => Set(ref _VIN, value);
        }

        #endregion

        #region Гос номер : StateNumber 

        private string _StateNumber = VehicleDataModel.StateNumber;

        /// <summary>StateNumber</summary>
        public string StateNumber
        {
            get => _StateNumber;
            set => Set(ref _StateNumber, value);
        }

        #endregion

        #region Выбранный тип ТС : SelectStateNumber 

        private string _SelectStateNumber = VehicleDataModel.SelectStateNumber;

        /// <summary>SelectStateNumber</summary>
        public string SelectStateNumber
        {
            get => _SelectStateNumber;
            set => Set(ref _SelectStateNumber, value);
        }

        #endregion

        #region Год изготовления : YearManufacture 

        private string _YearManufacture = VehicleDataModel.YearManufacture;

        /// <summary>YearManufacture</summary>
        public string YearManufacture
        {
            get => _YearManufacture;
            set => Set(ref _YearManufacture, value);
        }

        #endregion

        #region Категория ТС : Category 

        private string[] _Category = { "A", "B", "B1","C","CE","C1","C1E","D","D1","M","прицеп" };

        /// <summary>Category</summary>
        public string[] Category
        {
            get => _Category;
            set => Set(ref _Category, value);
        }

        #endregion

        #region Выбранная категория : SelectCategory 

        private string _SelectCategory = VehicleDataModel.SelectCategory;

        /// <summary>SelectCategory</summary>
        public string SelectCategory
        {
            get => _SelectCategory;
            set => Set(ref _SelectCategory, value);
        }

        #endregion

        #region Номер дивгателя : NumberEngine 

        private string _NumberEngine = VehicleDataModel.NumberEngine;

        /// <summary>NumberEngine</summary>
        public string NumberEngine
        {
            get => _NumberEngine;
            set => Set(ref _NumberEngine, value);
        }

        #endregion

        #region Номер шасси : ChassisNumber 

        private string _ChassisNumber = VehicleDataModel.ChassisNumber;

        /// <summary>ChassisNumber</summary>
        public string ChassisNumber
        {
            get => _ChassisNumber;
            set => Set(ref _ChassisNumber, value);
        }

        #endregion

        #region Номер кузова : BodyNumber 

        private string _BodyNumber = VehicleDataModel.BodyNumber;

        /// <summary>BodyNumber</summary>
        public string BodyNumber
        {
            get => _BodyNumber;
            set => Set(ref _BodyNumber, value);
        }

        #endregion

        #region Цвет кузова : Color 

        private string _Color = VehicleDataModel.Color;

        /// <summary>Color</summary>
        public string Color
        {
            get => _Color;
            set => Set(ref _Color, value);
        }

        #endregion

        #region Экологический класс : EnvironmentalClass 

        private string _EnvironmentalClass = VehicleDataModel.EnvironmentalClass;

        /// <summary>EnvironmentalClass</summary>
        public string EnvironmentalClass
        {
            get => _EnvironmentalClass;
            set => Set(ref _EnvironmentalClass, value);
        }

        #endregion

        #region Мощность двигателя : PowerEngine 

        private string _PowerEngine = VehicleDataModel.PowerEngine;

        /// <summary>PowerEngine</summary>
        public string PowerEngine
        {
            get => _PowerEngine;
            set => Set(ref _PowerEngine, value);
        }

        #endregion

        #region Рабочий объес двигателя : DisplacementEngine 

        private string _DisplacementEngine = VehicleDataModel.DisplacementEngine;

        /// <summary>DisplacementEngine</summary>
        public string DisplacementEngine
        {
            get => _DisplacementEngine;
            set => Set(ref _DisplacementEngine, value);
        }

        #endregion

        #region Тип двигателя : TypeEngine 

        private string _TypeEngine = VehicleDataModel.TypeEngine;

        /// <summary>TypeEngine</summary>
        public string TypeEngine
        {
            get => _TypeEngine;
            set => Set(ref _TypeEngine, value);
        }

        #endregion

        #region Разрешенная максимальная масса : PermittedMaximumMass

        private string _PermittedMaximumMass = VehicleDataModel.PermittedMaximumMass;

        /// <summary>PermittedMaximumMass</summary>
        public string PermittedMaximumMass
        {
            get => _PermittedMaximumMass;
            set => Set(ref _PermittedMaximumMass, value);
        }

        #endregion

        #region Масса без нагрузки : WeightWithoutLoad

        private string _WeightWithoutLoad = VehicleDataModel.WeightWithoutLoad;

        /// <summary>WeightWithoutLoad</summary>
        public string WeightWithoutLoad
        {
            get => _WeightWithoutLoad;
            set => Set(ref _WeightWithoutLoad, value);
        }

        #endregion

        #region Серия ПТС : SeriesPTS

        private string _SeriesPTS = VehicleDataModel.SeriesPTS;

        /// <summary>SeriesPTS</summary>
        public string SeriesPTS
        {
            get => _SeriesPTS;
            set => Set(ref _SeriesPTS, value);
        }

        #endregion

        #region Номер ПТС : NumberPTS

        private string _NumberPTS = VehicleDataModel.NumberPTS;

        /// <summary>NumberPTS</summary>
        public string NumberPTS
        {
            get => _NumberPTS;
            set => Set(ref _NumberPTS, value);
        }

        #endregion

        #region Кем выдан ПТС : WhoIssuedPTS

        private string _WhoIssuedPTS = VehicleDataModel.WhoIssuedPTS;

        /// <summary>WhoIssuedPTS</summary>
        public string WhoIssuedPTS
        {
            get => _WhoIssuedPTS;
            set => Set(ref _WhoIssuedPTS, value);
        }

        #endregion

        #region Когда выдан ПТС : DateIssuedPTS

        private DateTime _DateIssuedPTS = VehicleDataModel.DateIssuedPTS;

        /// <summary>DateIssuedPTS</summary>
        public DateTime DateIssuedPTS
        {
            get => _DateIssuedPTS;
            set => Set(ref _DateIssuedPTS, value);
        }

        #endregion

        #region Серия СТС : SeriesSTS

        private string _SeriesSTS = VehicleDataModel.SeriesSTS;

        /// <summary>SeriesSTS</summary>
        public string SeriesSTS
        {
            get => _SeriesSTS;
            set => Set(ref _SeriesSTS, value);
        }

        #endregion

        #region Номер СТС : NumberSTS

        private string _NumberSTS = VehicleDataModel.NumberSTS;

        /// <summary>NumberSTS</summary>
        public string NumberSTS
        {
            get => _NumberSTS;
            set => Set(ref _NumberSTS, value);
        }

        #endregion

        #region Кем выдан СТС : WhoIssuedSTS

        private string _WhoIssuedSTS = VehicleDataModel.SeriesSTS;

        /// <summary>WhoIssuedSTS</summary>
        public string WhoIssuedSTS
        {
            get => _WhoIssuedSTS;
            set => Set(ref _WhoIssuedSTS, value);
        }

        #endregion

        #region Когда выдан СТС : DateIssuedSTS

        private DateTime _DateIssuedSTS = VehicleDataModel.DateIssuedSTS;

        /// <summary>DateIssuedSTS</summary>
        public DateTime DateIssuedSTS
        {
            get => _DateIssuedSTS;
            set => Set(ref _DateIssuedSTS, value);
        }

        #endregion

        #region Изображение автомобиля : Image 

        private object _Image = VehicleDataModel.Image;

        /// <summary>Image</summary>
        public object Image
        {
            get => _Image;
            set => Set(ref _Image, value);
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        #region Команды

        #region Команда Добавление автомобиля в БД

        public ICommand AddVehicleCommand { get; }

        private bool CanAddVehicleCommandExecute(object p)
        {
            if ((MakeModel != "") && (VIN != "") && (StateNumber != "") && (NumberEngine != "") && (ChassisNumber != "") && (BodyNumber != "") && (Color != "") && (EnvironmentalClass != "") && (PowerEngine != "") && (DisplacementEngine != "") && (TypeEngine != "") && (PermittedMaximumMass != "") && (WeightWithoutLoad != "") && (SeriesPTS != "") && (NumberPTS != "") && (WhoIssuedPTS != "") && (SeriesSTS != "") && (NumberSTS != "") && (WhoIssuedSTS != ""))
                return true;
            else
                return false;
        }

        private void OnAddVehicleCommandExecuted(object p)
        {
            if ((Convert.ToInt32(YearManufacture) > 1920) && (Convert.ToDateTime("01.01." + YearManufacture) <= DateTime.Today))
            {
                if ((Convert.ToDateTime("01.01." + YearManufacture) <= DateIssuedPTS) || (Convert.ToDateTime("01.01." + YearManufacture) <= DateIssuedSTS))
                {
                    if (StateNumber.Length >= 8)
                    {
                        if (Convert.ToDouble(PermittedMaximumMass) > Convert.ToDouble(WeightWithoutLoad))
                        {
                            if ((SeriesSTS.Length == 4) && (NumberSTS.Length == 6))
                            {
                                if ((SeriesPTS.Length == 4) && (NumberPTS.Length == 6))
                                {
                                    if (VehicleDataModel.EditOrAdd == "Добавить автомобиль")
                                    {
                                        string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                        SqlConnection ThisConnection = new SqlConnection(connectionString);
                                        ThisConnection.Open();
                                        var command = ThisConnection.CreateCommand();
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.CommandText = "add_vehicle";
                                        command.Parameters.AddWithValue("@make_model", MakeModel);
                                        command.Parameters.AddWithValue("@vin", VIN);
                                        command.Parameters.AddWithValue("@state_number", StateNumber);
                                        command.Parameters.AddWithValue("@type_vehicle", SelectStateNumber);
                                        command.Parameters.AddWithValue("@category", SelectCategory);
                                        command.Parameters.AddWithValue("@year_manufacture", YearManufacture);
                                        command.Parameters.AddWithValue("@engine_number", NumberEngine);
                                        command.Parameters.AddWithValue("@сhassis_number", ChassisNumber);
                                        command.Parameters.AddWithValue("@body_number", BodyNumber);
                                        command.Parameters.AddWithValue("@color", Color);
                                        command.Parameters.AddWithValue("@power_engine", PowerEngine);
                                        command.Parameters.AddWithValue("@displacement_engine", DisplacementEngine);
                                        command.Parameters.AddWithValue("@type_engine", TypeEngine);
                                        command.Parameters.AddWithValue("@environmental_class", EnvironmentalClass);
                                        command.Parameters.AddWithValue("@permitted_maximum_mass", PermittedMaximumMass);
                                        command.Parameters.AddWithValue("@weight_without_load", WeightWithoutLoad);
                                        command.Parameters.AddWithValue("@series_PTS", SeriesPTS);
                                        command.Parameters.AddWithValue("@number_PTS", NumberPTS);
                                        command.Parameters.AddWithValue("@who_issued_PTS", WhoIssuedPTS);
                                        command.Parameters.AddWithValue("@date_issued_PTS", DateIssuedPTS);
                                        command.Parameters.AddWithValue("@series_STS", SeriesSTS);
                                        command.Parameters.AddWithValue("@number_STS", NumberSTS);
                                        command.Parameters.AddWithValue("@who_issued_STS", WhoIssuedSTS);
                                        command.Parameters.AddWithValue("@date_issued_STS", DateIssuedSTS);
                                        command.Parameters.AddWithValue("@status", "Свободен");
                                        command.Parameters.AddWithValue("@image", "NULL");
                                        command.Parameters.AddWithValue("@who_add_system", UserDataModel.id_user);
                                        command.Parameters.AddWithValue("@date_add_system", DateTime.Today);
                                        command.ExecuteNonQuery();
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
                                    else
                                    {
                                        string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
                                        SqlConnection ThisConnection = new SqlConnection(connectionString);
                                        ThisConnection.Open();
                                        var command = ThisConnection.CreateCommand();
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.CommandText = "Update_vehicle";
                                        command.Parameters.AddWithValue("@id_vehicle", VehicleDataModel.id_vehicle);
                                        command.Parameters.AddWithValue("@make_model", MakeModel);
                                        command.Parameters.AddWithValue("@vin", VIN);
                                        command.Parameters.AddWithValue("@state_number", StateNumber);
                                        command.Parameters.AddWithValue("@type_vehicle", SelectStateNumber);
                                        command.Parameters.AddWithValue("@category", SelectCategory);
                                        command.Parameters.AddWithValue("@year_manufacture", YearManufacture);
                                        command.Parameters.AddWithValue("@engine_number", NumberEngine);
                                        command.Parameters.AddWithValue("@сhassis_number", ChassisNumber);
                                        command.Parameters.AddWithValue("@body_number", BodyNumber);
                                        command.Parameters.AddWithValue("@color", Color);
                                        command.Parameters.AddWithValue("@power_engine", PowerEngine);
                                        command.Parameters.AddWithValue("@displacement_engine", DisplacementEngine);
                                        command.Parameters.AddWithValue("@type_engine", TypeEngine);
                                        command.Parameters.AddWithValue("@environmental_class", EnvironmentalClass);
                                        command.Parameters.AddWithValue("@permitted_maximum_mass", PermittedMaximumMass);
                                        command.Parameters.AddWithValue("@weight_without_load", WeightWithoutLoad);
                                        command.Parameters.AddWithValue("@series_PTS", SeriesPTS);
                                        command.Parameters.AddWithValue("@number_PTS", NumberPTS);
                                        command.Parameters.AddWithValue("@who_issued_PTS", WhoIssuedPTS);
                                        command.Parameters.AddWithValue("@date_issued_PTS", DateIssuedPTS);
                                        command.Parameters.AddWithValue("@series_STS", SeriesSTS);
                                        command.Parameters.AddWithValue("@number_STS", NumberSTS);
                                        command.Parameters.AddWithValue("@who_issued_STS", WhoIssuedSTS);
                                        command.Parameters.AddWithValue("@date_issued_STS", DateIssuedSTS);
                                        command.Parameters.AddWithValue("@status", VehicleDataModel.Status);
                                        command.Parameters.AddWithValue("@image", "NULL");
                                        command.Parameters.AddWithValue("@who_add_system", VehicleDataModel.WhAddSystem);
                                        command.Parameters.AddWithValue("@date_add_system", VehicleDataModel.DateAddSystem);
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
                                } else MessageBox.Show("Номер или серия ПТС указан неверно");
                            } else MessageBox.Show("Номер или серия СТС указан неверно");
                        }
                        else MessageBox.Show("Массы транспортного средства указаны не верно");
                    }
                    else MessageBox.Show("Гос. номер указан не верно");
                }
                else MessageBox.Show("Проверьте правильность заполнения данный СТС и ПТС");
            } else MessageBox.Show("Дата изготовления указана не верно");
        }

        #endregion

        #region Команда открытия окна "Техническое состояние ТС"

        public ICommand OpenTechnicalConditionCommand { get; }

        private bool CanOpenTechnicalConditionCommandExecute(object p)
        {
            if (VehicleDataModel.EditOrAdd == "Редактировать автомобиль")
                return true;
            else
                return false;
        }

        private void OnOpenTechnicalConditionCommandExecuted(object p)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["Partner"].ConnectionString;
            SqlConnection ThisConnection = new SqlConnection(connectionString);
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "select mileage,fuel,condition,description from technical_condition where id_vehicle = " + VehicleDataModel.id_vehicle;
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            dt.Load(thisReader);
            ThisConnection.Close();

            if (Convert.ToString(dt.Rows[0][0]) == "") dt.Rows[0][0] = "Не указан";
            if (Convert.ToString(dt.Rows[0][1]) == "") dt.Rows[0][1] = "Не указано";
            if (Convert.ToString(dt.Rows[0][2]) == "") dt.Rows[0][2] = "Не указано";
            if (Convert.ToString(dt.Rows[0][3]) == "") dt.Rows[0][3] = "Не указано";
            MessageBox.Show("Техническое состояние транспортного средства: \n Пробег: " + dt.Rows[0][0] + "\n Топливо: " + dt.Rows[0][1] + "\n Состояние: " + dt.Rows[0][2] + "\n Описание: " + dt.Rows[0][3], "Техническое состояние транспортного средства");
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------*/

        public AddVehicleWindowViewModel()
        {
            #region Команды

            AddVehicleCommand = new LamdaCommand(OnAddVehicleCommandExecuted, CanAddVehicleCommandExecute);

            OpenTechnicalConditionCommand = new LamdaCommand(OnOpenTechnicalConditionCommandExecuted, CanOpenTechnicalConditionCommandExecute);

            #endregion
        }

    }
}
