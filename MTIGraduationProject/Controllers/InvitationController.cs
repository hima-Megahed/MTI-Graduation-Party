using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MTIGraduationProject.DTOs;
using MTIGraduationProject.Models;
using MTIGraduationProject.ViewModels;

namespace MTIGraduationProject.Controllers
{
    public class InvitationController : Controller
    {
        private readonly MTI_Graduation_PartyEntities _mtiGraduationPartyEntities;

        public InvitationController()
        {
            _mtiGraduationPartyEntities = new MTI_Graduation_PartyEntities();
        }

        // GET: Invitation
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

            return RedirectToAction("RegisterInvitation");
        }

        public ActionResult EditInvitation(int id)
        {
            var invitation = _mtiGraduationPartyEntities.Invitations.FirstOrDefault(i => i.Id == id);
            if (invitation == null)
                return HttpNotFound();

            ViewBag.RegisteredSuccessfully = false;
            var invitationViewModel = new InvitationViewModel
            {
                Id = id,
                StudentId = invitation.StudentId,
                Address = invitation.Address,
                NationalId = invitation.NationalId,
                BirthDate = invitation.BirthDate,
                Name = invitation.Name,
                PlaceOfBirth = invitation.PlaceOfBirth,
                Relationship = invitation.Relationship
            };

            
            return View("RegisterInvitation", invitationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInvitation(InvitationViewModel invitationViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RegisteredSuccessfully = false;
                return View("RegisterInvitation", invitationViewModel);
            }

            var invitation = _mtiGraduationPartyEntities.Invitations.First(i => i.Id == invitationViewModel.Id);

            if (invitation == null)
                return HttpNotFound();

            invitation.StudentId = invitationViewModel.StudentId;
            invitation.Name = invitationViewModel.Name;
            invitation.NationalId = invitationViewModel.NationalId;
            invitation.BirthDate = invitationViewModel.BirthDate;
            invitation.Relationship = invitationViewModel.Relationship;
            invitation.PlaceOfBirth = invitationViewModel.PlaceOfBirth;
            invitation.Address = invitationViewModel.Address;

            _mtiGraduationPartyEntities.SaveChanges();

            TempData["RegisteredSuccessfully"] = true;
            return RedirectToAction("RegisterInvitation", new InvitationViewModel{Id = 0});
        }

        [HttpPost]
        public ActionResult GetInvitationList(int studentId)
        {
            var student = _mtiGraduationPartyEntities.Students.FirstOrDefault(s => s.Id == studentId);
            var invitationList = _mtiGraduationPartyEntities.Invitations.Where(i => i.StudentId == studentId).ToList();

            var invitationDto = new InvitationDto
            {
                Invitations = invitationList,
                InvitationsExist = (invitationList.Count != 0),
                Student = student
            };

            return PartialView("Partial Views/_InvitationList", invitationDto);
        }

        [HttpPost]
        public ActionResult DeleteInvitation(int id)
        {
            var invitation = _mtiGraduationPartyEntities.Invitations.First(i => i.Id == id);
            int studentId = invitation.StudentId.GetValueOrDefault();

            _mtiGraduationPartyEntities.Invitations.Remove(invitation);
            _mtiGraduationPartyEntities.SaveChanges();
            

            return Json(new { message = "success", studentId = studentId.ToString() }, JsonRequestBehavior.AllowGet);
        }

        
    }
}