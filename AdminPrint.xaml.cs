using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //АДМИНИСТРАТОР--------------------------------------------------LE RICHMOND ОТЧЕТ---------------------------------------------------------
{
    public partial class AdminPrint : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;
        public AdminPrint()
        {
            InitializeComponent(); RefreshData();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();
            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);

            OUY(); OUY1();
        }

        public void RefreshData()
        {
            dadaTableAdapter adapter = new dadaTableAdapter(); DataSet1.dadaDataTable table = new DataSet1.dadaDataTable(); adapter.Fill(table);
            dg.ItemsSource = table;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Administrator A = new Administrator();
            A.Show();
            this.Close();
        }

        public void OUY()
        {
            var date = DateTime.Now;
            var data1 = DateTime.Now.ToLongDateString();
            var data2 = DateTime.Now.ToShortTimeString();
            vrem.Text = Convert.ToString(data2);
            vrem1.Text = Convert.ToString(data1);
            vrem2.Text = Convert.ToString(data1);


            SqlCommand myCommand = new SqlCommand($"SELECT MAX(status) AS Name, COUNT(status) AS Count FROM rooms GROUP BY status HAVING(MAX(status) = 'Свободно')", con);
            SqlDataReader myReader = null;

            con.Open();
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                svob.Text = (myReader["Count"].ToString());
            }
            con.Close();
        }

        public void OUY1()
        {
            SqlCommand myCommand = new SqlCommand($"SELECT MAX(status) AS Name, COUNT(status) AS Count FROM rooms GROUP BY status HAVING(MAX(status) = 'Занято')", con);
            SqlDataReader myReader = null;

            con.Open();
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                zan.Text = (myReader["Count"].ToString());
            }
            con.Close();
        }

        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape) btxit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.P) ppp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
