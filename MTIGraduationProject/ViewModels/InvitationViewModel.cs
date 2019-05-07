using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MTIGraduationProject.Controllers;
using MTIGraduationProject.ViewModelsValidations;

namespace MTIGraduationProject.ViewModels
{
    public class InvitationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [ValidStudentId(ErrorMessage ="هذا الطالب غير موجود")]
        public int? StudentId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Relationship { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Address { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<HomeController, ActionResult>> edit =
                    (c => c.EditInvitation(this));
                Expression<Func<HomeController, ActionResult>> create =
                    (c => c.RegisterInvitation(this));

                var action = (Id != 0) ? edit : create;

                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
    }
}