using System;
using System.Linq;
using System.Web.Mvc;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.Controllers
{
    public class PrintingController : Controller
    {
        private readonly MTI_Graduation_PartyEntities _mtiGraduationPartyEntities;

        public static int NextBusNumber = 1;
        private static int _busNumber;
        private static int _busLimitCounter;
        private const int NumberOfChairsInBus = 40;

        public static int NextTableNumber = 1;
        private static int _tableNumber;
        private static int _tableLimitCounter;
        private const int NumberOfChairsInTable = 4;

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
            var message = "success";
            var invitations = _mtiGraduationPartyEntities.Invitations.Where(i => i.StudentId == studentId && i.Approved);
            var student = _mtiGraduationPartyEntities.Students.First(s => s.Id == studentId);

            // Checking if invitations already registered
            if (invitations.First().Attended == true)
            {
                message = "attendee Exist";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            // // setting Bus Id property
            if (student.BusId == null)
            {
                student.BusId = getBusNumber();
            }

            // setting Table Id property
            if (student.TableId == null)
            {
                student.TableId = getTableNumber();
            }

            // setting invitation attending and presence time
            foreach (var invitation in invitations)
            {
                invitation.Attended = true;
                invitation.PresenceDateTime = DateTime.Now;
            }

            _mtiGraduationPartyEntities.SaveChanges();

            if (student.BusId == null || student.TableId == null)
            {
                message = "fail";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        private int getTableNumber()
        {
            if (_tableLimitCounter < NumberOfChairsInTable)
            {
                _tableLimitCounter += 2;
                _tableNumber = NextTableNumber;

                if (_tableLimitCounter == NumberOfChairsInTable)
                {
                    NextTableNumber++;
                    _tableLimitCounter = 0;
                }
            }
            return _tableNumber;
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
        private void FillInFoodOutlets()
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
        private void ClearingTablesAndBusesIds()
        {
            var students = _mtiGraduationPartyEntities.Students.ToList();
            int tableId = 1, counter = 0;
            foreach (var student in students)
            {
                student.TableId = null; //tableId;
                student.BusId = null; //tableId;
                counter++;
                if (counter == 2)
                {
                    counter = 0;
                    tableId++;
                }
            }

            _mtiGraduationPartyEntities.SaveChanges();
        }

        /// <summary>
        /// Used to Fill in Tables Ids
        /// </summary>
        private void ClearingAttendedAndPresenceTime()
        {
            var invitations = _mtiGraduationPartyEntities.Invitations.ToList();
            
            foreach (var invitation in invitations)
            {
                invitation.Attended = false;
                invitation.PresenceDateTime = null;
            }

            _mtiGraduationPartyEntities.SaveChanges();
        }

        public ActionResult GetBusAndTableNumbers()
        {
            return PartialView("Partial Views/_BusAndTableCounter");
        }
    }
}