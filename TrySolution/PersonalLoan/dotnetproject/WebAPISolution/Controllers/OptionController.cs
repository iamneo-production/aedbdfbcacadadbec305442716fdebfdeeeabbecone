using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly LoanApplicationDbContext _context;

        public OptionController(LoanApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Option>>> GetAllOptions()
        {
            var options = await _context.Options.ToListAsync();
            return Ok(options);
        }
[HttpGet("LoanTypes")]
public async Task<ActionResult<IEnumerable<string>>> Get()
{
    // Project the OptionTitle property using Select
    var loanTypes = await _context.Options
        .OrderBy(x => x.LoanType)
        .Select(x => x.LoanType)
        .ToListAsync();

    return loanTypes;
}
        [HttpPost]
        public async Task<ActionResult> AddOption(Option option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Option id");

            var option = await _context.Options.FindAsync(id);
              _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
