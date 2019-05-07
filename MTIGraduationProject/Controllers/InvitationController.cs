﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult EditInvitation(InvitationViewModel invitationViewModel)
        {
            throw new NotImplementedException();
        }
    }
}