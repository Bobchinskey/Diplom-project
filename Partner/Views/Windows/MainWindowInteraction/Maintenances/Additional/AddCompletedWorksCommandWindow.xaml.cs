using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Partner.Views.Windows.MainWindowInteraction.Maintenances.Additional
{
    /// <summary>
    /// Логика взаимодействия для AddCompletedWorksCommandWindow.xaml
    /// </summary>
    public partial class AddCompletedWorksCommandWindow : Window
    {
        public AddCompletedWorksCommandWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
