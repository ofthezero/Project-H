using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //-----------------------------------------------------LE RICHMOND АДМИНИСТРАТОР--------------------------------------------------------
{
    public partial class A : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public A()
        {
            InitializeComponent();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter(); userTableAdapter.Fill(DataSet1.User);

            dat.Text = "Москва, Россия " + DateTime.Now.ToLongDateString(); //вывод даты
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) //управление окном
        {
            try { DragMove(); }
            catch { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) //выход - переход в окно авторизация
        {
            MainWindow R = new MainWindow();
            R.Show();
            this.Close();
        }

        private void guest_Click(object sender, RoutedEventArgs e) //переход в окно - гости
        {
            Gosti gosti = new Gosti();
            gosti.Show();
            this.Close();
        }

        private void komnati_Click(object sender, RoutedEventArgs e) //переход в окно - комнаты
        {
            Komnati komnati = new Komnati();
            komnati.Show();
            this.Close();
        }

        private void broni_Click(object sender, RoutedEventArgs e) //переход в окно - бронирование
        {
            Bron bron = new Bron();
            bron.Show();
            this.Close();
        }

        private void otch_Click(object sender, RoutedEventArgs e) //переход в окно - отчет 
        {
            pechat pechat = new pechat();
            pechat.Show();
            this.Close();
        }

        private void pol_Click(object sender, RoutedEventArgs e) //переход в окно - пользователи
        {
            Sotrudniki sotrudniki = new Sotrudniki();
            sotrudniki.Show();
            this.Close();
        }
    }
}
