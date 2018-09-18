using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Inlämningsuppgift___Adressbok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void SparaAdress(string namn, string gatuadress, string postnummer, 
            string postort, string telefon, string epost)
        {
            string path = @"C:\Users\saras\Downloads\Programmering\Inläming\Adressbok.txt";
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine($"{namn}, {gatuadress}, {postnummer}, {postort}, {telefon}, {epost}");
            sw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string namn = textBox1.Text;
            string gatuadress = textBox2.Text;
            string postnummer = textBox3.Text;
            string postort = textBox4.Text;
            string telefon = textBox5.Text;
            string epost = textBox6.Text;
            SparaAdress(namn, gatuadress, postnummer, postort, telefon, epost);

        }
    }
}
