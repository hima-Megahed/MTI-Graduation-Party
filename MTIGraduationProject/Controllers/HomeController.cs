using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTIGraduationProject.Models;
using MTIGraduationProject.ViewModels;

namespace MTIGraduationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly MTI_GraduationPartyEntities _mtiGraduationPartyEntities;

        public HomeController()
        {
            _mtiGraduationPartyEntities = new MTI_GraduationPartyEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult RegisterStudent()
        {
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
                return View(studentViewModel);

            var student = new Student
            {
                Id = studentViewModel.Id,
                Name = studentViewModel.Name,
                Specialization = studentViewModel.Specialization,
                TableId = studentViewModel.TableId,
                BusId = studentViewModel.BusId,
                Chair1Id = studentViewModel.Chair1Id,
                Chair2Id = studentViewModel.Chair2Id
            };

            
            _mtiGraduationPartyEntities.Students.Add(student);
            _mtiGraduationPartyEntities.SaveChanges();

            return RedirectToAction("RegisterStudent", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegisterInvitation()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditStudent(StudentViewModel studentViewModel)
        {
            throw new NotImplementedException();
        }
    }
}