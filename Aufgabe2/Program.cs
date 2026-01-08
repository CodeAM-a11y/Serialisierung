using System.Globalization;
using System.Text.Json;

namespace Aufgabe1;

class Person
{
    public string vorname { get; set; }
    public string name { get; set; }
    public int alter { get; set; }
    public DateTime geburtsdatum { get; set; }

    public Person(string vorname,string name, int alter, DateTime geburtsdatum)
    {
        this.vorname = vorname;
        this.name = name;
        this.alter = alter;
        this.geburtsdatum = geburtsdatum;
    }

    public override string ToString()
    {
        return $"Vorname: {vorname}, Name: {name}, Alter: {alter}, Geburstdatum: {geburtsdatum}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Person> ListePersonen = new List<Person>();
        string BenutzerEingabe;
        Boolean ProgrammFortführen = true;
        while (ProgrammFortführen)
        {
            Console.WriteLine("Möchten Sie Person eingeben oder Laden:");
            Console.WriteLine("Laden");
            Console.WriteLine("Speichern");
            Console.WriteLine("Löschen");
            Console.WriteLine("Beenden");
            Console.WriteLine("Liste");
            BenutzerEingabe = Console.ReadLine();
            if (BenutzerEingabe == "Speichern")
            {
                Console.WriteLine("Wie viele Personen möchten Sie speichern?");
                int AnzahlPersonen = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < AnzahlPersonen; i++)
                {
                    Console.WriteLine("Vornamen eingeben");
                    string tempVorname = Console.ReadLine();
                    Console.WriteLine("Nachnamen eingeben");
                    string tempName = Console.ReadLine();
                    Console.WriteLine("Alter eingeben");
                    int tempAlter = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Geburstdatum eingeben");
                    DateTime tempGeburstdatum = Convert.ToDateTime(Console.ReadLine());
                    ListePersonen.Add(new Person(tempVorname, tempName, tempAlter, tempGeburstdatum));
                }

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string JsonString = JsonSerializer.Serialize(ListePersonen, options);
                File.WriteAllText("personList.json", JsonString);
                Console.WriteLine("Personen erfolgreich gespeichert");
            }
            else if (BenutzerEingabe == "Laden")
            {
                ListePersonen =
                    JsonSerializer.Deserialize<List<Person>>(File.ReadAllText("personList.json"));
                Console.WriteLine("Geladene Person:");
                foreach (Person item in ListePersonen)
                {
                    Console.WriteLine(item);
                }
            }
            else if (BenutzerEingabe == "Löschen")
            {
                ListePersonen.Clear();
                Console.WriteLine("Erfolgreich Liste geleert");
            }
            else if (BenutzerEingabe=="Beenden")
            {
                ProgrammFortführen = false;
            }
            else if (BenutzerEingabe == "Liste")
            {
                foreach (Person item in ListePersonen)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}