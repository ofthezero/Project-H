using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel  //Администратор----------------------------------------------LE RICHMOND КОМНАТЫ---------------------------------------------------------
{
    public partial class AdminRooms : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public AdminRooms()
        {
            InitializeComponent(); RefreshData();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);
        }

        public void RefreshData()
        {
            room_categoriesTableAdapter adapter1 = new room_categoriesTableAdapter();
            DataSet1.room_categoriesDataTable table1 = new DataSet1.room_categoriesDataTable();
            adapter1.Fill(table1);
            tb4.ItemsSource = table1;
            tb4.DisplayMemberPath = "Наименование";
            tb4.SelectedValuePath = "Код типа";

            AdressHotelTableAdapter adapter2 = new AdressHotelTableAdapter();
            DataSet1.AdressHotelDataTable table2 = new DataSet1.AdressHotelDataTable();
            adapter2.Fill(table2);
            tb7.ItemsSource = table2;
            tb7.DisplayMemberPath = "ulica";
            tb7.SelectedValuePath = "id_adress";

            roomsTableAdapter adapter = new roomsTableAdapter(); DataSet1.roomsDataTable table = new DataSet1.roomsDataTable(); adapter.Fill(table);
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
                if (tb2.Text != null & tb3.Text != null & tb4.Text != null)
                {
                    if (dg.SelectedItem != null) tb2.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[0].ToString(); 
                    if (dg.SelectedItem != null) tb3.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); 
                    if (dg.SelectedItem != null) tb4.SelectedValue = (dg.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); 
                    if (dg.SelectedItem != null) tb7.SelectedValue = (dg.SelectedItem as DataRowView).Row.ItemArray[4].ToString();
                    if (dg.SelectedItem != null)

                    if ((dg.SelectedItem as DataRowView).Row.ItemArray[3].ToString().Equals("Свободно"))
                    {
                        radioButton_Свободно.IsChecked = true;
                    }
                    else
                    {
                        radioButton_Занято.IsChecked = true;
                    }
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
            tb4.SelectedValue = -1;
            radioButton_Свободно.IsChecked = false;
            radioButton_Занято.IsChecked = false;
        }

        private void BLA_Click(object sender, RoutedEventArgs e)
        {
            Administrator s = new Administrator();
            s.Show();
            this.Close();
        }

        public bool addRoom(string no, int type, string phone, string status, int adress)
        {

            string insertQuerry = "INSERT INTO rooms(room_no, room_type, phone, status) VALUES (@no,@type,@ph,@sts,@adr)";
            SqlCommand command = new SqlCommand(insertQuerry, con);
            
            command.Parameters.Add("@no", SqlDbType.Int).Value = no;
            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            command.Parameters.Add("@ph", SqlDbType.Int).Value = phone;
            command.Parameters.Add("@sts", SqlDbType.VarChar).Value = status;
            command.Parameters.Add("@adr", SqlDbType.VarChar).Value = adress;

            con.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        public bool editRoom(string no, int type, string phone, string status, int adress)
        {
            string editQuerry = "UPDATE rooms SET room_type=@type,phone=@ph,status=@sts, id_adress=@adr WHERE room_no=@no";
            SqlCommand command = new SqlCommand(editQuerry, con);
           
            command.Parameters.Add("@no", SqlDbType.VarChar).Value = no;
            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            command.Parameters.Add("@ph", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@sts", SqlDbType.VarChar).Value = status;
            command.Parameters.Add("@adr", SqlDbType.VarChar).Value = adress;

            con.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        private void dob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string statu = (radioButton_Свободно.IsChecked == true) ? "Свободно" : "Занято";
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && (tb2.Text).Length >=3 && (tb3.Text).Length >= 4 )
                {
                    new roomsTableAdapter().InsertQuery(Convert.ToInt32(tb2.Text), Convert.ToInt32(tb4.SelectedValue), Convert.ToInt32(tb3.Text), statu, Convert.ToInt32(tb7.SelectedValue));
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

            if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb4.Text) && (tb2.Text).Length >= 3 && (tb3.Text).Length >= 4)
            {
                string no = tb2.Text;
                int type = Convert.ToInt32(tb4.SelectedValue.ToString());
                string ph = tb3.Text;
                string status = (radioButton_Свободно.IsChecked == true) ? "Свободно" : "Занято";
                int adress =  Convert.ToInt32(tb7.SelectedValue.ToString());


                if (editRoom(no, type, ph, status, adress))
                {
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно изменены";
                    RefreshData();
                }
                else
                {
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Выберите строку из таблицы или измените данные";
                }
            }
            else
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Выберите строку из таблицы или измените данные";
            }
        }

        private void udal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new roomsTableAdapter().DeleteQuery(Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно удалены";
                RefreshData();

                tb2.Clear();
                tb3.Clear();
                tb4.SelectedValue = -1;
                radioButton_Свободно.IsChecked = false;
                radioButton_Занято.IsChecked = false;
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

        private void tb3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
             e.Handled = "0123456789 ,".IndexOf(e.Text) < 0; //цифры пробел запятая 
            //   e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ".IndexOf(e.Text) < 0; //русские буквы и пробел
        }

        private void tb2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0; //цифры пробел запятая 
        }

        private void tb2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void tb3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
    }
}
