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
using System.Windows.Shapes;

namespace WpfApp2
{
    public class User
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<User> Transports { get; set; }
    }

    public class Transport
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Identifier { get; set; }
        public DbSet<Transport> Transports { get; set; }
    }
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=registr;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
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

            var pass    = PasswordBox.Text;

            var context = new AppDbContext();

            var user_exists = context.Users.FirstOrDefault(x => x.login == login);
            if (user_exists is not null)
            {
                MessageBox.Show("Такой пользователь уже в клубе крутяшек");
                return;
            }
            var user = new User { login = login, password = pass };
            context.Users.Add(user);
            context.SaveChanges();
            MessageBox.Show("welcome to the club, buddy");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
