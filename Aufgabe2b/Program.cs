using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aufgabe1;

class Person
{
    public string vorname { get; set; }
    public string name { get; set; }
    public int alter { get; set; }
    public DateTime geburtsdatum { get; set; }
    public Person Vater {get;set;}
    public Person Mutter {get;set;}
    public Person Kind {get;set;}

    public Person()
    {
        
    }

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
                Person Mutter = new Person("Kati", "Mueller", 35,new DateTime(1990,12,12));
                Person Vatter = new Person("Timy","Mueller",37,new DateTime(1990,12,12));
                Person Kind = new Person("Ben", "Mueller",12,new DateTime(1990,12,12));
                Mutter.Kind = Kind;
                Vatter.Kind = Kind;
                Kind.Vater = Vatter;
                Kind.Mutter = Mutter;
                ListePersonen.Add(Mutter);
                ListePersonen.Add(Vatter);
                ListePersonen.Add(Kind);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.ReferenceHandler=ReferenceHandler.Preserve;
                                         string JsonString = JsonSerializer.Serialize(ListePersonen, options);
                File.WriteAllText("personList.json", JsonString);
                Console.WriteLine("Personen erfolgreich gespeichert");
            }
            else if (BenutzerEingabe == "Laden")
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.ReferenceHandler=ReferenceHandler.Preserve;
                ListePersonen =
                    JsonSerializer.Deserialize<List<Person>>(File.ReadAllText("personList.json"),options);
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