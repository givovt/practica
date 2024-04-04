using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int mistake = 0;
        private void login_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Password;
            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => x.login == login && x.password == password || x.email == login && x.password == password);
            if (user is null)
            {
                result_text.Text = "Неправильный логин,эмейл или пароль!";
                mistake++;
                if (mistake % 3 == 0)
                {
                    MessageBox.Show("Вы 3 раза ошиблись, вы задержины полицией кринжа на 15 секунд");
                    Thread.Sleep(15000);
                }
            }
            else
            {
                result_text.Text = "Вы успешно вошли в аккаунт!";
                result form = new result();
                form.Title = $"Добро пожаловать {LoginBox.Text}";
                form.Show();
                this.Close();
            }
        }

        private void switch_Click(object sender, RoutedEventArgs e)
        {
            Window1 form = new Window1();
            form.Show();
            this.Close();
        }

        int chet = 0;
        private void show_off_password_Click(object sender, RoutedEventArgs e)
        {
            chet++;
            if (chet % 2 == 0)
            {
                PasswordBox.Password = passres.Text;
                passres.Visibility = Visibility.Hidden;
                passres.IsEnabled = false;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordBox.IsEnabled = true;
                eye.Source = new BitmapImage(new Uri("C:\\Минаев\\WpfApp2\\res\\eyes1.png"));
            }
            else
            {
                passres.Text = PasswordBox.Password;
                passres.Visibility = Visibility.Visible;
                passres.IsEnabled = true;
                PasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.IsEnabled = false;
                eye.Source = new BitmapImage(new Uri("C:\\Минаев\\WpfApp2\\res\\eyes2.png"));
            }
        }
    }
}
