using System.Diagnostics.Eventing.Reader;
using InvoiceApi;
using InvoiceApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {


        // private readonly TaxDbContext _context;

        // public TaxController(TaxDbContext context)
        // {
        //     _context = context;
        // }

        [HttpGet("filtered")]
        public IActionResult GetTaxData([FromQuery] string date, [FromQuery] string country, [FromQuery] string cityOrState)
        {
            if(string.IsNullOrEmpty(date))
                date = "2025-02-14";
            if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(cityOrState))
            {
                return BadRequest("Missing required parameters: date, country, and cityOrState");
            }

            Console.WriteLine($"Received Request: Date={date}, Country={country}, CityOrState={cityOrState}");
            
            //var taxes = await _context.Taxes.ToListAsync();

            var taxData = new List<Tax>{};
            // TODO: Replace with a logic to fetch tax data from database
            if (country == "USA" && cityOrState == "Florida" && date == "2025-07-31")
            {
                taxData = new List<Tax>
                {
                    new Tax { Id = 1, Title = "VAT", Rate = 0.0m, Deducted = false },
                    new Tax { Id = 2, Title = "Back-to-School tax holiday (tax-tree)", Rate = 0.0m, Deducted = false },
                };
            }
            else if (country == "USA" && cityOrState == "California")
            {
                taxData = new List<Tax>
                {
                    new Tax { Id = 1, Title = "VAT", Rate = 0.15m, Deducted = false },
                    new Tax { Id = 2, Title = "$ Levy", Rate = 0.01m, Deducted = true },
                    new Tax { Id = 3, Title = "State Sales Tax", Rate = 0.0725m, Deducted = false },
                    new Tax { Id = 4, Title = "Local Sales Tax", Rate = 0.025m, Deducted = false },
                    new Tax { Id = 5, Title = "CA Environmental Fee", Rate = 0.01m, Deducted = false },
                    
                };
            }
            else if (country == "USA" && cityOrState == "Florida")
            {
                taxData = new List<Tax>
                {
                    new Tax { Id = 1, Title = "VAT", Rate = 0.15m, Deducted = false },
                    new Tax { Id = 2, Title = "$ Levy", Rate = 0.01m, Deducted = true },
                    new Tax { Id = 3, Title = "State Sales Tax", Rate = 0.06m, Deducted = false },
                    new Tax { Id = 4, Title = "Discretionary Sales Surtax", Rate = 0.01m, Deducted = false },
                };
            }
            else if (country == "Germany")
            {
                taxData = new List<Tax>
                {
                    new Tax { Id = 1, Title = "VAT", Rate = 0.19m, Deducted = false },
                    new Tax { Id = 2, Title = "£ Levy", Rate = 0.01m, Deducted = true },
                    new Tax { Id = 3, Title = "Solidarity Surcharge", Rate = 0.055m, Deducted = false },
                    new Tax { Id = 4, Title = "Trade Tax", Rate = 0.145m, Deducted = false }
                };
            }
            else if (country == "Turkey" && date == "2025-02-14")
            {
                taxData = new List<Tax>
                {
                    new Tax { Id = 1, Title = "VAT", Rate = 0.15m, Deducted = false },
                    new Tax { Id = 2, Title = "TL Levy", Rate = 0.01m, Deducted = true },
                    new Tax { Id = 3, Title = "Get Fund Levy", Rate = 0.025m, Deducted = false },
                    new Tax { Id = 4, Title = "Covid Levy", Rate = 0.01m, Deducted = false },
                    new Tax { Id = 5, Title = "NHIL", Rate = 0.025m, Deducted = false },
                };
            }
            else if (country == "Turkey")
            {
                taxData = new List<Tax>
                {
                    new Tax { Id = 1, Title = "VAT", Rate = 0.15m, Deducted = false },
                    new Tax { Id = 2, Title = "TL Levy", Rate = 0.01m, Deducted = true },
                    new Tax { Id = 3, Title = "Getfund, NHIL & Covid Levy", Rate = 0.06m, Deducted = false },
                };
            }




            
            

            return Ok(taxData);


        }

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
