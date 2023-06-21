using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientMgmtfinal.Data;
using PatientMgmtfinal.Models;
using System.Diagnostics;

namespace PatientMgmtfinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDb)
        {
            _logger = logger;
            _dbcontext = applicationDb;
        }

        public IActionResult Index()
        {
            var doctorTime = _dbcontext.DoctorTiming.ToList();
            listDoctor();
            return View("~/Views/DoctorTiming/Index.cshtml",doctorTime);
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
        private void listDoctor()
        {
            ViewBag.ListOfDoctor = _dbcontext.Doctor.ToList().Select(x => new SelectListItem
            {
                Value=x.DoctorID.ToString(),
                Text=x.Name.ToString(),
                Selected=true
            }).ToList();
        }
    }
}