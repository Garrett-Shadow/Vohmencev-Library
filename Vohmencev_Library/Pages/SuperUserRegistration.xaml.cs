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

namespace Vohmencev_Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для SuperUserRegistration.xaml
    /// </summary>
    public partial class SuperUserRegistration : Page
    {
        private readonly Database.Vohmencev_LibraryEntities Connection;

        public SuperUserRegistration()
        {
            InitializeComponent();
            Connection = Pages.Connector.GetModel();
        }

        private void RegSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string Phone = RegLoginText.Text.Trim();
            string Password = RegPasswordText.Password.Trim();
            string FullName = NameText.Text.ToString();
            if (Phone == null)
            {
                MessageBox.Show("Вы не ввели свой номер телефона!");
                return;
            }
            if (Password == null)
            {
                MessageBox.Show("Вы не ввели пароль!");
                return;
            }
            if (FullName == null)
            {
                MessageBox.Show("Вы не ввели ФИО!");
                return;
            }
            Database.Staff SuperUser = new Database.Staff();
            SuperUser.StaffLogin = Phone;
            SuperUser.StaffPassword = Password;
            SuperUser.StaffName = FullName;
            SuperUser.StaffPosition = "Администратор";
            Connection.Staff.Add(SuperUser);
            int result = Connection.SaveChanges();
            if (result == 0)
            {
                MessageBox.Show("Регистрация администратора не была выполнена!");
                return;
            }
            else
            {
                MessageBox.Show("Регистрация администратора была успешно выполнена!");
            }
            NavigationService.Navigate(Pages.PageClass.GetAuthorization());
        }
    }
}
