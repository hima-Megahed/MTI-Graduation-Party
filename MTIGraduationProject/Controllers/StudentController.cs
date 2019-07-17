using System.Linq;
using System.Web.Mvc;
using MTIGraduationProject.Models;
using MTIGraduationProject.ViewModels;
using MTIGraduationProject.ViewModelsValidations;

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
                studentViewModel.Id = 0;
                return View(studentViewModel);
            }


            var student = new Student
            {
                Id = studentViewModel.Id,
                Name = studentViewModel.Name,
                TableId = studentViewModel.TableId,
                BusId = studentViewModel.BusId,
                BreakfastOutlet = studentViewModel.BreakfastOutlet,
                LunchOutlet = studentViewModel.LunchOutlet
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
            var student = _mtiGraduationPartyEntities.Students.First(s => s.Id == studentId);

            if (student == null)
                return HttpNotFound();

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                BusId = student.BusId,
                Name = student.Name,
                TableId = student.TableId,
                BreakfastOutlet = student.BreakfastOutlet,
                LunchOutlet = student.LunchOutlet
            };

            TempData["OldStudentId"] = studentId;
            ViewBag.RegisteredSuccessfully = false;
            return View("RegisterStudent", studentViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditStudent(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid && !(new StudentExist().IsValidNewStudentId(studentViewModel.Id)))
            {
                ViewBag.RegisteredSuccessfully = false;
                return View("RegisterStudent", studentViewModel);
            }

            var oldStudentId = int.Parse(TempData["OldStudentId"].ToString());
            var oldStudent = _mtiGraduationPartyEntities.Students.First(s => s.Id == oldStudentId);
            
            // The Same Student ID
            if (oldStudentId == studentViewModel.Id)
            {
                // Update Student
                oldStudent.BusId = studentViewModel.BusId;
                oldStudent.Name = studentViewModel.Name;
                oldStudent.TableId = studentViewModel.TableId;
                oldStudent.BreakfastOutlet = studentViewModel.BreakfastOutlet;
                oldStudent.LunchOutlet = studentViewModel.LunchOutlet;

            }
            // Student ID has changed
            else
            {
                // Create New Student
                var newStudent = new Student
                {
                    Id = studentViewModel.Id,
                    BusId = studentViewModel.BusId,
                    Name = studentViewModel.Name,
                    TableId = studentViewModel.TableId,
                    BreakfastOutlet = studentViewModel.BreakfastOutlet,
                    LunchOutlet = studentViewModel.LunchOutlet
                };
                // Save New Student To Database
                _mtiGraduationPartyEntities.Students.Add(newStudent);
                _mtiGraduationPartyEntities.SaveChanges();

                // Get Invitations With Old Student
                var invitations = _mtiGraduationPartyEntities.Invitations.Where(i => i.StudentId == oldStudentId).ToList();

                // Update Invitations with New Student
                foreach (var invitation in invitations)
                {
                    invitation.StudentId = newStudent.Id;
                }

                // Delete Old Student
                _mtiGraduationPartyEntities.Students.Remove(oldStudent);
            }

            // Save Changes To database
            _mtiGraduationPartyEntities.SaveChanges();

            TempData["RegisteredSuccessfully"] = true;
            return RedirectToAction("RegisterStudent", new StudentViewModel {Id = 0});
        }

    }
}