using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel  //Администратор----------------------------------------------LE RICHMOND ТИП КОМНАТ---------------------------------------------------------
{
    public partial class AdminAdress : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public  AdminAdress()
        {
            InitializeComponent(); RefreshData();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);
        }

        public void RefreshData()
        {

            AdressHotelTableAdapter adapter = new AdressHotelTableAdapter(); DataSet1.AdressHotelDataTable table = new DataSet1.AdressHotelDataTable(); adapter.Fill(table);
            dg.ItemsSource = table;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow R = new MainWindow();
            R.Show();
            this.Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem != null)
            {
                if (tb1.Text != null & tb3.Text != null & tb3.Text != null)
                {
                    if (dg.SelectedItem != null) tb1.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                    if (dg.SelectedItem != null) tb2.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[2].ToString();
                    if (dg.SelectedItem != null) tb3.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[3].ToString();
                    if (dg.SelectedItem != null) tb4.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[4].ToString();
                }
                else { }
            }
            else { }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tb_error.Text = "";
            tb_ok.Text = "";
            tb2.Clear();
            tb3.Clear();

        }

        private void BLA_Click(object sender, RoutedEventArgs e)
        {
            Administrator s = new Administrator();
            s.Show();
            this.Close();
        }

        private void dob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb1.Text) && (tb1.Text).Length >= 4 && (tb2.Text).Length >= 4)
                {
                    new AdressHotelTableAdapter().InsertQuery(Convert.ToString(tb1.Text), Convert.ToString(tb2.Text), Convert.ToString(tb3.Text), Convert.ToString(tb4.Text));
                    tb_error.Text = ""; tb_ok.Text = "✔ Данные успешно добавлены";
                    RefreshData();
                }
                else { tb_ok.Text = ""; tb_error.Text = "⚠ Проверьте правильность  введенных данных"; }
            }
            catch { tb_ok.Text = ""; tb_error.Text = "⚠ Проверьте правильность  введенных данных"; }
        }

        private void izm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new AdressHotelTableAdapter().UpdateQuery(Convert.ToString(tb1.Text), Convert.ToString(tb2.Text), Convert.ToString(tb3.Text), Convert.ToString(tb4.Text), Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно изменены";
                RefreshData();
            }
            catch { tb_ok.Text = ""; tb_error.Text = "⚠ Проверьте правильность  введенных данных"; }
        }

        private void udal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new AdressHotelTableAdapter().DeleteQuery(Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно удалены";
                RefreshData();
                tb1.Clear();
                tb2.Clear();
                tb2.Clear();
                tb4.Clear();
            }
            catch
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Выберите строку из таблицы для удаления";
            }
        }

        private void exitt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Ma = new MainWindow();
            Ma.Show();
            this.Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) btExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.X) btnExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void tb2_PreviewTextInput(object sender, TextCompositionEventArgs e) 
        {

            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ12345678 .".IndexOf(e.Text) < 0;
        }

        private void tb3_PreviewTextInput(object sender, TextCompositionEventArgs e) 
        {
            e.Handled = "/абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789 ".IndexOf(e.Text) < 0;
        }

        private void tb3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ-".IndexOf(e.Text) < 0;
        }
    }
}
