using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            Crud bewerkingen = new Crud();

            Leerlingen nieuweLijst = new Leerlingen
            {
                LeerlingLijst = new List<Leerling>
                {
                    new Leerling
                    {
                        Naam = "Janssens",
                        Voornaam = "Jan",
                        Geboortedatum = new DateTime(2001, 2, 23),
                        Klas = "5IB",
                        Punten = new List<Punt>
                        {
                            new Punt("Frans", 9.5),
                            new Punt("Wiskunde", 5.5)
                        }
                    },

                    new Leerling
                    { 
                        Naam = "Willems",
                        Voornaam = "Wim",
                        Geboortedatum = new DateTime(2001, 2, 23),
                        Klas = "6IB",
                        Punten = new List<Punt>
                        {
                            new Punt("Frans", 10),
                            new Punt("Wiskunde", 8.5)
                        }
                    }
                }
            };

            //Maak een JSON object van het C# object nieuwelijst -> SerializeObject()
            string json = JsonConvert.SerializeObject(nieuweLijst, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();

            //Maak een C# object bestaandelijst van een JSON object -> DeserializeObject()
            Leerlingen bestaandelijst = JsonConvert.DeserializeObject<Leerlingen>(json);
            bestaandelijst.LeerlingLijst[0].Klas = "6IB";
      
            //Maak terug een JSON object
            json = JsonConvert.SerializeObject(bestaandelijst, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();


            foreach (Leerling ll in bestaandelijst.LeerlingLijst)
            {
                Console.WriteLine(ll.Naam + " " + ll.Voornaam);
            }
            Console.ReadLine();

            //Create
            bewerkingen.VoegPuntToe(bestaandelijst, "Willems", "Wim", "NaWe", 7.5);
            //Read
            bewerkingen.ToonLeerlingen(bestaandelijst);
            bewerkingen.ToonPunten(json, "Willems", "Wim");
            //Update
              bewerkingen.PasPuntAan(bestaandelijst, "Willems", "Wim", "NaWe", 9.5);
            //Delete
            bewerkingen.VerwijderPunten(bestaandelijst, "Willems", "Wim", "NaWe");


            json = JsonConvert.SerializeObject(bestaandelijst, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();
        }

    }

    public class Leerlingen
    {
        public List<Leerling> LeerlingLijst { get; set; }
    }

    public class Leerling
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Klas { get; set; }
        public List<Punt> Punten { get; set; }
        
    }

    public class Punt
    {
        public string Vak { get; set; }
        public double Punten { get; set; }

        public Punt(string vak, double punten)
        {
            this.Vak = vak;
            this.Punten = punten;
        }

    }

    public class Crud
    {
        public void PasPuntAan(Leerlingen lijst, string v1, string v2, string v3, double v4)
        {
            int teller = 0;
            foreach (Leerling ll in lijst.LeerlingLijst)
            {
                if (ll.Naam == v1 && ll.Voornaam == v2)
                {
                    for (int i = 0; i < lijst.LeerlingLijst[teller].Punten.Count; i++)
                    {

                        if (lijst.LeerlingLijst[teller].Punten[i].Vak == v3)
                        {
                            lijst.LeerlingLijst[teller].Punten[i].Punten = v4;
                        }
                    }

                }
                teller++;
            }

        }

        public void ToonLeerlingen(Leerlingen lijst)
        {

            foreach (Leerling ll in lijst.LeerlingLijst)
            {

                Console.WriteLine(ll.Naam + " " + ll.Voornaam);

            }
        }

            public void ToonPunten(string lijst, string v1, string v2)
        {
            int tel = 0;
            //werkt niet en ik vind echt nie wrm ik krijg altijd CRUD_JSON.Punt bij wat ik ook doe
            Leerlingen bestaandelijst = JsonConvert.DeserializeObject<Leerlingen>(lijst);
            foreach(Leerling ll in bestaandelijst.LeerlingLijst)
            {
                if (ll.Naam == v1 && ll.Voornaam == v2)
                {
                    for (int i = 0; i < bestaandelijst.LeerlingLijst[tel].Punten.Count; i++)
                    {
                        Console.WriteLine(bestaandelijst.LeerlingLijst[tel].Punten[i].Punten);
                    }

                }
                tel++;
            }
                       
        }

        public void VerwijderPunten(Leerlingen lijst, string v1, string v2, string v3)
        {
            int teller = 0;
            foreach (Leerling ll in lijst.LeerlingLijst)
            {
                if (ll.Naam == v1 && ll.Voornaam == v2)
                {
                    for (int i = 0; i < lijst.LeerlingLijst[teller].Punten.Count; i++)
                    {

                        if (lijst.LeerlingLijst[teller].Punten[i].Vak == v3)
                        {
                            lijst.LeerlingLijst[teller].Punten[i] = null; 
                        }
                    }

                }
                teller++;
            }
        }

        public void VoegPuntToe(Leerlingen lijst, string v1, string v2, string v3, double v4)
        {
            int teller = 0;
            foreach (Leerling ll in lijst.LeerlingLijst)
            {
                if (ll.Naam == v1 && ll.Voornaam == v2)
                {
                    lijst.LeerlingLijst[teller].Punten.Add( new Punt(v3, v4));

                }
                teller++;
            }
        }
    }


}
