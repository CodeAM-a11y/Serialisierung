using System.Text.Json;

namespace Aufgabe1;

class Person
{
    public string vorname { get; set; }
    public string name { get; set; }
    public int alter { get; set; }

    public Person(string vorname,string name, int alter)
    {
        this.vorname = vorname;
        this.name = name;
        this.alter = alter;
    }

    public override string ToString()
    {
        return $"Vorname: {vorname}, Name: {name}, Alter: {alter}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        string BenutzerEingabe;
        Console.WriteLine("Möchten Sie Person eingeben oder Laden:");
        Console.WriteLine("Laden");
        Console.WriteLine("Speichern");
        BenutzerEingabe = Console.ReadLine();
        if (BenutzerEingabe == "Speichern")
        {
            Console.WriteLine("Vornamen eingeben");
            string tempVorname=Console.ReadLine();
            Console.WriteLine("Nachnamen eingeben");
            string tempName=Console.ReadLine();
            Console.WriteLine("Alter eingeben");
            int tempAlter=Convert.ToInt32(Console.ReadLine());
            Person tempPerson = new Person(tempVorname, tempName, tempAlter);
            string JsonString= JsonSerializer.Serialize(tempPerson,new JsonSerializerOptions(){WriteIndented =  true});
            File.WriteAllText($"{tempPerson.name}.json", JsonString);
            Console.WriteLine("Person erfolgreich gespeichert");
        }
        else if (BenutzerEingabe == "Laden")
        {
            Console.WriteLine("Personen Name eingeben");
            string PersonenName = Console.ReadLine();
            Person PersonLaden= 
            JsonSerializer.Deserialize<Person>(File.ReadAllText($"{PersonenName}.json"));
            Console.WriteLine("Geladene Person:");
            Console.WriteLine(PersonLaden);
        }
    }
}