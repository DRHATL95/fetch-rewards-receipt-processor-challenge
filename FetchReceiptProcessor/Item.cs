using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FetchReceiptProcessor
{
    /// <summary>
    /// Represents an item on a receipt
    /// </summary>
    public class Item
    {
        [JsonPropertyName("shortDescription")]
        [RegularExpression(@"^[\w\s\-]+$")]
        public required string ShortDescription { get; set; }

        [JsonPropertyName("price")]
        [RegularExpression(@"^\d+\.\d{2}$")]
        public required string Price { get; set; }
    }
}
