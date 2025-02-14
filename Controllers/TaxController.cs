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
            new Tax { Id = 1, Name = "VAT", Rate = 0.15m, Deducted = false },
            new Tax { Id = 2, Name = "TL Levy", Rate = 0.01m, Deducted = true },
            new Tax { Id = 3, Name = "Get Fund Levy", Rate = 0.025m, Deducted = false },
            new Tax { Id = 4, Name = "Covid Levy", Rate = 0.01m, Deducted = false },
            new Tax { Id = 5, Name = "NHIL", Rate = 0.025m, Deducted = false },
            
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
            existingTax.Name = tax.Name;
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
