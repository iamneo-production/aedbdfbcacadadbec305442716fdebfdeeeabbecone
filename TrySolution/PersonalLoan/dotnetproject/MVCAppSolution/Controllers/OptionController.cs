using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.Services;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    public class OptionController : Controller
    {
        private readonly IOptionService _OptionService;

        public OptionController(IOptionService OptionService)
        {
            _OptionService = OptionService;

        }

        public IActionResult AddOption()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOption(Option option)
        {
            try
            {
                if (option == null)
                {
                    return BadRequest("Invalid Option data");
                }

                var success = _OptionService.AddOption(option);

                if (success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Failed to add the Option. Please try again.");
                return View(option);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error response or another appropriate response
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return View(option);
            }
        }

        public IActionResult Index()
        {
            try
            {
                var listOptions = _OptionService.GetAllOptions();
                return View("Index",listOptions);
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
                var success = _OptionService.DeleteOption(id);

                if (success)
                {
                    // Check if the deletion was successful and return a view or a redirect
                    return RedirectToAction("Index"); // Redirect to the list of Options, for example
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
