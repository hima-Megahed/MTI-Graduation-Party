using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTIGraduationProject.Models;
using MTIGraduationProject.ViewModels;

namespace MTIGraduationProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult RegisterAttendance()
        {
            throw new NotImplementedException();
        }
    }
}