using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.Controllers
{
    public class SystemTestController : Controller
    {
        private readonly MTI_Graduation_PartyEntities _mtiGraduationPartyEntities;

        public SystemTestController()
        {
            _mtiGraduationPartyEntities = new MTI_Graduation_PartyEntities();
        }

        // GET: SystemTest
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStudent(int studentId)
        {
            string message = "success";

            if (!_mtiGraduationPartyEntities.Students.Any(s => s.Id == studentId))
            {
                message = "student doesn't exist";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            if (_mtiGraduationPartyEntities.SystemTests.Any(st => st.StudentId == studentId))
            {
                message = "student registered";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            var systemTest = new SystemTest
            {
                StudentId = studentId,
                StudentName = _mtiGraduationPartyEntities.Students.First(s => s.Id == studentId).Name,
                StudentStatus = true
            };

            _mtiGraduationPartyEntities.SystemTests.Add(systemTest);
            _mtiGraduationPartyEntities.SaveChanges();
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllStudents()
        {
            var systemTestsStudents = _mtiGraduationPartyEntities.SystemTests.AsEnumerable();
            return PartialView("Partial Views/SystemTestPrintStudents", systemTestsStudents);
        }
    }
}