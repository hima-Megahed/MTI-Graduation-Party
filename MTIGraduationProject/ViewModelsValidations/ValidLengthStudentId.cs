using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.ViewModelsValidations
{
    public class ValidLengthStudentId : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dbEntities = new MTI_Graduation_PartyEntities();

            var validInt = int.TryParse(value.ToString(), out int studentId);

            return studentId.ToString().Length == 5;
        }
    }
}