using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LecturerController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Lecturer
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var lecturers = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();
           
            return View(lecturers);


        }
    }
}