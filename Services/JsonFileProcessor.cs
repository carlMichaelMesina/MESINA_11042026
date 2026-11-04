using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MESINA_11042026.Services
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }

    public interface IFileProcessor
    {
        Task<object> ProcessFileAsync(Stream fileStream);
    }

    public class JsonFileProcessor : IFileProcessor
    {
        public async Task<object> ProcessFileAsync(Stream fileStream)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var jsonItems = await JsonSerializer.DeserializeAsync<List<ItemDto>>(fileStream, jsonOptions);

            if (jsonItems == null)
            {
                return new { Message = "Empty or invalid JSON structure." };
            }

            // Filter items where active and price > 10
            var filteredItems = jsonItems.Where(i => i.IsActive && i.Price > 10.0m).ToList();

            return new
            {
                TotalItemsParsed = jsonItems.Count,
                FilteredCount = filteredItems.Count,
                Data = filteredItems
            };
        }
    }
}