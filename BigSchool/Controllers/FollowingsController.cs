using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BigSchool.DTOs;
using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            {
                _dbContext.Entry(following).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.SaveChanges();
                return Json(new { isFollow = false,followeeId =  followingDto.FolloweeId});
            }
            
            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();
            return Json(new { isFollow = true, followeeId = followingDto.FolloweeId });
        }
    }
}
