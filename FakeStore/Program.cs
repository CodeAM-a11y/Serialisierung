using System.Text.Json;

namespace FakeStore;

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
            foreach (Product product in products)
            {
                Console.WriteLine(product.title+" "+product.price+" "+product.description);
                Console.WriteLine(product.rating.count);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}