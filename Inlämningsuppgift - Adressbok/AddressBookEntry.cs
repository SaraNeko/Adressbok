using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift___Adressbok
{
    class AddressBookEntry
    {
        private Regex SPLIT_REGEX = new Regex(", ");

        public readonly string Namn;
        public readonly string Gatuadress;
        public readonly string Postnummer;
        public readonly string Postort;
        public readonly string Telefon;
        public readonly string Epost;

        public AddressBookEntry(string informationString)
        {
            string[] information = SPLIT_REGEX.Split(informationString);

            Namn = information[0];
            Gatuadress = information[1];
            Postnummer = information[2];
            Postort = information[3];
            Telefon = information[4];
            Epost = information[5];
        }

        public AddressBookEntry(string namn, string gatuadress, string postnummer, string postort, string telefon, string epost)
        {
            Namn = namn;
            Gatuadress = gatuadress;
            Postnummer = postnummer;
            Postort = postort;
            Telefon = telefon;
            Epost = epost;
        }

        public override string ToString()
        {
            return String.Join(", ", new string[] { Namn, Gatuadress, Postnummer, Postort, Telefon, Epost });
        }
    }
}
