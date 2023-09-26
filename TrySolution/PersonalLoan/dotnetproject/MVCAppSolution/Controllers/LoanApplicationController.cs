using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.Services;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService _LoanApplicationService;

        public LoanApplicationController(ILoanApplicationService LoanApplicationService)
        {
            _LoanApplicationService = LoanApplicationService;

        }

        public IActionResult AddLoanApplication()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLoanApplication(LoanApplication loanApplication)
        {
            try
            {
                if (loanApplication == null)
                {
                    return BadRequest("Invalid LoanApplication data");
                }

                var success = _LoanApplicationService.AddLoanApplication(loanApplication);

                if (success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Failed to add the LoanApplication. Please try again.");
                return View(loanApplication);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                // Return an error response or another appropriate response
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return View(loanApplication);
            }
        }

        public IActionResult Index()
        {
            try
            {
                var listLoanApplications = _LoanApplicationService.GetAllLoanApplications();
                return View("Index", listLoanApplications);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error view or another appropriate response
                return View("Error"); // Assuming you have an "Error" view
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _LoanApplicationService.DeleteLoanApplication(id);

                if (success)
                {
                    // Check if the deletion was successful and return a view or a redirect
                    return RedirectToAction("Index"); // Redirect to the list of LoanApplications, for example
                }
                else
                {
                    // Handle other error cases
                    return View("Error"); // Assuming you have an "Error" view
                }
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error view or another appropriate response
                return View("Error"); // Assuming you have an "Error" view
            }
        }
    }
}
