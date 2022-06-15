using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            string importPassvisiable = Pro.Text;
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
                                Receptionist R = new Receptionist();
                                R.Show();
                                this.Close();
                                break;
                            }

                        case "Администратор":
                            {
                                Administrator A = new Administrator();
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
            PassCode R = new PassCode();
            R.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminBook R = new AdminBook();
            R.Show();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && btnLogin2.Visibility == Visibility.Collapsed) btnLogin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.Enter && btnLogin.Visibility == Visibility.Collapsed) btnLogin2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.Enter) btnLogin2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.X) btnExit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            if (e.Key == Key.F3)
            {
                Administrator s = new Administrator();
                s.Show();
                this.Close();
            }
            if (e.Key == Key.F4)
            {
                Receptionist R = new Receptionist();
                R.Show();
                this.Close();
            }
        }

        private void pass_MouseEnter(object sender, MouseEventArgs e)
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Collapsed;
            Pro.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Collapsed;
            btnLogin2.Visibility = Visibility.Visible;
        }

        private void btnLogin_Click2(object sender, RoutedEventArgs e)
        {
            string importEmail = Email.Text;
           
            string importPassword = Pro.Text;

            for (int i = 0; i < DataSet1.Tables["User"].Rows.Count; i++)
            {

                if (importEmail == DataSet1.Tables["User"].Rows[i]["Логин"].ToString() | importEmail == DataSet1.Tables["User"].Rows[i]["Эл_почта"].ToString() && importPassword == DataSet1.Tables["User"].Rows[i]["Пароль"].ToString())
                {
                    string roleID = DataSet1.Tables["User"].Rows[i]["Должность"].ToString();

                    switch (roleID)
                    {
                        case "Ресепшионист":
                            {
                                Receptionist R = new Receptionist();
                                R.Show();
                                this.Close();
                                break;
                            }

                        case "Администратор":
                            {
                                Administrator s = new Administrator();
                                s.Show();
                                this.Close();
                                break;
                            }
                        default: break;
                    }
                }
                else { tb_error.Text = "⚠  Проверьте правильность введенных данных"; }
            }
        }

       

     
        private void tnxit_Click(object sender, RoutedEventArgs e) //не видим
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Visible;
            Pro.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Visible;
            btnLogin2.Visibility = Visibility.Collapsed;
            btnxit.Visibility = Visibility.Visible;
            tnxit.Visibility = Visibility.Collapsed;
            eye.Visibility = Visibility.Visible;
            ee.Visibility = Visibility.Collapsed;
        }

        private void eye_Click(object sender, RoutedEventArgs e) //видим
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Collapsed;
            Pro.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Collapsed;
            btnLogin2.Visibility = Visibility.Visible;
            btnxit.Visibility = Visibility.Collapsed;
            tnxit.Visibility = Visibility.Visible;
            eye.Visibility = Visibility.Collapsed;
            ee.Visibility = Visibility.Visible;
        }
    }
}
