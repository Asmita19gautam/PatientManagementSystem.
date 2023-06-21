using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientMgmtfinal.Data;
using PatientMgmtfinal.Models;

namespace PatientMgmtfinal.Controllers
{
    [Authorize]
    public class DoctorTimingController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly INotyfService _notyf;

        public DoctorTimingController(ApplicationDbContext dbContext, INotyfService notyf)
        {
            _dbContext = dbContext;
            _notyf = notyf;
        }
        // GET: DoctorTimingController
        public ActionResult Index()
        {
            var doctorTime = _dbContext.DoctorTiming.ToList();
            listDoctor();
            return View(doctorTime);
        }

        // GET: DoctorTimingController/Details/5
        public ActionResult Details(int id)
        {
            var details = _dbContext.DoctorTiming.Find(id);
            listDoctor();
            return View("Create",details);
        }

        // GET: DoctorTimingController/Create
        public ActionResult Create(DoctorTiming doctorTiming)
        {
            ViewData["Action"] = "Create";
            listDoctor();
            doctorTiming.DateAvailable = DateTime.Now.Date;
            return View(doctorTiming);
        }

        // POST: DoctorTimingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorTiming doctorTiming, IFormCollection collection)
        {
            try
            {
                if (doctorTiming != null && ModelState.IsValid)
                {
                    //doctorTiming.DateAvailable = Convert.ToDateTime(doctorTiming.DateAvailable.ToShortDateString());
                    _dbContext.DoctorTiming.Add(doctorTiming);
                    _dbContext.SaveChanges();
                    _notyf.Success("Successfully added doctor's avalabililty details", 5);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToArray();
                foreach (var error in errors)
                    _notyf.Error("Couldnot save the data" + error.ErrorMessage);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error("Couldnot save the data" + ex.Message);
                return View();
            }
        }

        // GET: DoctorTimingController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            var drTime = _dbContext.DoctorTiming.Find(id);
            listDoctor();
            return View("Create",drTime);
        }

        // POST: DoctorTimingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorTiming doctorTiming, IFormCollection collection)
        {
            try
            {
                if(doctorTiming != null && ModelState.IsValid)
                {
                    doctorTiming.DateAvailable = Convert.ToDateTime(doctorTiming.DateAvailable.ToShortDateString());
                    _dbContext.DoctorTiming.Update(doctorTiming);
                    _dbContext.SaveChanges();
                    _notyf.Success("Updated data successfully", 5);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToArray();
                foreach (var error in errors)
                    _notyf.Error("Couldnot save the data" + error.ErrorMessage);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error("Couldnot save the data" + ex.Message);
                return View();
            }
        }

        // GET: DoctorTimingController/Delete/5
        public ActionResult Delete(int id)
        {
            listDoctor();
            ViewData["Action"] = "Delete";
            var data = _dbContext.DoctorTiming.Find(id);
            return View("Create",data);
        }

        // POST: DoctorTimingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DoctorTiming doctorTiming, IFormCollection collection)
        {
            try
            {
                if (doctorTiming != null && ModelState.IsValid)
                {
                    _dbContext.DoctorTiming.Remove(doctorTiming);
                    _dbContext.SaveChanges();
                    _notyf.Success("Data deleted successfully", 5);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToArray();
                foreach (var error in errors)
                    _notyf.Error("Couldnot save the data" + error.ErrorMessage);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error("Couldnot save the data" + ex.Message);
                return View();
            }
        }
        private void listDoctor()
        {
            ViewBag.DoctorList = _dbContext.Doctor.ToList().Select(i => new SelectListItem
            {
                Value = i.DoctorID.ToString(),
                Text = i.Name.ToString(),
                Selected = true
            }).ToList();
        }
    }
}
