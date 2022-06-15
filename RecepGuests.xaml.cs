using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //РЕСЕПШИОНИСТ--------------------------------------------------LE RICHMOND ГОСТИ---------------------------------------------------------
{
    public partial class RecepGuests : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public RecepGuests()
        {
            InitializeComponent(); RefreshData();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);
        }

        public void RefreshData()
        {
            clientsTableAdapter adapter = new clientsTableAdapter(); DataSet1.clientsDataTable table = new DataSet1.clientsDataTable(); adapter.Fill(table);
            dg.ItemsSource = table;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
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
                if (tb2.Text != null & tb3.Text != null & tb4.Text != null & tb5.Text != null)
                {
                    if (dg.SelectedItem != null) tb2.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); 
                    if (dg.SelectedItem != null) tb3.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); 
                    if (dg.SelectedItem != null) tb4.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); 
                    if (dg.SelectedItem != null) tb5.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[4].ToString();
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
            tb4.Clear();
            tb5.Clear();
        }

        private void BLA_Click(object sender, RoutedEventArgs e)
        {
            Receptionist s = new Receptionist();
            s.Show();
            this.Close();
        }

        private void dob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb4.Text) && !String.IsNullOrWhiteSpace(tb5.Text) && (tb3.Text).Length > 5 && (tb4.Text).Length >= 4 && (tb5.Text).Length >= 6 && (tb2.Text).Length > 4)
                {
                    new clientsTableAdapter().InsertQuery(Convert.ToString(tb2.Text), Convert.ToDecimal(tb3.Text), Convert.ToString(tb4.Text), Convert.ToString(tb5.Text));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";
                    RefreshData();
                    tb2.Clear();
                    tb3.Clear();
                    tb4.Clear();
                    tb5.Clear();
                }
                else
                {
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Проверьте правильность  введенных данных";
                }
            }
            catch
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Проверьте правильность  введенных данных";
            }
        }

        private void izm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb4.Text) && !String.IsNullOrWhiteSpace(tb5.Text) && (tb3.Text).Length > 5 && (tb4.Text).Length >= 4 && (tb5.Text).Length >= 6 && (tb2.Text).Length > 4)
                {
                    new clientsTableAdapter().UpdateQuery(Convert.ToString(tb2.Text), Convert.ToDecimal(tb3.Text), Convert.ToString(tb4.Text), Convert.ToString(tb5.Text), Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно изменены";
                    RefreshData();
                }
                else
                {
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Проверьте правильность  введенных данных";
                }
            }
            catch
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Выберите строку из таблицы или измените данные";
            }
        }

        private void udal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new clientsTableAdapter().DeleteQuery(Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно удалены";
                RefreshData();
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

        private void tb2_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
                    }

        private void tb2_PreviewTextInput(object sender, TextCompositionEventArgs e) //анг и русский алфафвит пробел точка 
        {

            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ .".IndexOf(e.Text) < 0;
        }

        private void tb3_PreviewTextInput(object sender, TextCompositionEventArgs e) //только цыфры
        {
          e.Handled = "0123456789".IndexOf(e.Text) < 0;
           
        }

        private void tb4_PreviewTextInput(object sender, TextCompositionEventArgs e) //только цыфры + буквы англ знаки пробел
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            // e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ._".IndexOf(e.Text) < 0;
        }

        private void tb5_PreviewTextInput(object sender, TextCompositionEventArgs e) //только цыфры + буквы англ знаки пробел
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            //  e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ._".IndexOf(e.Text) < 0;
        }

        private void tb3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void tb4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void tb5_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
    }
}
