using System.ComponentModel.DataAnnotations;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.ViewModelsValidations
{
    public class ValidLengthStudentId : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var validInt = int.TryParse(value.ToString(), out int studentId);

            return studentId.ToString().Length == 5;
        }
    }
}