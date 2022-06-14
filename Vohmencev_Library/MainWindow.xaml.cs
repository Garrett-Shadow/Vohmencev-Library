using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vohmencev_Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckSuperUser();
        }

        private void CheckSuperUser()
        {
            int count = Pages.Connector.GetModel().Staff.Count();
            if (count == 0)
            {
                MainFrame.Navigate(Pages.PageClass.GetSuperUserRegistration());
            }
            else
            {
                MainFrame.Navigate(Pages.PageClass.GetAuthorization());
            }
        }
    }
}
