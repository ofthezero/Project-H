using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using TheHotel.DataSet1TableAdapters;

namespace TheHotel //ВСЕ--------------------------------------------------LE RICHMOND ОТПРАВКА КОДА ПОДТВЕРЖДЕНИЕ---------------------------------------------------------
{
    public partial class repass : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet1 DataSet1; UserTableAdapter userTableAdapter;

        string randomcode;
        public static string to;

        public repass()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["TheHotel.Properties.Settings.HotelConnectionString"].ConnectionString.ToString();

            DataSet1 = new DataSet1();
            userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DataSet1.User);

            dat.Text = "Москва, Россия " + DateTime.Now.ToLongDateString(); //вывод даты
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); } //управление окном
            catch { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) //выход - переход в окно авторизация
        {
            MainWindow R = new MainWindow();
            R.Show();
            this.Close();
        }

        private void guest_Click(object sender, RoutedEventArgs e) //отправка кода 
        {
            SqlConnection conn = new SqlConnection(@"Data Source=IAMROBERT\MSSQLSERVERR;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand myCommand = new SqlCommand($"SELECT Email AS [Эл.почта] FROM[User]", conn); conn.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();

            try
            {
                while (myReader.Read())
                {
                    if (myReader["Эл.почта"].ToString() == textBox1.Text)
                    {
                        string from, pass, messagebody; Random rand = new Random();
                        randomcode = (rand.Next(999999)).ToString(); //выбор рандомного числа
                        MailMessage message = new MailMessage();

                        to = (textBox1.Text).ToString(); //получение эл. почты получателя (сотрудника)
                        from = "itsatestpo@mail.ru"; //логин эл. почты отправителя (администратора)
                        pass = "YWAFBXcJrL1imU4GTuqA"; //пароль эл. почты отправителя (администратора)

                        messagebody = $" Ваш код восстановления пароля: <b> {randomcode} </b> <br> от LE RICHMOND <br> <br> <em> Пожалйста, проигнорируйте" +
                        $" данное письмо, если оно попало к Вам по ошибке. </em>  <br> <br> --<br> С уважением <br> Служба поддержки LE RICHMOND"; //сообщение

                        message.To.Add(to);
                        message.From = new MailAddress(from);
                        message.Body = messagebody;
                        message.Subject = "Служба поддержки LE RICHMOND"; //заголовок

                        SmtpClient smtp = new SmtpClient("smtp.mail.ru");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from, pass);

                        message.IsBodyHtml = true;

                        smtp.Send(message); //отправка кода

                        tb_error.Text = ""; tb_ok.Text = "✔ Код подтверждения отправлен"; //вывод об отправке сообщения
                    }
                    con.Close();
                }
            }
            catch { tb_error.Text = "⚠ Эл. почта не найдена"; tb_ok.Text = ""; } //вывод об ошибке отправки сообщения
        }

        private void broni_Click(object sender, RoutedEventArgs e) //проверка кода
        {
            if (randomcode == (textBox2.Text).ToString())
            {
                to = textBox1.Text;
                otch rp = new otch();
                this.Hide();
                rp.Show();
            }
            else
            { tb_ok.Text = ""; tb_error.Text = "⚠ Проверьте правильность ввода кода"; } //вывод об ошибке ввода кода
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
