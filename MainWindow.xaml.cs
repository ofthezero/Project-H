using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //ВСЕ--------------------------------------------------LE RICHMOND АВТОРИЗАЦИЯ---------------------------------------------------------
{
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1;
        UserTableAdapter userTableAdapter;

        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();

            DataSet1 = new DataSet1(); userTableAdapter = new UserTableAdapter(); userTableAdapter.Fill(DataSet1.User);
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string importEmail = Email.Text;
            string importPassword = Password.Password;

            for (int i = 0; i < DataSet1.Tables["User"].Rows.Count; i++)
            {
                if (importEmail == DataSet1.Tables["User"].Rows[i]["Логин"].ToString() | importEmail == DataSet1.Tables["User"].Rows[i]["Эл_почта"].ToString() && importPassword == DataSet1.Tables["User"].Rows[i]["Пароль"].ToString())
                {
                    string roleID = DataSet1.Tables["User"].Rows[i]["Должность"].ToString();

                    switch (roleID)
                    {
                        case "Ресепшионист":
                            {
                                R R = new R();
                                R.Show();
                                this.Close();
                                break;
                            }

                        case "Администратор":
                            {
                                A A = new A();
                                A.Show();
                                this.Close();
                                break;
                            }
                        default: break;
                    }
                }
                else { tb_error.Text = "⚠  Проверьте правильность введенных данных"; }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PEREHOD_Click(object sender, RoutedEventArgs e)
        {
            repass R = new repass();
            R.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bron R = new Bron();
            R.Show();
        }
    }
}
