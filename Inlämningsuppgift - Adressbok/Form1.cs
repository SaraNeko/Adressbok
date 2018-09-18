using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Inlämningsuppgift___Adressbok
{
    public partial class Adressbok : Form
    {
        Regex SPLIT_REGEX = new Regex(", ");
        static string SAVE_FILE_PATH = "Adressbok.txt";
        static string WINDOW_TITLE = "Adressbok";

        public Adressbok()
        {
            InitializeComponent();
            this.Text = WINDOW_TITLE;
            PopulateListBox();
        }

        void SparaAdress(string namn, string gatuadress, string postnummer,
            string postort, string telefon, string epost)
        {
            StreamWriter sw = new StreamWriter(SAVE_FILE_PATH, true);
            sw.WriteLine($"{namn}, {gatuadress}, {postnummer}, {postort}, {telefon}, {epost}");
            sw.Close();
            PopulateListBox();
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

        List<string> LaddaAdresser()
        {
            List<string> adressbok = new List<string>();
            StreamReader sr = new StreamReader(SAVE_FILE_PATH);
            string rad = "";
            while ((rad = sr.ReadLine()) != null)
            {
                adressbok.Add(rad);
            }
            sr.Close();
            return adressbok;
        }

        private void PopulateListBox() {
            listBox1.Items.Clear();
            foreach (string adress in LaddaAdresser())
            {
                listBox1.Items.Add(adress);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string entry = (string)listBox1.SelectedItem;
            if (entry == null) { return; }  // Don't allow editing if no entry is selected.

            string[] information = SPLIT_REGEX.Split(entry);
            textBox1.Text = information[0];
            textBox2.Text = information[1];
            textBox3.Text = information[2];
            textBox4.Text = information[3];
            textBox5.Text = information[4];
            textBox6.Text = information[5];
            deleteEntry(entry);
        }

        private void deleteEntry(string requestedEntry) {
            List<string> adressbok = LaddaAdresser();

            StreamWriter sw = new StreamWriter(SAVE_FILE_PATH, false);
            foreach (string entry in adressbok) {
                if (entry != requestedEntry)
                {                    
                    sw.WriteLine(entry);
                }
            }
            sw.Close();
            PopulateListBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchString = textBox7.Text.ToLower();
            PopulateListBox();  // Re-populate the list box in case a previous search is still in effect.
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                string entry = (string) listBox1.Items[i];
                string[] information = SPLIT_REGEX.Split(entry);
                string namn = information[0];
                string postort = information[3];

                if (!(namn.ToLower().Contains(searchString) || postort.ToLower().Contains(searchString)))
                {
                    listBox1.Items.RemoveAt(i);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            PopulateListBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string selected = (string)listBox1.SelectedItem;
            deleteEntry(selected);
        }
    }
}
