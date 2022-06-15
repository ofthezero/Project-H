using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //РЕСЕПШИОНИСТ------------------------------------------------LE RICHMOND ГЛАВНОЕ ОКНО------------------------------------------------------------
{
    public partial class Receptionist : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public Receptionist()
        {
            InitializeComponent();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();

            DataSet1 = new DataSet1();
            userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);

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

        private void guest_Click(object sender, RoutedEventArgs e) //гости
        {
            RecepGuests gosti = new RecepGuests();
            gosti.Show();
            this.Close();
        }

        private void broni_Click(object sender, RoutedEventArgs e) //бронирование
        {
            RecepBook bron = new RecepBook();
            bron.Show();
            this.Close();
        }
          private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) btExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.Q) гости.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.W) broni.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.X) btnExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
