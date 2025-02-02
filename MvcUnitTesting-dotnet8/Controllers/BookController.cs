using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8.Controllers
{
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository<Book> repository;

        public BookController(IRepository<Book> bookRepo, ILogger<HomeController> logger)
        {
            repository = bookRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return View(new List<Book>()); // Return empty list if no genre is provided
            }

            var genreBooks = await Task.Run(() => repository.Find(b => b.Genre == genre)); // Ensure async execution
            ViewData["Genre"] = genre;
            return View(genreBooks);
        }

    }
}
