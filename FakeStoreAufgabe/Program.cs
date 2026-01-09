using System.Text;
using System.Text.Json;

namespace FakeStore;

public class Suche
{
    public List<Product> _jsonData { get; set; }
    public IEnumerable<Product> tempErgebnissSuche { get; set; }

    public Suche(List<Product> jsonData)
    {
        this._jsonData = jsonData;
        this.tempErgebnissSuche = _jsonData;
    }

    public void SucheTitel(string SuchEingabe)
    {
        IEnumerable<Product> ErgebnissTitel = this.tempErgebnissSuche.Where(p => p.title.Equals(SuchEingabe));
        tempErgebnissSuche = ErgebnissTitel;
    }

    public void SucheProduktGruppe(string SuchEingabe)
    {
        IEnumerable<Product> ErgebnissGruppe= this.tempErgebnissSuche.Where(p => p.category.Equals(SuchEingabe));
        tempErgebnissSuche = ErgebnissGruppe;
    }

    public void SucheNachPreis(string SuchEingabe)
    {
        decimal günstigerAls=Convert.ToDecimal(SuchEingabe);
        IEnumerable<Product> ErgebnissPreis = this.tempErgebnissSuche.Where(p => günstigerAls>p.price);
        tempErgebnissSuche = ErgebnissPreis;
    }

    public void SucheNachRating(string SuchEingabe)
    {
        float ratingSuche=Convert.ToSingle(SuchEingabe);
        IEnumerable<Product> ErgebnissRating = this.tempErgebnissSuche.Where(p => ratingSuche < p.rating.rate);
        tempErgebnissSuche = ErgebnissRating;
    }

    public void SortierenPreis()
    {
        tempErgebnissSuche=tempErgebnissSuche.OrderBy(p => p.price);
    }

    public void SortierenRating()
    {
        tempErgebnissSuche=tempErgebnissSuche.OrderBy(p => p.rating.count);
    }

    public void SuchAnzeige()
    {
        foreach (var item in tempErgebnissSuche)
        {
            Console.WriteLine(item.title);
        }
    }

    public void SuchAnfrage()
    {
        StringBuilder SuchKriterien = new();
        StringBuilder SortierenKriterien = new();
        Console.WriteLine("Nach welchen Suchkriterien möchten Sie suchen? Zur Auswahl stehen: Titel, Preis," +
                          " Kategorie, Rating");
        SuchKriterien.Append(Console.ReadLine());
        if (SuchKriterien.ToString().Contains("Titel"))
        {
            Console.WriteLine("Geben Sie den Titel ein");
            SucheTitel(Console.ReadLine());
        }
        if (SuchKriterien.ToString().Contains("Preis"))
        {
            Console.WriteLine("Geben Sie den maximalen Preis an");
            SucheNachPreis(Console.ReadLine());
        }
        if (SuchKriterien.ToString().Contains("Kategorie"))
        {
            Console.WriteLine("Geben Sie die Kategorie an");
            SucheProduktGruppe(Console.ReadLine());
        }
        if (SuchKriterien.ToString().Contains("Rating"))
        {
            Console.WriteLine("Geben Sie Rating an das Sie mindestens haben");
            SucheNachRating(Console.ReadLine());
        }
        else
        {
            Console.WriteLine("Keine passende Eingabe gefunden");
        }
        Console.WriteLine("Wie möchten Sie sortieren? Zur Auswahl stehen: Preis, Rating");
        SortierenKriterien.Append(Console.ReadLine());
        if (SortierenKriterien.ToString().Contains("Preis"))
        {
            SortierenPreis();
        }
        else if (SortierenKriterien.ToString().Contains("Rating"))
        {
            SortierenRating();
        }
        else
        {
            Console.WriteLine("Keine passende Eingabe gefunden");
        }
        SuchAnzeige();
    }
}

public class Product
{
    public int id { get;set; }
    public string title { get;set; }
    public decimal price { get;set; }
    public string description { get;set; }
    public string category { get;set; }
    public string image { get;set; }
    public Rating rating { get;set; }
    
    public struct Rating
    {
        public float rate { get;set; }
        public int count { get;set; }
    }
    
}
class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://fakestoreapi.com/products"; 
        
        using HttpClient client = new();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();
            List<Product> products =JsonSerializer.Deserialize<List<Product>>(jsonData);
            Suche suche1 = new Suche(products);
            suche1.SuchAnfrage();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}