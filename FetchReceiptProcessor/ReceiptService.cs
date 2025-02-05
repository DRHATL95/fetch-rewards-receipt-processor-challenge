using Microsoft.Extensions.Caching.Memory;

namespace FetchReceiptProcessor
{
    public class ReceiptService(IMemoryCache memoryCache)
    {
        /// <summary>
        /// Processes a receipt and returns the id.
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public string ProcessReceipt(Receipt receipt)
        {
            var id = Guid.NewGuid().ToString();
            var points = GeneratePointsForReceipt(receipt);

            // TODO: Potentially check for duplicates
            memoryCache.Set(id, points);

            return id;
        }

        /// <summary>
        /// Retrieves the points for a given receipt id.
        /// </summary>
        /// <param name="id">The id to look for</param>
        /// <returns></returns>
        public string GetReceiptPointsForId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }

            return memoryCache.TryGetValue(id, out string points) 
                ? points 
                : string.Empty;
        }

        /// <summary>
        /// Generates points for a given receipt based on certain criteria.
        /// </summary>
        /// <param name="receipt">The receipt to be evaluated</param>
        /// <returns></returns>
        private static string GeneratePointsForReceipt(Receipt receipt)
        {
            var totalPoints = 0;

            // One point for every alphanumeric character in the retailer name.
            totalPoints += receipt.Retailer.Count(char.IsLetterOrDigit);

            // 50 points if the total is a round dollar amount with no cents.
            if (receipt.Total.EndsWith(".00"))
            {
                totalPoints += 50;
            }

            // 25 points if the total is a multiple of 0.25.
            if (decimal.Parse(receipt.Total) % 0.25m == 0)
            {
                totalPoints += 25;
            }

            // 5 points for every two items on the receipt.
            totalPoints += receipt.Items.Length / 2 * 5;

            // If the trimmed length of the item description is a multiple of 3, multiply the price by 0.2 and round up to the nearest integer. The result is the number of points earned.
            totalPoints += receipt.Items
                .Where(item => item.ShortDescription.Trim().Length % 3 == 0)
                .Sum(item => (int)Math.Ceiling(decimal.Parse(item.Price) * 0.2m));

            // 6 points if the day in the purchase date is odd.
            if (receipt.PurchaseDate.Day % 2 == 1)
            {
                totalPoints += 6;
            }

            // 10 points if the time of purchase is after 2:00pm and before 4:00pm.
            if (receipt.PurchaseTime.IsBetween(new TimeOnly(14, 0, 0), new TimeOnly(16, 0, 0)))
            {
                totalPoints += 10;
            }

            return totalPoints.ToString();
        }
    }
}
