using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //АДМИНИСТРАТОР--------------------------------------------------LE RICHMOND ПОЛЬЗОВАТЕЛИ---------------------------------------------------------
{
    public partial class AdminEmployee : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public AdminEmployee()
        {
            InitializeComponent(); RefreshData();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);
        }

        public void RefreshData()
        {
            UserTableAdapter adapter = new UserTableAdapter(); DataSet1.UserDataTable table = new DataSet1.UserDataTable(); adapter.Fill(table);
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
                    if (dg.SelectedItem != null)
                        tb2.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (dg.SelectedItem != null)
                        tb3.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); if (dg.SelectedItem != null)
                        tb4.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[4].ToString(); if (dg.SelectedItem != null)
                        tb5.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[5].ToString(); if (dg.SelectedItem != null)
                        tb7.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
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
            tb7.Clear();
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
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb4.Text) && !String.IsNullOrWhiteSpace(tb5.Text) && !String.IsNullOrWhiteSpace(tb7.Text) && tb4.Text == "Ресепшионист" | tb4.Text == "Администратор" && (tb2.Text).Length >= 6 && (tb3.Text).Length >= 6 && (tb7.Text).Length >= 5 && (tb5.Text).Length >= 7)
                {
                    new UserTableAdapter().InsertQuery1(Convert.ToString(tb7.Text), Convert.ToString(tb2.Text), Convert.ToString(tb3.Text), Convert.ToString(tb4.Text), Convert.ToString(tb5.Text));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";
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
                tb_error.Text = "⚠ Проверьте правильность  введенных данных";
            }
        }

        private void izm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb4.Text) && !String.IsNullOrWhiteSpace(tb5.Text) && !String.IsNullOrWhiteSpace(tb7.Text) && tb4.Text == "Ресепшионист" | tb4.Text == "Администратор"  && (tb2.Text).Length >= 6 && (tb3.Text).Length >= 6 && (tb7.Text).Length >= 5 && (tb5.Text).Length >= 7)
                {
                    new UserTableAdapter().UpdateQuery(Convert.ToString(tb2.Text), Convert.ToString(tb3.Text), Convert.ToString(tb5.Text), Convert.ToString(tb7.Text), Convert.ToString(tb4.Text), Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
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
                new UserTableAdapter().DeleteQuery(Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно удалены";
                RefreshData();
                tb2.Clear();
                tb3.Clear();
                tb4.Clear();
                tb5.Clear();
                tb7.Clear();
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

        private void tb7_PreviewTextInput(object sender, TextCompositionEventArgs e) //инициалы только буквы и точка 
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ .-".IndexOf(e.Text) < 0; //русские буквы и пробел
        }

        private void tb2_PreviewTextInput(object sender, TextCompositionEventArgs e) //только английские буквы, цыфры точка,@#!%
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ._".IndexOf(e.Text) < 0;
        }

        private void tb4_PreviewTextInput(object sender, TextCompositionEventArgs e) //только рус буквы
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ".IndexOf(e.Text) < 0; 
        }

        private void tb5_PreviewTextInput(object sender, TextCompositionEventArgs e) //англ букы цыфры .,@_-
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 @.-_".IndexOf(e.Text) < 0;
        }

        private void tb3_PreviewTextInput(object sender, TextCompositionEventArgs e) //пароль
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 @.-_".IndexOf(e.Text) < 0;
        }

        private void tb2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
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
