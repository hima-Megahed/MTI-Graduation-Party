﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.ViewModelsValidations
{
    public class StudentExist : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dbEntities = new MTI_Graduation_PartyEntities();

            var validInt = int.TryParse(value.ToString(), out int studentId);

            return (validInt && dbEntities.Students.Any(s => s.Id == studentId));
        }

        public bool IsValidNewStudentId(int id)
        {
            return (id < 99999 && id > 0 && id.ToString().Length == 5);
        }
    }
}