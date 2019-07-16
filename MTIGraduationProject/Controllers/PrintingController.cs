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

        private static int _busNumber;
        public static int NextBusNumber = 1;
        private static int _busLimitCounter;
        private const int NumberOfChairsInBus = 4;
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
        public ActionResult RegisterAttendance(int studentId)
        {
            string message = "success";
            var invitations = _mtiGraduationPartyEntities.Invitations.Where(i => i.StudentId == studentId && i.Approved);
            var student = _mtiGraduationPartyEntities.Students.FirstOrDefault(s => s.Id == studentId);

            if (student == null)
                return HttpNotFound();

            if (invitations.First().Attended == true)
            {
                message = "attendee Exist";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            foreach (var invitation in invitations)
            {
                invitation.Attended = true;
                invitation.PresenceDateTime = DateTime.Now;
            }

            student.BusId = getBusNumber();

            _mtiGraduationPartyEntities.SaveChanges();

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        private int getBusNumber()
        {
            if (_busLimitCounter < NumberOfChairsInBus)
            {
                _busLimitCounter += 2;
                _busNumber = NextBusNumber;

                if (_busLimitCounter == NumberOfChairsInBus)
                {
                    NextBusNumber++;
                    _busLimitCounter = 0;
                }
            }
            return _busNumber;
        }

        public ActionResult PrintAttendeesWithBuses()
        {
            var attendeesBusReportResults = _mtiGraduationPartyEntities.AttendeesBusReport().AsEnumerable();
            return PartialView("Partial Views/_BusesReport", attendeesBusReportResults);

        }

        /// <summary>
        /// Used to Fill in Food Outlets
        /// </summary>
        private void PopulateFoodOutlets()
        {
            var students = _mtiGraduationPartyEntities.Students.ToList();
            int Outlet1 = 1, Outlet2 = 2;

            for (int i = 0; i < students.Count; i++)
            {
                if (i < (students.Count / 2))
                {
                    students[i].BreakfastOutlet = Outlet1;
                    students[i].LunchOutlet = Outlet1;
                }
                else
                {
                    students[i].BreakfastOutlet = Outlet2;
                    students[i].LunchOutlet = Outlet2;
                }
            }
            _mtiGraduationPartyEntities.SaveChanges();
        }

        /// <summary>
        /// Used to Fill in Tables Ids
        /// </summary>
        private void PopulateTablesIds()
        {
            var students = _mtiGraduationPartyEntities.Students.ToList();
            int tableId = 1, counter = 0;
            foreach (var student in students)
            {
                student.TableId = tableId;
                counter++;
                if (counter == 2)
                {
                    counter = 0;
                    tableId++;
                }
            }

            _mtiGraduationPartyEntities.SaveChanges();
        }

        public ActionResult GetBusNumber()
        {
            return PartialView("Partial Views/_BusCounter");
        }
    }
}