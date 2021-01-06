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
            else
            {
                try
                {
                    MailAddress from = new MailAddress(Mail);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неверный формат электронной почты. Почта должна иметь окончания - ****@****.**");
                    TextBMail.Clear();
                }
            }
        }
    }
}
