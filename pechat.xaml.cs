using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //АДМИНИСТРАТОР--------------------------------------------------LE RICHMOND ОТЧЕТ---------------------------------------------------------
{
    public partial class pechat : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;
        public pechat()
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
            A A = new A();
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
    }
}
