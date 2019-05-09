﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MTIGraduationProject.Models;

namespace MTIGraduationProject.DTOs
{
    public class InvitationDto
    {
        public List<Invitation> Invitations { get; set; }
        public bool InvitationsExist { get; set; }
        public Student Student { get; set; }
    }
}