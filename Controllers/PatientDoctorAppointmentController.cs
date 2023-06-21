using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientMgmtfinal.Data;
using PatientMgmtfinal.Models;
using System.Data.Entity.Core.Objects;
using System.Globalization;

namespace PatientMgmtfinal.Controllers
{
    [Authorize]
    public class PatientDoctorAppointmentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly INotyfService _notyf;

        public PatientDoctorAppointmentController(ApplicationDbContext dbContext, INotyfService notyfService)
        {
            _dbContext = dbContext;
            _notyf = notyfService;
        }
        // GET: PatientDoctorAppointmentController
        public ActionResult Index()
        {
            var data = _dbContext.PatientDoctorAppointment.ToList();
            patientList();
            doctorList();
            doctortimingList();
            return View(data);
        }

        // GET: PatientDoctorAppointmentController/Details/5
        public ActionResult Details(int id)
        {
            var data = _dbContext.PatientDoctorAppointment.Find(id);
            patientList();
            doctorList();
            doctortimingList();
            listDepartment();
            return View("Create",data);
        }

        // GET: PatientDoctorAppointmentController/Create
        public ActionResult Create(PatientDoctorAppointment patientDoctorAppointment)
        {
            ViewData["Action"] = "Create";
            //DateTime dt = DateTime.ParseExact(DateTime.Now.ToString(), "M/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            patientDoctorAppointment.ApppointmentTime = DateTime.Now.Date;
            patientList();
            doctorList();
            doctortimingList();
            listDepartment();
            return View(patientDoctorAppointment);
        }

        // POST: PatientDoctorAppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientDoctorAppointment pdA,IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid && pdA != null)
                {
                    _dbContext.PatientDoctorAppointment.Add(pdA);
                    _dbContext.SaveChanges();
                    _notyf.Success("Successfully scheduled appointment", 5);
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

        // GET: PatientDoctorAppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            patientList();
            doctorList();
            doctortimingList();
            listDepartment();
            var data = _dbContext.PatientDoctorAppointment.Find(id);
            return View("Create",data);
        }

        // POST: PatientDoctorAppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientDoctorAppointment pdA, IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid && pdA != null)
                {
                    _dbContext.PatientDoctorAppointment.Update(pdA);
                    _dbContext.SaveChanges();
                    _notyf.Success("Data updated successfully", 5);
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

        // GET: PatientDoctorAppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewData["Action"] = "Delete";
            patientList();
            doctorList();
            doctortimingList();
            listDepartment();
            var data = _dbContext.PatientDoctorAppointment.Find(id);
            return View("Create",data);
        }

        // POST: PatientDoctorAppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PatientDoctorAppointment pdA, IFormCollection collection)
        {
            try
            {
                if(ModelState.IsValid && pdA != null)
                {
                    _dbContext.PatientDoctorAppointment.Remove(pdA);
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
        public List<Department> getdepList(string date)
        {
            var drtime = _dbContext.DoctorTiming.Where(x => x.DateAvailable.Date == Convert.ToDateTime(date) /*&& x.AvailableTime >= Convert.ToDateTime(time).TimeOfDay*/).Select(x => /*new { x.DoctorID, x.AvailableTime}*/x.DoctorID).ToList();
            var doc = _dbContext.Doctor.Where(x => drtime.Contains(x.DoctorID)).Select(x=>x.DepID).ToList();
            var dep = _dbContext.Department.Where(x=> doc.Contains(x.DepID)).ToList();
            return dep;
        } 
        public List<DoctorTiming> getdrtimeList(string date, string docID)
        {
            var drtime = _dbContext.DoctorTiming.Where(x => x.DateAvailable == Convert.ToDateTime(date) && x.DoctorID.ToString()==docID).ToList();
            //var doc = _dbContext.Doctor.Where(x => drtime.Contains(x.DoctorID)).Select(x=>x.DepID).ToList();
            //var dep = _dbContext.Department.Where(x=> doc.Contains(x.DepID)).ToList();
            return drtime;
        } 
        public List<Doctor> getdocList(string id)
        {
            var doc = _dbContext.Doctor.Where(x =>x.DepID.ToString()==id).ToList();
            return doc;
        }
        private void patientList()
        {
            ViewBag.PatientList = _dbContext.Patient.ToList().Select(i=> new SelectListItem
            {
                Value = i.PatientID.ToString(),
                Text = i.Name.ToString(),
                Selected = true
            });
        }
        private void doctorList()
        {
            ViewBag.DoctorList = _dbContext.Doctor.ToList().Select(i=> new SelectListItem
            {
                Value = i.DoctorID.ToString(),
                Text = i.Name.ToString(),
                Selected = true
            });
        }
        private void doctortimingList()
        {
            ViewBag.DoctorTimingList = _dbContext.DoctorTiming.ToList().Select(i=> new SelectListItem
            {
                Value = i.DrTimeID.ToString(),
                Text = i.AvailableTime.ToString(),
                Selected = true
            });
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
