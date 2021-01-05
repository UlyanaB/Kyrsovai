using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    public partial class Registr : Form
    {
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
    }
}
