using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TheHotel //ПОЛЬЗОВАТЕЛИ---------------------------------------------LE RICHMOND ИЗМЕНЕНИЕ ПАРОЛЯ-------------------------------------------------------------
{
    public partial class PassRecovery : Window
    {
        SqlConnection con = new SqlConnection();
        string username = PassCode.to;

        public PassRecovery()
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

            if ( textBox1.Text == textBox2.Text && (textBox1.Text).Length >= 6 && (textBox2.Text).Length >= 6 )
            {
                SqlConnection conn = new SqlConnection(@"Data Source=IAMROBERT\MSSQLSERVERR;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string q = "update [User] set [Password]='" + password + "' where Email='" + username + "'";
                SqlCommand cmd = new SqlCommand(q, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                tb_error.Text = ""; tb_ok.Text = "✔ Пароль успешно изменен";
                this.Close();

                MainWindow R = new MainWindow();
                R.Show();
                this.Close();
            }
            else
            {
                tb_ok.Text = ""; tb_error.Text = "⚠ Пароли не совподают или не соответствуют требованиям";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) btExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.Enter) broni.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.X) btnExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void textBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 @.-_".IndexOf(e.Text) < 0;
        }

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 @.-_".IndexOf(e.Text) < 0;
        }
    }

}
