using RemoteControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace RemoteControl.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        int favoritesCameras = 3;
        IRepository db;
        public UserController(IRepository repo)
        {
            db = repo;
        }
        public ActionResult Camera()
        {
            var s = db.GetHashCode();
            var cameras = db.Camera.Where(c => c.UserOwner == User.Identity.Name);

            return PartialView(cameras);
        }
         public ActionResult History()
        {
            return View();
         }
   
        public ActionResult HistoryList(int? page)
        {
   
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var camera = db.UserCamera;
            if (User.IsInRole("admin"))
                return View(camera.OrderByDescending(i => i.Id).ToPagedList(pageNumber, pageSize));

            return View(camera.Where(c => c.UserName == User.Identity.Name).OrderByDescending(i => i.Id).ToPagedList(pageNumber, pageSize));
        }



        [AllowAnonymous]
        public ActionResult FavoritesCameras(Camera camera)
        {

            IEnumerable<UserCamera> uc = db.UserCamera.Where(c => c.UserName == User.Identity.Name 
                                                                    && c.CameraId != camera.Id 
                                                                    && c.CommonTimeUse !=null
                                                                    );
                                                                    
            List<Camera> cameras=new List<Camera>();
            var group= uc.GroupBy(c => c.CameraId);
            foreach (var key in group)
            {
               Camera cam = key.OrderBy(c=>c.CommonTimeUse).Select(d=>d.Camera).ToArray()[0];
               if (cam !=null)
                    cameras.Add(cam);
            }
           
            return PartialView(cameras.Take(favoritesCameras));
        }
        public ActionResult Videochat(Player player)
        {
            return PartialView(player);
        }

	}
}