namespace Serialisierung;
using System.Xml.Serialization;

public class JSONHero
{
    public string name { get; set; }
    public int alter { get; set; }
    public JSONHero partner { get; set; }
    public JSONHero(string name, int alter)
    {
        this.name = name;
    }
}

[Serializable()]
public class Hero()
{
    public string Name;
    public Hero Partner;
    
    [NonSerialized()]public string stuff;

    private int alter;

    public Hero(string name, int alter) : this()
    {
        this.Name = name;
        this.alter = alter;
    }

    public int getAge()
    {
        return alter;
    }
}

class Program
    {

        public static void XmlExample()
        {
            Hero batman = new Hero("Batman", 42);
            batman.stuff = "ABC";
            batman.Partner = new Hero("Robin", 23);
            XmlSerializer xml= new XmlSerializer(typeof(Hero));
            using (FileStream fs = File.Create("Batman.xml"))
            {
                xml.Serialize(fs, batman);
            }
        }

        static void Main(string[] args)
        {
            XmlSerializer xml= new XmlSerializer(typeof(Hero));
            XmlExample();
            Hero NewBatman = xml.Deserialize(new FileStream("Batman.xml", FileMode.Open)) as Hero;
            Console.WriteLine(NewBatman.Name);
        }
    }
