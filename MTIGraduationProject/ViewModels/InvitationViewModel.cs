using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using MTIGraduationProject.Controllers;
using MTIGraduationProject.ViewModelsValidations;

namespace MTIGraduationProject.ViewModels
{
    public class InvitationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [ValidLengthStudentId(ErrorMessage = "رقم الطالب يجب أن يكون 5 أرقام")]
        [StudentExist(ErrorMessage ="هذا الطالب غير موجود")]
        public int? StudentId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Relationship { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MaxLength(14, ErrorMessage = "الرقم القومي يجب ألا يزيد عن 14 رقم")]
        [MinLength(14, ErrorMessage = "الرقم القومي يجب ألا يقل عن 14 رقم")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Address { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<InvitationController, ActionResult>> edit =
                    (c => c.EditInvitation(this));
                Expression<Func<InvitationController, ActionResult>> create =
                    (c => c.RegisterInvitation(this));

                var action = (Id != 0) ? edit : create;

                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
    }
}