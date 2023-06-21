using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientMgmtfinal.Data;
using PatientMgmtfinal.Models;

namespace PatientMgmtfinal.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly INotyfService _notyf;
        public PatientController(ApplicationDbContext dbContext, INotyfService notyf)
        {
            _dbContext = dbContext;
            _notyf = notyf;
        }

        // GET: PatientController
        public ActionResult Index()
        {
            var patientList = _dbContext.Patient.ToList();
            return View(patientList);
        }

        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            var details = _dbContext.Patient.Find(id);
            return View("Create", details);
        }

        // GET: PatientController/Create
        public ActionResult Create(Patient patient)
        {
            ViewData["Action"] = "Create";
            return View(patient);
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid && patient!=null)
                {
                    _dbContext.Add(patient);
                    _dbContext.SaveChanges();
                    _notyf.Success("Patient added successfully", 5);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToArray();
                foreach (var error in errors)
                    _notyf.Error("Couldnot save the data" + error.ErrorMessage);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _notyf.Error("Couldnot save the data" + ex.Message);
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            var patient = _dbContext.Patient.Find(id);
            return View("Create",patient);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient, IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _dbContext.Patient.Update(patient);
                    _dbContext.SaveChanges();
                    _notyf.Success("Patient updated successfully", 5);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToArray();
                foreach (var error in errors)
                    _notyf.Error("Couldnot save the data" + error.ErrorMessage);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                _notyf.Error("Couldnot upodate the data" + e.Message);
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewData["Action"] = "Delete";
            var dataTODlt = _dbContext.Patient.Find(id);
            return View("Create",dataTODlt);
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Patient patient, IFormCollection collection)
        {
            try
            {
                _dbContext.Patient.Remove(patient);
                _dbContext.SaveChanges();
                _notyf.Success("Patient deleted successfully", 5);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }
    }
}
