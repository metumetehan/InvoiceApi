using InvoiceApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        static private List<Tax> taxes = new List<Tax>
        {
            new Tax { Id = 1, Title = "VAT", Rate = 0.15m, Deducted = false },
            new Tax { Id = 2, Title = "TL Levy", Rate = 0.01m, Deducted = true },
            new Tax { Id = 3, Title = "Get Fund Levy", Rate = 0.025m, Deducted = false },
            new Tax { Id = 4, Title = "Covid Levy", Rate = 0.01m, Deducted = false },
            new Tax { Id = 5, Title = "NHIL", Rate = 0.025m, Deducted = false },
            
        };

        [HttpGet]
        public ActionResult<List<Tax>> GetTaxes()
        {
            return Ok(taxes);
        }

        [HttpGet("{id}")]
        public ActionResult<Tax> GetTax(int id)
        {
            var tax = taxes.FirstOrDefault(t => t.Id == id);
            if (tax == null)
            {
                return NotFound();
            }
            return Ok(tax);
        }

        [HttpPost]
        public ActionResult<Tax> CreateTax(Tax tax)
        {
            if (tax == null)
            {
                return BadRequest();
            }

            tax.Id = taxes.Max(t => t.Id) + 1;
            taxes.Add(tax);
            return CreatedAtAction(nameof(GetTax), new { id = tax.Id }, tax);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTax(int id, Tax tax)
        {
            if (tax == null)
            {
                return BadRequest();
            }
            var existingTax = taxes.FirstOrDefault(t => t.Id == id);
            if (existingTax == null)
            {
                return NotFound();
            }
            existingTax.Title = tax.Title;
            existingTax.Rate = tax.Rate;
            existingTax.Deducted = tax.Deducted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTax(int id)
        {
            var tax = taxes.FirstOrDefault(t => t.Id == id);
            if (tax == null)
            {
                return NotFound();
            }
            taxes.Remove(tax);
            return NoContent();
        }

    }
}
