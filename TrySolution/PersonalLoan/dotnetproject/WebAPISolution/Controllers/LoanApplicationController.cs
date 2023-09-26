using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanApplicationDbContext _context;

        public LoanApplicationController(LoanApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanApplication>>> GetAllLoanApplications()
        {
            var loanApplications = await _context.LoanApplications.ToListAsync();
            return Ok(loanApplications);
        }

        [HttpPost]
        public async Task<ActionResult> AddLoanApplication(LoanApplication loanApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.LoanApplications.AddAsync(loanApplication);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanApplication(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid LoanApplication id");

            var loanApplication = await _context.LoanApplications.FindAsync(id);
              _context.LoanApplications.Remove(loanApplication);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
