using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using MTIGraduationProject.Controllers;
using MTIGraduationProject.ViewModelsValidations;

namespace MTIGraduationProject.ViewModels
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, 99999, ErrorMessage = "رقم الطالب غير صحيح")]
        [ValidLengthStudentId(ErrorMessage = "رقم الطالب يجب أن يكون 5 أرقام")]
        public int Id { get; set; }

        [Required(ErrorMessage="هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int? TableId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int? BusId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int? FoodOutlet1 { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int? FoodOutlet2 { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<StudentController, ActionResult>> edit =
                    (c => c.EditStudent(this));
                Expression<Func<StudentController, ActionResult>> create =
                    (c => c.RegisterStudent(this));

                var action = (Id != 0) ? edit : create;

                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
    }
}