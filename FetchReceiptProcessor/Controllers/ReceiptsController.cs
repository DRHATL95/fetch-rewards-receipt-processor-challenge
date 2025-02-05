using Microsoft.AspNetCore.Mvc;

namespace FetchReceiptProcessor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReceiptsController(ReceiptService receiptService) : ControllerBase
    {
        [HttpPost("/process")]
        public ActionResult<string> ProcessReceipt([FromBody] Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = receiptService.ProcessReceipt(receipt);

            return Ok(new { Id = id });
        }

        [HttpGet("/{id}/points")]
        public IActionResult GetPointsForReceipt(string id)
        {
            var points = receiptService.GetReceiptPointsForId(id);

            if (!int.TryParse(points, out var parsedResult))
            {
                return Ok(new { Points = points });
            }

            if (parsedResult == 0)
            {
                return NotFound();
            }

            return Ok(new { Points = points });
        }
    }
}
