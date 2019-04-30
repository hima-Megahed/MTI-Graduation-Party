using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using MTIGraduationProject.Controllers;

namespace MTIGraduationProject.ViewModels
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Range(1, 99999, ErrorMessage = "رقم الطالب غير صحيح")]
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
        public int? Chair1Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int? Chair2Id { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<HomeController, ActionResult>> edit =
                    (c => c.EditStudent(this));
                Expression<Func<HomeController, ActionResult>> create =
                    (c => c.RegisterStudent(this));

                var action = (Id != 0) ? edit : create;

                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
    }
}