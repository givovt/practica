using Azure.Core.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace WpfApp2
{
    public class User
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinaevDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            var login   = loginbox.Text;

            var pass    = PasswordBox.Password;

            var emaill = email_box.Text;

            var context = new AppDbContext();

            var user_exists = context.Users.FirstOrDefault(x => x.login == login);
            if (user_exists is not null)
            {
                result_reg_text.Text = "Такой пользователь уже в клубе крутяшек";
                n1.Visibility = Visibility.Visible;
                n2.Visibility = Visibility.Hidden;
                n3.Visibility = Visibility.Hidden;
                n4.Visibility = Visibility.Hidden;
                return;
            }

            char[] charr = {'!','@','#','$','%','^','&','*','(',')'};
            int sum = 0;
            int sum2 = 0;

            var email_exists = context.Users.FirstOrDefault(x => x.email == emaill);

            if(email_exists is not null)
            {
                result_reg_text.Text = "Человек с таким адресом уже зарегестрирован";
                n1.Visibility = Visibility.Hidden;
                n2.Visibility = Visibility.Visible;
                n3.Visibility = Visibility.Hidden;
                n4.Visibility = Visibility.Hidden;
                return;
            }

            for (int i = 0; emaill.Length > i; i++)
            {
                if (emaill[i] == charr[1])
                {
                    sum2++;
                }
            }

            if(sum2 < 1)
            {
                result_reg_text.Text = "Адрес не коректен";
                n1.Visibility = Visibility.Hidden;
                n2.Visibility = Visibility.Visible;
                n3.Visibility = Visibility.Hidden;
                n4.Visibility = Visibility.Hidden;
                return;
            }

            for (int i = 0; PasswordBox.Password.Length > i; i++)
            {
                if (pass.Contains(charr[i]) == true)
                {
                    sum++;
                }
            }

            if (sum < 1)
            {
                result_reg_text.Text = "В пароле должны присутсвовать символы";
                n1.Visibility = Visibility.Hidden;
                n2.Visibility = Visibility.Hidden;
                n3.Visibility = Visibility.Visible;
                n4.Visibility = Visibility.Hidden;
                return;
            }

            if (pass.Length < 6)
            {
                result_reg_text.Text = "Пароль слишком маленький";
                n1.Visibility = Visibility.Hidden;
                n2.Visibility = Visibility.Hidden;
                n3.Visibility = Visibility.Visible;
                n4.Visibility = Visibility.Hidden;
                return;
            }
            if (PasswordBox.Password != Password2.Password)
            {
                n4.Visibility = Visibility.Visible;
                result_reg_text.Text = "Вы не подтвердили пароль";
                n1.Visibility = Visibility.Hidden;
                n2.Visibility = Visibility.Hidden;
                n3.Visibility = Visibility.Hidden;
            }
            else
            {
                var user = new User { login = login, password = pass, email = emaill};
                context.Users.Add(user);
                context.SaveChanges();
                result_reg_text.Text = "welcome to the club, buddy";
                MainWindow form = new MainWindow();
                form.Show();
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }
    }
}
