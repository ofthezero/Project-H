using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Configuration;
using TheHotel.DataSet1TableAdapters;
using System.Data;

namespace TheHotel  //Администратор----------------------------------------------LE RICHMOND БРОНИРОВАНИЕ---------------------------------------------------------
{
    public partial class AdminBook : Window
    {
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;
        SqlConnection con = new SqlConnection();

        public AdminBook()
        {
            InitializeComponent(); RefreshData();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);
        }
       
        public void RefreshData()
        {
            room_categoriesTableAdapter adapter1 = new room_categoriesTableAdapter(); //вывод данных - тип комнаты
            DataSet1.room_categoriesDataTable table1 = new DataSet1.room_categoriesDataTable();
            adapter1.Fill(table1); tb3.ItemsSource = table1;           
            tb3.DisplayMemberPath = "Наименование"; 
            tb3.SelectedValuePath = "Код типа";

            clientsTableAdapter adapter133 = new clientsTableAdapter(); //вывод данных - инициалы гостя
            DataSet1.clientsDataTable table133 = new DataSet1.clientsDataTable();
            adapter133.Fill(table133); tb2.ItemsSource = table133;
            tb2.DisplayMemberPath = "Инициалы";
            tb2.SelectedValuePath = "Код гостя";

            AdressHotelTableAdapter adapter134 = new AdressHotelTableAdapter(); //вывод данных - адрес отеля
            DataSet1.AdressHotelDataTable table134 = new DataSet1.AdressHotelDataTable();
            adapter134.Fill(table134); tb7.ItemsSource = table134;
            tb7.DisplayMemberPath = "ulica";
            tb7.SelectedValuePath = "id_adress";

            reservationTableAdapter adapter = new reservationTableAdapter(); DataSet1.reservationDataTable table = new DataSet1.reservationDataTable(); adapter.Fill(table);
            dg.ItemsSource = table;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) //управление окном
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

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e) //вывод данных из таблицы в текст-комбо боксы
        {

            if (dg.SelectedItem != null)
            {
                if (dg.SelectedItem != null) tb2.SelectedValue = (dg.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); 
                if (dg.SelectedItem != null) tb4.SelectedValue = (dg.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); 
                if (dg.SelectedItem != null) tb5.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[3].ToString();
                if (dg.SelectedItem != null) tb6.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[4].ToString();
                if (dg.SelectedItem != null) tb7.SelectedValue = (dg.SelectedItem as DataRowView).Row.ItemArray[5].ToString();

                else { } 
            }
            else { }
        }

        private void Clear_Click(object sender, RoutedEventArgs e) //функция - очистить
        {
            tb_error.Text = "";
            tb_ok.Text = "";
            tb2.SelectedValue = -1;
            tb3.SelectedValue = -1;
            tb4.SelectedValue = -1;
            tb7.SelectedValue = -1;
            tb5.Text = Convert.ToString(DateTime.Now);
            tb6.Text = Convert.ToString(DateTime.Now);
        }

        private void BLA_Click(object sender, RoutedEventArgs e) //назад - переход в главное окно администратора
        {
            Administrator s = new Administrator();
            s.Show();
            this.Close();
        }

        private void dob_Click(object sender, RoutedEventArgs e) //добавление данных в таблицу reservation и изменение статуса комнаты в таблице rooms
        {
            try
            {
                if (tb5.SelectedDate < DateTime.Today)
                {
                    //ошибка при выборе даты пребываения ранее сегодняшнего дня
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Нельзя забронировать на предыдушие дни";
                }
                else if (tb6.SelectedDate < tb5.SelectedDate)
                {
                    //ошибка при выборе даты выезда ранее даты пребывания
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Дата выезда не может быть раньше даты заселения";
                }
                else if (tb2.SelectedIndex > -1 && tb4.SelectedIndex > -1)
                {
                    //добавление данных в таблицу reservation
                    new reservationTableAdapter().InsertQuery((int)tb2.SelectedValue, (int)tb4.SelectedValue, Convert.ToString(tb5.Text), Convert.ToString(tb6.Text), (int)tb7.SelectedValue);
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";

                    //изменение статуса комнаты в таблице rooms
                    new roomsTableAdapter().UpdateQuery1("Занято", (int)tb2.SelectedValue);

                    RefreshData();
                }
                else { tb_ok.Text = ""; tb_error.Text = "⚠ Проверьте правильность введенных данных"; }
            }
            catch { tb_ok.Text = ""; tb_error.Text =  "⚠ Проверьте правильность введенных данных"; }
        }

        public bool setReservRoom(string rno, string sts)
        {
            string updateQuery = "UPDATE rooms SET status=@sts WHERE room_no=@rno";
            SqlCommand command = new SqlCommand(updateQuery, con);

            command.Parameters.Add("@rno", SqlDbType.VarChar).Value = rno;
            command.Parameters.Add("@sts", SqlDbType.VarChar).Value = sts;
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

        public bool editReserv(int reserId, string client_id, string roomNo, DateTime date_in, DateTime date_out, int id_adress)
        {
            string insertQuerry = "UPDATE reservation SET client_id=@cld,room_no=@Rno,date_in=@Din,date_out=@Dout, id_adress = @Adr WHERE reserv_id=@rid";
            
            SqlCommand command = new SqlCommand(insertQuerry, con);
           
            command.Parameters.Add("@rid", SqlDbType.Int).Value = reserId;
            command.Parameters.Add("@cld", SqlDbType.VarChar).Value = client_id;
            command.Parameters.Add("@Rno", SqlDbType.VarChar).Value = roomNo;
            command.Parameters.Add("@Din", SqlDbType.Date).Value = date_in;
            command.Parameters.Add("@Dout", SqlDbType.Date).Value = date_out;
            command.Parameters.Add("@Adr", SqlDbType.Int).Value = id_adress;

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

        public DataTable roomByType(int type)
        {
            string selectQuery = "SELECT * FROM rooms WHERE room_type=@type AND status ='Свободно'";
            SqlCommand command = new SqlCommand(selectQuery, con);
            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public int typeByRoomNo(string rno)
        {
            string selectQuery = "SELECT room_type FROM rooms WHERE room_no=@rno";
            SqlCommand command = new SqlCommand(selectQuery, con);
            command.Parameters.Add("@rno", SqlDbType.VarChar).Value = rno;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return Convert.ToInt32(table.Rows[0][0].ToString());
        }

        private void izm_Click(object sender, RoutedEventArgs e) //изменение данных в таблице resservation и измение статуса комнаты при ее изменении в таблице rooms
        {
            try
            {
                int reservId = Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]); //код бронирования
                string rno = Convert.ToString((dg.SelectedItems[0] as DataRowView).Row.ItemArray[2]); //код комнаты из таблицы
                string client_id = tb2.SelectedValue.ToString(); //код гостя
                string roomNo = tb4.SelectedValue.ToString(); //код комнаты 
                DateTime dIn = Convert.ToDateTime(tb5.Text); //дата пребывния
                DateTime dOut = Convert.ToDateTime(tb6.Text); //дата чек-аута
                int id_adress = Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[5]); //код бронирования


                if (editReserv(reservId, client_id, roomNo, dIn, dOut, id_adress) && setReservRoom(roomNo, "Занято") && setReservRoom(rno, "Свободно"))
                {
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно изменены";               
                    RefreshData();          
                }
                else
                { tb_ok.Text = ""; tb_error.Text = "⚠ Выберите строку из таблицы или измените данные"; }
            }
            catch { tb_ok.Text = ""; tb_error.Text = "⚠ Выберите строку из таблицы или измените данные"; }
        }

        public bool removeReserv(int id)
        { 
            string insertQuerry = "DELETE FROM reservation WHERE reserv_id=@id";

            SqlCommand command = new SqlCommand(insertQuerry, con);
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
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

        private void udal_Click(object sender, RoutedEventArgs e) //удаление данных из таблицы reservation а также измениние статуса комнаты в таблице rooms       
        {  
            try
            {
                int reservId = Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]);
                string rno = Convert.ToString((dg.SelectedItems[0] as DataRowView).Row.ItemArray[2]);
           
                if (removeReserv(reservId) && setReservRoom(rno, "Свободно"))
                {
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно удалены";
                    RefreshData();
                    tb2.SelectedValue = -1;
                    tb3.SelectedValue = -1;
                    tb4.SelectedValue = -1;
                    tb7.SelectedValue = -1;
                    tb5.Text = Convert.ToString(DateTime.Now);
                    tb6.Text = Convert.ToString(DateTime.Now);
                }
            }
            catch
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Выберите строку из таблицы для удаления";
            }
        }

        private void exitt_Click(object sender, RoutedEventArgs e)   //выход из аккаунта
        {
            MainWindow Ma = new MainWindow();
            Ma.Show();
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            string curr = tb7.SelectedIndex.ToString();
            int currentValue = (Convert.ToInt16(curr));
            int currentValue2 = currentValue + 1;

            string curr1 = tb3.SelectedIndex.ToString();
            int currentValue1 = (Convert.ToInt16(curr1));
            int currentValue21 = currentValue1 + 1;

            roomsTableAdapter adapter13 = new roomsTableAdapter();
            DataSet1.roomsDataTable table13 = new DataSet1.roomsDataTable();
            adapter13.Fill(table13); table13.DefaultView.RowFilter = "[№ Отеля] =  '" + currentValue2 +"' and [Тип комнаты] =  '" + currentValue21 + "' and [Статус] = 'Свободно' ";
            tb4.ItemsSource = table13;
            tb4.DisplayMemberPath = "[Номер комнаты]";
            tb4.SelectedValuePath = "[Номер комнаты]";
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) btExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.X) btnExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

      

        private void ComboBox_SelectionChanged7(object sender, SelectionChangedEventArgs e)
        {
            string curr = tb7.SelectedIndex.ToString();
            int currentValue = (Convert.ToInt16(curr));
            int currentValue2 = currentValue + 1;

            roomsTableAdapter adapter13 = new roomsTableAdapter();
            DataSet1.roomsDataTable table13 = new DataSet1.roomsDataTable();
            adapter13.Fill(table13); table13.DefaultView.RowFilter = "[№ Отеля] =  '" + currentValue2 + "' and [Статус] = 'Свободно' ";
            tb4.ItemsSource = table13;
            tb4.DisplayMemberPath = "[Номер комнаты]";
            tb4.SelectedValuePath = "[Номер комнаты]";
        }
    }
}


        