using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.Xml;
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

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Text;
            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => x.login == login && x.password == password);
            if (user is null)
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
            MessageBox.Show("Вы успешно вошли в аккаунт!");
        }
        private void DeleteUser()
        {
            var userId = 1;
            var context = new AppDbContext();
            var user = context.Users.Find(userId);

            context.Users.Remove(user);
            context.SaveChanges();
        }

        private void UpdateUser()
        {
            var userId = 1;
            var context = new AppDbContext();
            var user = context.Users.Find(userId);

            user.login = "admin1";

            context.SaveChanges();
        }
    }
}
