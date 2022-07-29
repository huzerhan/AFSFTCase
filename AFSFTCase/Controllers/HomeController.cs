using AFSFTCase.Data;
using AFSFTCase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AFSFTCase.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public AFSFTDbContext _db;
        public HomeController(AFSFTDbContext db)
        {
            _db = db;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}