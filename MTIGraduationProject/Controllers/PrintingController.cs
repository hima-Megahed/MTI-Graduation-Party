using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MTIGraduationProject.DTOs;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.Controllers
{
    public class PrintingController : Controller
    {
        private readonly MTI_Graduation_PartyEntities _mtiGraduationPartyEntities;

        public PrintingController()
        {
            _mtiGraduationPartyEntities = new MTI_Graduation_PartyEntities();
        }
        // GET: Printing
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAttendance(int invitationId)
        {
            string message = "success";
            var invitation = _mtiGraduationPartyEntities.Invitations.Any(i => i.Id == invitationId);

            if (!invitation)
                return HttpNotFound();

            var attendanceRecord = new Attendee
            {
                InvitationId = invitationId
            };

            var attendanceExist = _mtiGraduationPartyEntities.Attendees.Any(a => a.InvitationId == invitationId);

            if (attendanceExist)
                message = "attendee Exist";
            else
            {
                _mtiGraduationPartyEntities.Attendees.Add(attendanceRecord);
                _mtiGraduationPartyEntities.SaveChanges();
            }
            
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}