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
    public partial class Komnati : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public Komnati()
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
            tb4.DisplayMemberPath = "label";
            tb4.SelectedValuePath = "category_id";

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
            A s = new A();
            s.Show();
            this.Close();
        }

        public bool addRoom(string no, int type, string phone, string status)
        {

            string insertQuerry = "INSERT INTO rooms(room_no, room_type, phone, status) VALUES (@no,@type,@ph,@sts)";
            SqlCommand command = new SqlCommand(insertQuerry, con);
            
            command.Parameters.Add("@no", SqlDbType.Int).Value = no;
            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            command.Parameters.Add("@ph", SqlDbType.Int).Value = phone;
            command.Parameters.Add("@sts", SqlDbType.VarChar).Value = status;

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

        public bool editRoom(string no, int type, string phone, string status)
        {
            string editQuerry = "UPDATE rooms SET room_type=@type,phone=@ph,status=@sts WHERE room_no=@no";
            SqlCommand command = new SqlCommand(editQuerry, con);
           
            command.Parameters.Add("@no", SqlDbType.VarChar).Value = no;
            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            command.Parameters.Add("@ph", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@sts", SqlDbType.VarChar).Value = status;

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
                if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text))
                {
                    new roomsTableAdapter().InsertQuery(Convert.ToInt32(tb2.Text), Convert.ToInt32(tb4.SelectedValue), Convert.ToInt32(tb3.Text), statu);
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

            if (!String.IsNullOrWhiteSpace(tb2.Text) && !String.IsNullOrWhiteSpace(tb3.Text) && !String.IsNullOrWhiteSpace(tb4.Text))
            {
                string no = tb2.Text;
                int type = Convert.ToInt32(tb4.SelectedValue.ToString());
                string ph = tb3.Text;
                string status = (radioButton_Свободно.IsChecked == true) ? "Свободно" : "Занято";


                if (editRoom(no, type, ph, status))
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
    }
}
