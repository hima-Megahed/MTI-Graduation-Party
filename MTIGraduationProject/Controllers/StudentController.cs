using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTIGraduationProject.Models;
using MTIGraduationProject.ViewModels;

namespace MTIGraduationProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly MTI_Graduation_PartyEntities _mtiGraduationPartyEntities;

        public StudentController()
        {
            _mtiGraduationPartyEntities = new MTI_Graduation_PartyEntities();
        }
        // GET: Student
        public ActionResult RegisterStudent()
        {
            ViewBag.RegisteredSuccessfully = false;

            if (TempData.ContainsKey("RegisteredSuccessfully"))
            {
                ViewBag.RegisteredSuccessfully = true;
                TempData.Remove("RegisteredSuccessfully");
            }


            var studentViewModel = new StudentViewModel
            {
                Id = 0
            };

            return View(studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStudent(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RegisteredSuccessfully = false;
                return View(studentViewModel);
            }


            var student = new Student
            {
                Id = studentViewModel.Id,
                Name = studentViewModel.Name,
                Specialization = studentViewModel.Specialization,
                TableId = studentViewModel.TableId,
                BusId = studentViewModel.BusId,
                foodOutlet1 = studentViewModel.FoodOutlet1,
                foodOutlet2 = studentViewModel.FoodOutlet2
            };

            _mtiGraduationPartyEntities.Students.Add(student);
            _mtiGraduationPartyEntities.SaveChanges();

            TempData["RegisteredSuccessfully"] = true;

            return RedirectToAction("RegisterStudent");
        }

        [HttpPost]
        public JsonResult GetStudentName(int studentId)
        {
            var studentName = _mtiGraduationPartyEntities.Students.FirstOrDefault(s => s.Id == studentId)?.Name; 

            return Json(studentName, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditStudent(StudentViewModel studentViewModel)
        {
            throw new NotImplementedException();
        }

    }
}