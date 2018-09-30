using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Inlämningsuppgift___Adressbok
{
    public partial class Adressbok : Form
    {
        static string SAVE_FILE_PATH = "Adressbok.txt";

        static string WINDOW_TITLE = "Adressbok";

        List<AddressBookEntry> entries = new List<AddressBookEntry>();

        AddressBookEntry entryBeingEdited = null;
               

        public Adressbok()
        {
            if (!File.Exists(SAVE_FILE_PATH)) { File.Create(SAVE_FILE_PATH).Dispose(); }

            InitializeComponent();
            this.Text = WINDOW_TITLE;

            LoadEntriesFromSaveFile();
            PopulateListBox();
        }

        /* Load all the address book entries from the save file. */
        private void LoadEntriesFromSaveFile()
        {
            StreamReader streamReader = new StreamReader(SAVE_FILE_PATH);
            string line = "";
            while ((line = streamReader.ReadLine()) != null)
            {
                AddressBookEntry entry = new AddressBookEntry(line);
                entries.Add(entry);
            }
            streamReader.Close();
        }

        /* Save all the address book entries to the save file. */
        private void SaveEntriesToSaveFile()
        {
            StreamWriter streamWriter = new StreamWriter(SAVE_FILE_PATH, false);
            foreach (AddressBookEntry entry in entries)
            {
                streamWriter.WriteLine(entry.ToString());
            }
            streamWriter.Close();
        }

        /* Populate the entries list box with all the saved address book entries. */
        private void PopulateListBox()
        {
            textBoxSearch.Text = ""; // Clear search box since we're going to show all entries.

            listBoxEntries.Items.Clear();
            foreach (AddressBookEntry entry in entries)
            {
                listBoxEntries.Items.Add(entry.ToString());
            }
        }

        /* Save an address book entry with the given information. */
        private void SparaAdress(string namn, string gatuadress, string postnummer, string postort, string telefon, string epost)
        {
            if (entryBeingEdited != null)
            {
                entries.Remove(entryBeingEdited);
                entryBeingEdited = null;
            }

            AddressBookEntry entry = new AddressBookEntry(namn, gatuadress, postnummer, postort, telefon, epost);
            entries.Add(entry);

            SaveEntriesToSaveFile();
            PopulateListBox();
            ClearInformationTextBoxes();
        }

        /* Delete the entry represented by the given string. */
        private void DeleteEntry(string requestedEntry)
        {
            entries.Remove(FindEntryByString(requestedEntry));

            SaveEntriesToSaveFile();
            PopulateListBox();
        }

        /* Find the entry represented by the given string. */
        private AddressBookEntry FindEntryByString(string requestedEntry)
        {
            foreach (AddressBookEntry entry in entries)
            {
                if (entry.ToString() == requestedEntry)
                {
                    return entry;
                }
            }
            throw new Exception($"Entry '{requestedEntry}' not found.");
        }

        /* Clear the information entry text boxes. */
        private void ClearInformationTextBoxes()
        {
            textBoxNamn.Clear();
            textBoxGatuadress.Clear();
            textBoxPostnummer.Clear();
            textBoxPostort.Clear();
            textBoxTelefon.Clear();
            textBoxEpost.Clear();
        }


        /* Event methods */

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string namn = textBoxNamn.Text;
            string gatuadress = textBoxGatuadress.Text;
            string postnummer = textBoxPostnummer.Text;
            string postort = textBoxPostort.Text;
            string telefon = textBoxTelefon.Text;
            string epost = textBoxEpost.Text;

            if (namn == "" || gatuadress == "" || postnummer == "" || postort == "" || telefon == "" || epost == "")
            {
                return;  // Don't allow saving if a text box is empty.
            }

            SparaAdress(namn, gatuadress, postnummer, postort, telefon, epost);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string selected = (string)listBoxEntries.SelectedItem;
            if (selected == null) { return; }  // Don't allow editing if no entry is selected.

            entryBeingEdited = FindEntryByString(selected);

            textBoxNamn.Text = entryBeingEdited.Namn;
            textBoxGatuadress.Text = entryBeingEdited.Gatuadress;
            textBoxPostnummer.Text = entryBeingEdited.Postnummer;
            textBoxPostort.Text = entryBeingEdited.Postort;
            textBoxTelefon.Text = entryBeingEdited.Telefon;
            textBoxEpost.Text = entryBeingEdited.Epost;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchString = textBoxSearch.Text.ToLower();
            PopulateListBox();  // Re-populate the list box in case a previous search is still in effect.
            foreach (AddressBookEntry entry in entries)
            {
                if (!(entry.Namn.ToLower().Contains(searchString) || entry.Postort.ToLower().Contains(searchString)))
                {
                    listBoxEntries.Items.Remove(entry.ToString());
                }
            }
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            PopulateListBox();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            entryBeingEdited = null;
            ClearInformationTextBoxes();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string selected = (string)listBoxEntries.SelectedItem;
            if (selected == null) { return; }  // Don't allow deleting if no entry is selected.

            DeleteEntry(selected);
        }
    }
}
