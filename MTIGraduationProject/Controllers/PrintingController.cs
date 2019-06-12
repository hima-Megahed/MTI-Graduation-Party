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
        public ActionResult Index(int studentId = 0)
        {
            return View(studentId);
        }

        [HttpPost]
        public ActionResult RegisterAttendance(int invitationId)
        {
            string message = "success";
            var invitation = _mtiGraduationPartyEntities.Invitations.FirstOrDefault(i => i.Id == invitationId);

            if (invitation == null)
                return HttpNotFound();

            if (invitation.Attended == true)
            {
                message = "attendee Exist";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            invitation.Attended = true;
            invitation.PresenceDateTime = DateTime.Now;
            _mtiGraduationPartyEntities.SaveChanges();

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintAttendeesWithBuses()
        {
            var attendeesBusReport_Results = _mtiGraduationPartyEntities.AttendeesBusReport().AsEnumerable();
            return PartialView("Partial Views/_BusesReport", attendeesBusReport_Results);

        }
    }
}