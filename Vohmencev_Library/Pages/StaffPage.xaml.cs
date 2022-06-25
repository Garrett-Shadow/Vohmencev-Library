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
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        public Database.Vohmencev_LibraryEntities Connection;
        public Database.Users SelectedUser { get; set; }
        public Database.Authors SelectedAuthor { get; set; }
        public Database.Publishers SelectedPublisher { get; set; }
        public Database.Books SelectedBook { get; set; }
        public Database.BorrowingOfBooks SelectedBorrowing { get; set; }
        public List<Database.Users> Users { get; set; }
        public List<Database.Authors> Authors { get; set; }
        public List<Database.Publishers> Publishers { get; set; }
        public List<Database.Books> Books { get; set; }
        public List<Database.Books> BorrowingContent { get; set; }
        public List<Database.BorrowingOfBooks> Borrowings { get; set; }
        public HashSet<Database.Books> BorrowingHashSet { get; set; }

        public StaffPage()
        {
            InitializeComponent();
            Connection = Pages.Connector.GetModel();
            BorrowingContent = new List<Database.Books>();
            BorrowingHashSet = new HashSet<Database.Books>();
            DataContext = this;
            UserListUpdate();
            AuthorListUpdate();
            PublisherListUpdate();
            BookListUpdate();
            ManagementReadyButton.IsEnabled = false;
            ManagementCancelButton.IsEnabled = false;
        }

        //Добавление читателей

        private void UserAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameText == null || UserPhoneText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            int NewUserNumber;
            var LastUser = Connection.Users.OrderBy(user => user.UserCard).ToList().LastOrDefault();
            if (LastUser == null)
            {
                NewUserNumber = 1;
            }
            else
            {
                NewUserNumber = LastUser.UserCard + 1;
            }
            var NewUser = new Database.Users();
            NewUser.UserCard = NewUserNumber;
            NewUser.UserName = UserNameText.Text.ToString();
            NewUser.UserPhone = UserPhoneText.Text.ToString();
            Connection.Users.Add(NewUser);
            Connection.SaveChanges();
            UserListUpdate();
            SelectedUser = null;
            UserList.SelectedIndex = -1;
            UserNameText.Text = "";
            UserPhoneText.Text = "";
            MessageBox.Show("Новый читатель успешно добавлен!");
        }

        private void UserRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameText == null || UserPhoneText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            SelectedUser = null;
            UserList.SelectedIndex = -1;
            UserNameText.Text = "";
            UserPhoneText.Text = "";
            MessageBox.Show("Данные читателя успешно обновлены!");
        }

        private void UserDropButton_Click(object sender, RoutedEventArgs e)
        {
            UserAddButton.IsEnabled = true;
            UserRefreshButton.IsEnabled = false;
            SelectedUser = null;
            UserList.SelectedIndex = -1;
            UserNameText.Text = "";
            UserPhoneText.Text = "";
            UserBindingUpdate();
        }

        private void UserListUpdate()
        {
            Users = Connection.Users.OrderBy(user => new { user.UserName }).ToList();
            UserList.ItemsSource = Users;
            BorrowingUserCombo.ItemsSource = Users;
            ManagementUserCombo.ItemsSource = Users;
        }

        private void UserListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserList.SelectedIndex != -1)
            {
                UserAddButton.IsEnabled = false;
                UserRefreshButton.IsEnabled = true;
                SelectedUser = UserList.SelectedItem as Database.Users;
                UserBindingUpdate();
            }
            else
            {
                UserAddButton.IsEnabled = true;
                UserRefreshButton.IsEnabled = false;
                SelectedUser = null;
                UserList.SelectedIndex = -1;
                UserNameText.Text = "";
                UserPhoneText.Text = "";
                UserBindingUpdate();
            }
        }

        private void UserBindingUpdate()
        {
            UserNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            UserPhoneText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        //Добавление авторов

        private void AuthorAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorNameText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            int NewAuthorNumber;
            var LastAuthor = Connection.Authors.OrderBy(auth => auth.AuthorCode).ToList().LastOrDefault();
            if (LastAuthor == null)
            {
                NewAuthorNumber = 1;
            }
            else
            {
                NewAuthorNumber = LastAuthor.AuthorCode + 1;
            }
            var NewAuthor = new Database.Authors();
            NewAuthor.AuthorCode = NewAuthorNumber;
            NewAuthor.AuthorName = AuthorNameText.Text.ToString();
            Connection.Authors.Add(NewAuthor);
            Connection.SaveChanges();
            AuthorListUpdate();
            SelectedAuthor = null;
            AuthorList.SelectedIndex = -1;
            AuthorNameText.Text = "";
            MessageBox.Show("Новый автор успешно добавлен!");
        }

        private void AuthorRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorNameText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            SelectedAuthor = null;
            AuthorList.SelectedIndex = -1;
            AuthorNameText.Text = "";
            MessageBox.Show("Данные автора успешно обновлены!");
        }

        private void AuthorDropButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorAddButton.IsEnabled = true;
            AuthorRefreshButton.IsEnabled = false;
            SelectedAuthor = null;
            AuthorList.SelectedIndex = -1;
            AuthorNameText.Text = "";
            AuthorBindingUpdate();
        }

        private void AuthorListUpdate()
        {
            Authors = Connection.Authors.OrderBy(auth => new { auth.AuthorName }).ToList();
            AuthorList.ItemsSource = Authors;
            BookAuthorCombo.ItemsSource = Authors;
        }

        private void AuthorListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorList.SelectedIndex != -1)
            {
                AuthorAddButton.IsEnabled = false;
                AuthorRefreshButton.IsEnabled = true;
                SelectedAuthor = AuthorList.SelectedItem as Database.Authors;
                AuthorBindingUpdate();
            }
            else
            {
                AuthorAddButton.IsEnabled = true;
                AuthorRefreshButton.IsEnabled = false;
                SelectedAuthor = null;
                AuthorList.SelectedIndex = -1;
                AuthorNameText.Text = "";
                AuthorBindingUpdate();
            }
        }

        private void AuthorBindingUpdate()
        {
            AuthorNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        //Добавление издателей

        private void PublisherAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (PublisherNameText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            int NewPublisherNumber;
            var LastPublisher = Connection.Publishers.OrderBy(pub => pub.PublisherCode).ToList().LastOrDefault();
            if (LastPublisher == null)
            {
                NewPublisherNumber = 1;
            }
            else
            {
                NewPublisherNumber = LastPublisher.PublisherCode + 1;
            }
            var NewPublisher = new Database.Publishers();
            NewPublisher.PublisherCode = NewPublisherNumber;
            NewPublisher.PublisherName = PublisherNameText.Text.ToString();
            Connection.Publishers.Add(NewPublisher);
            Connection.SaveChanges();
            PublisherListUpdate();
            SelectedPublisher = null;
            PublisherList.SelectedIndex = -1;
            PublisherNameText.Text = "";
            MessageBox.Show("Новый издатель успешно добавлен!");
        }

        private void PublisherRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (PublisherNameText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            SelectedPublisher = null;
            PublisherList.SelectedIndex = -1;
            PublisherNameText.Text = "";
            MessageBox.Show("Данные издателя успешно обновлены!");
        }

        private void PublisherDropButton_Click(object sender, RoutedEventArgs e)
        {
            PublisherAddButton.IsEnabled = true;
            PublisherRefreshButton.IsEnabled = false;
            SelectedPublisher = null;
            PublisherList.SelectedIndex = -1;
            PublisherNameText.Text = "";
            PublisherBindingUpdate();
        }

        private void PublisherListUpdate()
        {
            Publishers = Connection.Publishers.OrderBy(pub => new { pub.PublisherName }).ToList();
            PublisherList.ItemsSource = Publishers;
            BookPublisherCombo.ItemsSource = Publishers;
        }

        private void PublisherListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PublisherList.SelectedIndex != -1)
            {
                PublisherAddButton.IsEnabled = false;
                PublisherRefreshButton.IsEnabled = true;
                SelectedPublisher = PublisherList.SelectedItem as Database.Publishers;
                PublisherBindingUpdate();
            }
            else
            {
                PublisherAddButton.IsEnabled = true;
                PublisherRefreshButton.IsEnabled = false;
                SelectedPublisher = null;
                PublisherList.SelectedIndex = -1;
                PublisherNameText.Text = "";
                PublisherBindingUpdate();
            }
        }

        private void PublisherBindingUpdate()
        {
            PublisherNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        //Добавление книг

        private void BookAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookNameText == null || BookAuthorCombo.SelectedIndex == -1 || BookPublisherCombo.SelectedIndex == -1 || BookGenresText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            int NewBookNumber;
            var LastBook = Connection.Books.OrderBy(book => book.BookCode).ToList().LastOrDefault();
            if (LastBook == null)
            {
                NewBookNumber = 1;
            }
            else
            {
                NewBookNumber = LastBook.BookCode + 1;
            }
            var NewBook = new Database.Books();
            NewBook.BookCode = NewBookNumber;
            NewBook.BookName = BookNameText.Text.ToString();
            NewBook.BookAuthor = (BookAuthorCombo.SelectedItem as Database.Authors).AuthorCode;
            NewBook.BookPublisher = (BookPublisherCombo.SelectedItem as Database.Publishers).PublisherCode;
            NewBook.BookGenres = BookGenresText.Text.ToString();
            Connection.Books.Add(NewBook);
            Connection.SaveChanges();
            BookListUpdate();
            SelectedBook = null;
            BookList.SelectedIndex = -1;
            BookNameText.Text = "";
            BookAuthorCombo.SelectedItem = -1;
            BookPublisherCombo.SelectedItem = -1;
            BookGenresText.Text = "";
            MessageBox.Show("Новая книга успешно добавлена!");
        }

        private void BookRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookNameText == null || BookAuthorCombo.SelectedIndex == -1 || BookPublisherCombo.SelectedIndex == -1 || BookGenresText == null)
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            Connection.SaveChanges();
            SelectedBook = null;
            BookList.SelectedIndex = -1;
            BookNameText.Text = "";
            BookAuthorCombo.SelectedItem = -1;
            BookPublisherCombo.SelectedItem = -1;
            BookGenresText.Text = "";
            MessageBox.Show("Данные книги успешно обновлены!");
        }

        private void BookDropButton_Click(object sender, RoutedEventArgs e)
        {
            BookAddButton.IsEnabled = true;
            BookRefreshButton.IsEnabled = false;
            SelectedBook = null;
            BookList.SelectedIndex = -1;
            BookNameText.Text = "";
            BookAuthorCombo.SelectedItem = -1;
            BookPublisherCombo.SelectedItem = -1;
            BookGenresText.Text = "";
            BookBindingUpdate();
        }

        private void BookListUpdate()
        {
            Books = Connection.Books.OrderBy(book => new { book.BookName }).ToList();
            BookList.ItemsSource = Books;
            BorrowingBookList.ItemsSource = Books;
        }

        private void BookListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookList.SelectedIndex != -1)
            {
                BookAddButton.IsEnabled = false;
                BookRefreshButton.IsEnabled = true;
                SelectedBook = BookList.SelectedItem as Database.Books;
                BookBindingUpdate();
            }
            else
            {
                BookAddButton.IsEnabled = true;
                BookRefreshButton.IsEnabled = false;
                SelectedBook = null;
                BookList.SelectedIndex = -1;
                BookNameText.Text = "";
                BookAuthorCombo.SelectedItem = -1;
                BookPublisherCombo.SelectedItem = -1;
                BookGenresText.Text = "";
                BookBindingUpdate();
            }
        }

        private void BookBindingUpdate()
        {
            BookNameText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            BookAuthorCombo.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateTarget();
            BookPublisherCombo.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateTarget();
            BookGenresText.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        //Выдача книг

        private void BorrowingAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (BorrowingUserCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали читателя!");
                return;
            }
            if (BorrowingContentList.Items.Count == 0)
            {
                MessageBox.Show("Вы не выбрали книги для выдачи!");
                return;
            }
            foreach (var Book in BorrowingHashSet)
            {
                int NewBorrowingNumber;
                var LastBorrowing = Connection.BorrowingOfBooks.OrderBy(borr => borr.BorrowingNumber).ToList().LastOrDefault();
                if (LastBorrowing == null)
                {
                    NewBorrowingNumber = 1;
                }
                else
                {
                    NewBorrowingNumber = LastBorrowing.BorrowingNumber + 1;
                }
                DateTime TodayDate = DateTime.Now;
                Database.BorrowingOfBooks NewBorrowing = new Database.BorrowingOfBooks();
                NewBorrowing.BorrowingNumber = NewBorrowingNumber;
                NewBorrowing.Book = (Book as Database.Books).BookCode;
                NewBorrowing.LibraryUser = (BorrowingUserCombo.SelectedItem as Database.Users).UserCard;
                NewBorrowing.DateSince = TodayDate;
                NewBorrowing.DateUntil = TodayDate.AddDays(14);
                NewBorrowing.BorrowingStatus = "На руках";
                Connection.BorrowingOfBooks.Add(NewBorrowing);
                Connection.SaveChanges();
            }
            BorrowingHashSet = new HashSet<Database.Books>();
            BorrowingContent = new List<Database.Books>();
            BorrowingUserCombo.SelectedIndex = -1;
            BorrowingContentListUpdate();
            MessageBox.Show("Выбранные книги выданы читателю!");
        }

        private void BorrowingContentListUpdate()
        {
            BorrowingContentList.ItemsSource = BorrowingContent;
        }

        private void BorrowingDropButton_Click(object sender, RoutedEventArgs e)
        {
            BorrowingHashSet = new HashSet<Database.Books>();
            BorrowingContent = new List<Database.Books>();
            BorrowingUserCombo.SelectedIndex = -1;
        }

        private void BorrowingBookListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedBook = BorrowingBookList.SelectedItem as Database.Books;
            if (SelectedBook != null)
            {
                BorrowingHashSet.Add(SelectedBook);
                BorrowingContent = BorrowingHashSet.ToList();
                BorrowingContentList.ItemsSource = BorrowingContent;
                BorrowingContentListUpdate();
            }
            BorrowingBookList.SelectedIndex = -1;
        }

        private void BorrowingContentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BorrowingContentList.SelectedIndex > -1)
            {
                BorrowingHashSet.Remove(BorrowingContentList.SelectedItem as Database.Books);
                BorrowingContent = BorrowingHashSet.ToList();
                BorrowingContentListUpdate();
            }
        }

        //Продление и выдача книг

        private void ManagementUserButton_Click(object sender, RoutedEventArgs e)
        {
            ManagementListUpdate();
        }

        private void ManagementReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBorrowing != null)
            {
                DateTime TodayDate = DateTime.Now;
                SelectedBorrowing.BorrowingStatus = "Продлена";
                SelectedBorrowing.DateUntil = TodayDate.AddDays(7);
                Connection.SaveChanges();
                MessageBox.Show("Книга продлена!");
                SelectedBorrowing = null;
                ManagementListUpdate();
            }
            else
            {
                MessageBox.Show("Вы не выбрали книгу для продления!");
                return;
            }
        }

        private void ManagementCancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBorrowing != null)
            {
                DateTime TodayDate = DateTime.Now;
                SelectedBorrowing.BorrowingStatus = "Сдана";
                SelectedBorrowing.DateUntil = TodayDate;
                Connection.SaveChanges();
                MessageBox.Show("Книга сдана!");
                SelectedBorrowing = null;
                ManagementListUpdate();
            }
            else
            {
                MessageBox.Show("Вы не выбрали книгу для сдачи!");
                return;
            }
        }

        private void ManagementListUpdate()
        {
            ManagementList.Items.Clear();
            if (ManagementUserCombo.SelectedIndex != -1)
            {
                Borrowings = Connection.BorrowingOfBooks.OrderBy(borr => new { borr.BorrowingNumber }).ToList();
                foreach (var books in Borrowings)
                {
                    if (books.LibraryUser==(ManagementUserCombo.SelectedItem as Database.Users).UserCard)
                    {
                        ManagementList.Items.Add(books);
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали читателя!");
                return;
            }
        }

        private void ManagementListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManagementList.Items.Count != -1)
            {
                ManagementReadyButton.IsEnabled = true;
                ManagementCancelButton.IsEnabled = true;
                SelectedBorrowing = ManagementList.SelectedItem as Database.BorrowingOfBooks;
            }
            else
            {
                ManagementReadyButton.IsEnabled = false;
                ManagementCancelButton.IsEnabled = false;
                SelectedBorrowing = null;
                MessageBox.Show("Вы не выбрали книгу на руках у читателя!");
            }
        }
    }
}
