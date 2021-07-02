using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Create()
        {
            //get list category
            BigSchoolConnect con = new BigSchoolConnect();
            Course objCourse = new Course();
            objCourse.ListCategory = con.Categories.ToList();
            return View(objCourse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objcourse)
        {
            BigSchoolConnect con = new BigSchoolConnect();

            ModelState.Remove("LecturerID");
            if (!ModelState.IsValid)
            {
                objcourse.ListCategory = con.Categories.ToList();
                return View("Create", objcourse);
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objcourse.LecturerID = user.Id;

            con.Courses.Add(objcourse);
            con.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}