using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace TheHotel //ПОЛЬЗОВАТЕЛИ---------------------------------------------LE RICHMOND ИЗМЕНЕНИЕ ПАРОЛЯ-------------------------------------------------------------
{
    public partial class otch : Window
    {
        SqlConnection con = new SqlConnection();
        string username = repass.to;

        public otch()
        {
            InitializeComponent();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();

            dat.Text = "Москва, Россия " + DateTime.Now.ToLongDateString();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
                //////////////////////////////////////////////////////////
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) //выход
        {
            MainWindow R = new MainWindow();
            R.Show();
            this.Close();
        }

        private void broni_Click(object sender, RoutedEventArgs e) //проверка кода
        {
            string password = textBox1.Text;
            if (textBox1.Text == textBox2.Text)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=IAMROBERT\MSSQLSERVERR;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string q = "update [User] set [Password]='" + password + "' where Email='" + username + "'";
                SqlCommand cmd = new SqlCommand(q, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("✔ Пароль успешно изменен");
                this.Close();
            }
            else
            {
                MessageBox.Show("⚠ Пароли не совподают");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
