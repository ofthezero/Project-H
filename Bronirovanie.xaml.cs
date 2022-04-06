using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Threading;
using TheHotel.DataSet1TableAdapters;
using System.Data;


namespace TheHotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Bronirovanie : Window
    {
        DispatcherTimer timerToStartKart = new DispatcherTimer();
        DateTime startDate = Convert.ToDateTime("12.02.2023 00:00:00");
        TimeSpan ToEnd;
        SqlConnection con = new SqlConnection();

        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        DataSet1 DataSet1;

        UserTableAdapter userTableAdapter;

        public Bronirovanie()
        {
            InitializeComponent(); RefreshData();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();

            DataSet1 = new DataSet1();

            userTableAdapter = new UserTableAdapter();

            userTableAdapter.Fill(DataSet1.User);




            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += new EventHandler(UpdateTimer_Tick);
            //timer.Interval = new TimeSpan(0, 0, 1);
            //timer.Start();

            //timerToStartKart.Interval = TimeSpan.FromMilliseconds(1);
            //timerToStartKart.Tick += Tick;
            //timerToStartKart.Start();


        }

        public void RefreshData()
        {

            room_categoriesTableAdapter adapter1 = new room_categoriesTableAdapter();
            DataSet1.room_categoriesDataTable table1 = new DataSet1.room_categoriesDataTable();
            adapter1.Fill(table1);
            tb3.ItemsSource = table1;
            tb3.DisplayMemberPath = "label";
            tb3.SelectedValuePath = "category_id";

            //room_categoriesTableAdapter adapter2 = new room_categoriesTableAdapter();
            //DataSet1.room_categoriesDataTable table2 = new DataSet1.room_categoriesDataTable();
            //adapter1.Fill(table2);
            //tb3.ItemsSource = table2;
            //tb3.DisplayMemberPath = "label";
            //tb3.SelectedValuePath = "category_id";

            //string selectQuery = "SELECT * FROM `rooms` WHERE `room_type`=@type AND `status` ='Свободно'";
            //SqlCommand command = new SqlCommand(selectQuery, con);
            //SqlParameter type = command.Parameters.Add("@type", SqlDbType.Int);
            //SqlDataAdapter adapter22 = new SqlDataAdapter(command);
            //DataTable table22 = new DataTable();
            //adapter22.Fill(table22);
            //tb4.ItemsSource = table22;
            //tb4.DisplayMemberPath = "room_no";
            //tb4.SelectedValuePath = "rooms_no";
            //con.Open();

            roomsTableAdapter adapter13 = new roomsTableAdapter();
            DataSet1.roomsDataTable table13 = new DataSet1.roomsDataTable();
            adapter13.Fill(table13);
            tb4.ItemsSource = table13;





            tb4.DisplayMemberPath = "room_no";
            tb4.SelectedValuePath = "room_no";

            table13.DefaultView.RowFilter = "status = 'Свободно'";
            table13.DefaultView.RowFilter = "room_type = room_no";
            //
            //table13.DefaultView.RowFilter = "room_type = room_no";


            reservationTableAdapter adapter = new reservationTableAdapter(); DataSet1.reservationDataTable table = new DataSet1.reservationDataTable(); adapter.Fill(table);
            dg.ItemsSource = table;

            //roomsTableAdapter adapter13 = new roomsTableAdapter();
            //DataSet1.roomsDataTable table13 = new DataSet1.roomsDataTable();
            //adapter13.Fill(table13);
            //tb4.ItemsSource = table13;





            //tb4.DisplayMemberPath = "room_no";
            //tb4.SelectedValuePath = "room_no";
            ////table13.DefaultView.RowFilter = "room_type = tb3.SelectedValuePath, status = 'Свободно'";
            //table13.DefaultView.RowFilter = "room_type = room_no";

            //string type = tb4;
            //SqlConnection con = new SqlConnection(@"Data Source = IAMROBERT\MSSQLSERVERR; Initial Catalog = Hotel; Integrated Security = True");
            ////string selectQuery = "SELECT  room_type = @type,  status = 'Свободно' FROM dbo.rooms";
            //SqlCommand command = new SqlCommand("SELECT  room_type = @type, status = 'Свободно' FROM dbo.rooms", con);
            //command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            //SqlDataAdapter adapter22 = new SqlDataAdapter(command);
            //DataTable table22 = new DataTable();

            //adapter22.Fill(table22);
            //tb4.DataContext = table22;
            //tb4.DisplayMemberPath = "room_no";
            //tb4.SelectedValuePath = "room_no";
            //return;

            //DataRowView row = (DataRowView)tb4.SelectedItem;

            //tb4.SelectedValue = row["status"].ToString();









        }


        // to get room type by room no чтобы получить тип номера по номеру
        // room type is int
        public int typeByRoomNo(string rno)
        {
            string selectQuery = "SELECT `room_type` FROM `rooms` WHERE `room_no`=@rno";
            SqlCommand command = new SqlCommand(selectQuery, con);
            command.Parameters.Add("@rno", SqlDbType.VarChar).Value = rno;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return Convert.ToInt32(table.Rows[0][0].ToString());

        }

        public bool setReservRoom(string rno, string sts)
        {
            string updateQuery = "UPDATE `room` SET `RoomStatus`=@sts WHERE `RoomId`=@rno";
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
        //private void UpdateTimer_Tick(object sender, EventArgs e)
        //{
        //    dat.Text = "Москва, Россия " + DateTime.Now.ToLongDateString();

        //}

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow R = new MainWindow();
            R.Show();
            this.Close();

        }






        //private void Tick(object sender, EventArgs e)
        //{
        //    Task.Factory.StartNew(async () =>
        //    {
        //        await Application.Current.Dispatcher.InvokeAsync(() =>
        //        {
        //            if (DateTime.Now < startDate)
        //            {
        //                ToEnd = startDate - DateTime.Now;
        //                DateTime aboba = DateTime.MinValue + ToEnd;
        //                TimerToStart.Text = ($"До начала события осталось {aboba.Year - 1} лет, {aboba.Month - 1} месяцев, {aboba.Day - 1} дней, {aboba.Hour} часов, {aboba.Minute} минут и {aboba.Second} секунд.");
        //            }
        //        });
        //    });
        //}

        private void button_room_Click(object sender, RoutedEventArgs e) //КОМНАТЫ
        {
            Komnati komnati = new Komnati();
            komnati.Show();
            this.Close();
        }

        private void button_reception_Click(object sender, RoutedEventArgs e) //БРОНЬ
        {
            Bron bron = new Bron();
            bron.Show();
            this.Close();
        }

        private void button_guest_Click(object sender, RoutedEventArgs e) //ГОСТИ
        {
            Gosti gosti = new Gosti();
            gosti.Show();
            this.Close();
        }

        private void sotrudniki_button_Click(object sender, RoutedEventArgs e) //СОТРУДНИКИ
        {
            Sotrudniki sotrudniki = new Sotrudniki();
            sotrudniki.Show();
            this.Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dg.SelectedItem != null)
            {
                if (tb2.Text != null & tb3.Text != null & tb4.Text != null & tb5.Text != null)
                {
                    if (dg.SelectedItem != null) //inn           
                        tb2.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (dg.SelectedItem != null)
                        tb3.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (dg.SelectedItem != null)
                        tb4.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); if (dg.SelectedItem != null)
                        tb5.Text = (dg.SelectedItem as DataRowView).Row.ItemArray[4].ToString();
                }
                else { }
            }
            else { }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tb_error.Text = "";
            tb_ok.Text = "";
            //tb2.Clear();
            //tb3.Clear();
            //tb4.Clear();
            //tb5.Clear();
        }

        private void BLA_Click(object sender, RoutedEventArgs e)
        {
            A s = new A();
            s.Show();
            this.Close();
        }

        private void dob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if(!String.IsNullOrWhiteSpace(tb2.Text))
                //if(!String.IsNullOrWhiteSpace(tb3.Text))
                //if(!String.IsNullOrWhiteSpace(tb4.Text))
                //if(!String.IsNullOrWhiteSpace(tb5.Text))


                new clientsTableAdapter().InsertQuery(Convert.ToString(tb2.Text), Convert.ToDecimal(tb4.Text), Convert.ToString(tb3.Text), Convert.ToString(tb5.Text));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно добавлены";
                RefreshData();

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
                //if(!String.IsNullOrWhiteSpace(tb2.Text))
                //if(!String.IsNullOrWhiteSpace(tb3.Text))
                //if(!String.IsNullOrWhiteSpace(tb4.Text))
                //if(!String.IsNullOrWhiteSpace(tb5.Text))


                new clientsTableAdapter().UpdateQuery(Convert.ToString(tb2.Text), Convert.ToDecimal(tb4.Text), Convert.ToString(tb3.Text), Convert.ToString(tb5.Text), Convert.ToInt32((dg.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                tb_error.Text = "";
                tb_ok.Text = "✔ Данные успешно изменены";
                RefreshData();

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
                //if(!String.IsNullOrWhiteSpace(tb2.Text))
                //if(!String.IsNullOrWhiteSpace(tb3.Text))
                //if(!String.IsNullOrWhiteSpace(tb4.Text))
                //if(!String.IsNullOrWhiteSpace(tb5.Text))


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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tb4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int rno = 0;
            //string selectQuery = "SELECT `room_type` FROM `rooms` WHERE `room_no`=@rno";
            //SqlCommand command = new SqlCommand(selectQuery, con);
            //command.Parameters.Add("@rno", SqlDbType.VarChar).Value = rno;
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //Convert.ToInt32(table.Rows[0][0].ToString());
            //return;
        }
    }
}
