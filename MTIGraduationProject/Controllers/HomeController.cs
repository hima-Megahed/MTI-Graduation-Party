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
        private readonly MTI_Graduation_PartyEntities _mtiGraduationPartyEntities;

        public HomeController()
        {
            _mtiGraduationPartyEntities = new MTI_Graduation_PartyEntities();
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

        [HttpGet]
        public ActionResult RegisterInvitation()
        {
            ViewBag.RegisteredSuccessfully = false;

            if (TempData.ContainsKey("RegisteredSuccessfully"))
            {
                ViewBag.RegisteredSuccessfully = true;
                TempData.Remove("RegisteredSuccessfully");
            }


            var invitationViewModel = new InvitationViewModel()
            {
                Id = 0
            };

            return View(invitationViewModel);
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

            return RedirectToAction("RegisterStudent", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegisterInvitation(InvitationViewModel invitationViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RegisteredSuccessfully = false;
                return View(invitationViewModel);
            }
                

            var invitation = new Invitation
            {
                StudentId = invitationViewModel.StudentId,
                Name = invitationViewModel.Name,
                Address = invitationViewModel.Address,
                NationalId = invitationViewModel.NationalId,
                BirthDate = invitationViewModel.BirthDate,
                PlaceOfBirth = invitationViewModel.PlaceOfBirth,
                Relationship = invitationViewModel.Relationship
            };

            _mtiGraduationPartyEntities.Invitations.Add(invitation);
            _mtiGraduationPartyEntities.SaveChanges();

            TempData["RegisteredSuccessfully"] = true;

            return RedirectToAction("RegisterInvitation", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditStudent(StudentViewModel studentViewModel)
        {
            throw new NotImplementedException();
        }

        public ActionResult EditInvitation(InvitationViewModel invitationViewModel)
        {
            throw new NotImplementedException();
        }
    }
}