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
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly INotyfService _notyf;
        public DoctorController(ApplicationDbContext dbContext, INotyfService notyf)
        {
            _dbContext = dbContext;
            _notyf = notyf;
        }
        // GET: DoctorController
        public ActionResult Index()
        {
            var doctorList = _dbContext.Doctor.ToList();
            listDepartment();
            return View(doctorList);
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            listDepartment();
            var docDetails = _dbContext.Doctor.Find(id);
            return View("Create",docDetails);
        }

        // GET: DoctorController/Create
        public ActionResult Create(Doctor doctor)
        {
            ViewData["Action"] = "Create";
            listDepartment();
            return View(doctor);
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctor doctor, IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid && doctor != null)
                {
                    _dbContext.Doctor.Add(doctor);
                    _dbContext.SaveChanges();
                    _notyf.Success("Doctor added successfully", 5);
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

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            listDepartment();
            ViewData["Action"] = "Edit";
            var doctorToEdit = _dbContext.Doctor.Find(id);
            return View("Create",doctorToEdit);
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Doctor doctor, IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid && doctor != null)
                {
                    _dbContext.Doctor.Update(doctor);
                    _dbContext.SaveChanges();
                    _notyf.Success("Doctor added successfully", 5);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToArray();
                foreach (var error in errors)
                    _notyf.Error("Couldnot save the data" + error.ErrorMessage);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error("Couldnot edit the data" + ex.Message);
                return View("Create");
            }
        }

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            listDepartment();
            ViewData["Action"] = "Delete";
            var doctorToDelete = _dbContext.Doctor.Find(id);
            return View("Create",doctorToDelete);
        }

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Doctor doctor, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid && doctor != null)
                {
                    _dbContext.Doctor.Remove(doctor);
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
                return View("Create");
            }
        }
        private void listDepartment()
        {
            ViewBag.DepartmentList = _dbContext.Department.ToList().Select(i => new SelectListItem
            {
                Value = i.DepID.ToString(),
                Text = i.Name.ToString(),
                Selected = true
            }).ToList();
        }
    }
}
