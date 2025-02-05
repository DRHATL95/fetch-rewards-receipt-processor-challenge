using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FetchReceiptProcessor
{
    /// <summary>
    /// Represents the receipt used to process
    /// </summary>
    public class Receipt
    {
        [JsonPropertyName("retailer")]
        [RegularExpression(@"^[\w\s\-&]+$")]
        public required string Retailer { get; set; }

        [JsonPropertyName("purchaseDate")]
        public required DateTime PurchaseDate { get; set; }

        [JsonPropertyName("purchaseTime")]
        public required TimeOnly PurchaseTime { get; set; }

        [JsonPropertyName("items")]
        [MinLength(1)]
        public required Item[] Items { get; set; }

        [JsonPropertyName("total")]
        [RegularExpression(@"^\d+\.\d{2}$")]
        public required string Total { get; set; }
    }
}
