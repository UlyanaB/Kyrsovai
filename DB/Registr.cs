using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

using Registrator;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;

namespace DB
{
    public partial class Registr : Form
    {
        private string Mail = "";
        private string Password = "";
        private string NameR = "";
        private string Login = "";

        public Registr()
        {
            InitializeComponent();
        }

        private void TextBMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormRegistr_Load(object sender, EventArgs e)
        {
            string PasNumber = "1234567890";
            string PasSymbol = "@!#№%:;,.^)(?/][";
            string PasLetterBig = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string PasLetterLittle = "qwertyuiopasdfghjklzxcvbnm";
            string Pas = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM@!#№%:;,.^)(?/][";
            Random rndCol = new Random();
            int Col = rndCol.Next(6, 20);
            Random rnd = new Random();
            TextBPassword.Text += PasNumber[rnd.Next(PasNumber.Length)];
            TextBPassword.Text += PasSymbol[rnd.Next(PasNumber.Length)];
            TextBPassword.Text += PasLetterBig[rnd.Next(PasNumber.Length)];
            TextBPassword.Text += PasLetterLittle[rnd.Next(PasNumber.Length)];
            for (int i = 4; i < Col; i++)
            TextBPassword.Text += Pas[rnd.Next(Pas.Length)];
        }

        //public void SaveBase

        private void BtnAdminRegistr_Click(object sender, EventArgs e)
        {
            Mail = TextBMail.Text;
            Password = TextBPassword.Text;
            NameR = TextBName.Text;
            Login = TextBLogin.Text;
            

            if (Mail == "" || Password == "" || NameR == "" || Login == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            Guid guid = Guid.NewGuid();
            using (Program.mainForm.sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                Program.mainForm.sqlConnection.Open();
                #region проверить уникальность
                using (SqlCommand sqlCommandUnique = new SqlCommand("select top 1 1 from dbo.AdminDB where logina = @login",
                                                                    Program.mainForm.sqlConnection))
                {
                    sqlCommandUnique.Parameters.AddWithValue("@login", Login);
                    if (sqlCommandUnique.ExecuteScalar() != null)
                    {
                        MessageBox.Show("Уже есть пользователь с таким логином");
                        return;
                    }
                }
                #endregion проверить уникальность

                #region добавить пользователя в промежуточную таблицу
                using (SqlCommand sqlCommandAdd = new SqlCommand("insert dbo.tmpAdminDB ( guid_admin,  dt,  logina,  passworda,  mail,  name) " +
                                                                                "values (@guid_admin, @dt, @logina, @passworda, @mail, @name)",
                                                                            Program.mainForm.sqlConnection))
                {
                    sqlCommandAdd.Parameters.AddWithValue("@guid_admin", guid);
                    sqlCommandAdd.Parameters.AddWithValue("@dt", DateTime.Now.AddHours(5));
                    sqlCommandAdd.Parameters.AddWithValue("@logina", Login);
                    sqlCommandAdd.Parameters.AddWithValue("@passworda", Password);
                    sqlCommandAdd.Parameters.AddWithValue("@mail", Mail);
                    sqlCommandAdd.Parameters.AddWithValue("@name", NameR);
                    sqlCommandAdd.ExecuteNonQuery();
                }
                #endregion добавить пользователя в промежуточную таблицу
            }
            #region отправить письмо для регистрации
            {
                try
                {
                    MailAddress to = new MailAddress(Mail);
                    MailAddress from = new MailAddress("Borisovets.u@yandex.ru", "Регистрация");
                    //MailAddress from = new MailAddress("bba2stud@yandex.ru", "Регистрация");
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Вы запрашивали регистрацию в программе";
                    m.Body = "<h2>Для завершения регистрации перейдите по <a href = " +
                                Registrate.prefix + "?id=" + guid.ToString() + @"/" + "target=\"_blank\" >ссылке</a></h2>";
                    m.IsBodyHtml = true;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.yandex.ru",
                        Port = 25,
                        //Credentials = new NetworkCredential("bba2stud@yandex.ru", "127z!vgy"),
                        Credentials = new NetworkCredential("Borisovets.u@yandex.ru", "VbhD{fect"),
                        EnableSsl = true,
                    };
                    smtp.Send(m);
                    MessageBox.Show("На указанный почтовый ящик отправленно письмо. " +
                                    "Для подтверждения регистрации нужно перейти в ссылке в письме." +
                                    "Ссылка актуальна в течении пяти часов.");
                    return;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неверный формат электронной почты. Почта должна иметь окончания - ****@****.**");
                    TextBMail.Clear();
                }
            }
            #endregion отправить письмо для регистрации
        }
    }
}
