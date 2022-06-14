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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private Database.Vohmencev_LibraryEntities Connection;
        public Database.Staff SelectedEmployee { get; set; }
        private List<Database.Staff> Employees { get; set; }
        public List<Database.Roles> Roles { get; set; }

        public AdminPage()
        {
            InitializeComponent();
            Connection = Pages.Connector.GetModel();
            Roles = Connection.Roles.ToList();
            RoleCombo.ItemsSource = Roles;
            DataContext = this;
            StaffListUpdate();
        }

        private void AdminAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffLogin == null || StaffPassword == null || StaffNameText == null || RoleCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            var NewEmployee = new Database.Staff();
            NewEmployee.StaffLogin = StaffLogin.Text.Trim();
            NewEmployee.StaffPassword = StaffPassword.Text.Trim();
            NewEmployee.StaffName = StaffNameText.Text.ToString();
            NewEmployee.StaffPosition = (RoleCombo.SelectedItem as Database.Roles).RoleName;
            Connection.Staff.Add(NewEmployee);
            Connection.SaveChanges();
            StaffListUpdate();
            StaffNameText.Text = "";
            StaffLogin.Text = "";
            StaffPassword.Text = "";
            RoleCombo.SelectedIndex = -1;
            SelectedEmployee = null;
            MessageBox.Show("Новый сотрудник успешно добавлен!");
        }

        private void AdminRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffLogin == null || StaffPassword == null || StaffNameText == null || RoleCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            MessageBox.Show("Данные сотрудника успешно обновлены!");
        }

        private void AdminDropButton_Click(object sender, RoutedEventArgs e)
        {
            AdminAddButton.IsEnabled = true;
            AdminRefreshButton.IsEnabled = false;
            StaffList.SelectedIndex = -1;
            StaffNameText.Text = "";
            StaffLogin.Text = "";
            StaffPassword.Text = "";
            RoleCombo.SelectedIndex = -1;
            SelectedEmployee = null;
            StaffBindingUpdate();
        }

        private void StaffListUpdate()
        {
            Employees = Connection.Staff.OrderBy(emp => new { emp.StaffName }).ToList();
            StaffList.ItemsSource = Employees;
        }

        private void StaffListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffList.SelectedIndex != -1)
            {
                AdminAddButton.IsEnabled = false;
                AdminRefreshButton.IsEnabled = true;
                SelectedEmployee = StaffList.SelectedItem as Database.Staff;
                StaffBindingUpdate();
            }
            else
            {
                AdminAddButton.IsEnabled = true;
                AdminRefreshButton.IsEnabled = false;
                StaffList.SelectedIndex = -1;
                StaffNameText.Text = "";
                StaffLogin.Text = "";
                StaffPassword.Text = "";
                RoleCombo.SelectedIndex = -1;
                SelectedEmployee = null;
                StaffBindingUpdate();
            }
        }

        private void StaffBindingUpdate()
        {
            StaffNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            StaffLogin.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            StaffPassword.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            RoleCombo.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateTarget();
        }
    }
}
