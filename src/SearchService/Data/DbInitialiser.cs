using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitialiser
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("AuctionSearchDb", MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Colour, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();

            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();
            var items = await httpClient.GetItemsForSearchDb();
            Console.WriteLine(items.Count + " returned from auction service");
            if (items.Count > 0) await DB.SaveAsync(items);

            // if (count == 0)
            // {
            //     Console.WriteLine("No data = will attempt to send");
            //     var itemData = await File.ReadAllTextAsync("Data/auctions.json");
            //     var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            //     var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
            //     await DB.SaveAsync(items);
            // }
        }
    }
}