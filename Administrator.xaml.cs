using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //-----------------------------------------------------LE RICHMOND АДМИНИСТРАТОР--------------------------------------------------------
{
    public partial class Administrator : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public Administrator()
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
            AdminGuests gosti = new AdminGuests();
            gosti.Show();
            this.Close();
        }

        private void komnati_Click(object sender, RoutedEventArgs e) //переход в окно - комнаты
        {
            AdminRooms komnati = new AdminRooms();
            komnati.Show();
            this.Close();
        }

        private void broni_Click(object sender, RoutedEventArgs e) //переход в окно - бронирование
        {
            AdminBook bron = new AdminBook();
            bron.Show();
            this.Close();
        }

        private void otch_Click(object sender, RoutedEventArgs e) //переход в окно - отчет 
        {
            AdminPrint pechat = new AdminPrint();
            pechat.Show();
            this.Close();
        }

        private void pol_Click(object sender, RoutedEventArgs e) //переход в окно - пользователи
        {
            AdminEmployee sotrudniki = new AdminEmployee();
            sotrudniki.Show();
            this.Close();
        }

        private void otch_Clickk(object sender, RoutedEventArgs e) //переход в окно - тип комнат
        {
            AdminRoomsType sotrudniki = new AdminRoomsType();
            sotrudniki.Show();
            this.Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) btExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.X) btExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.Q) гости.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.W) komnati.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.E) otc.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.R) broni.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.T) pol.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.P) otch.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }

        private void adr_Click(object sender, RoutedEventArgs e)
        {
            AdminAdress adress = new AdminAdress();
            adress.Show();
            this.Close();
        }
    }
}
