using ASP12.Filters;
using ASP12.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext dbContext;
        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(dbContext.Companies.ToList());
        }
        [HttpGet]
        public IActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            dbContext.Companies.Add(company);
            dbContext.SaveChanges();
            return View("AddCompany", $"Company {company.Name} added!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

