using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RemoteControl.Models;
using PagedList;

namespace RemoteControl.Controllers
{
    public class HistoryController : Controller
    {
        private RCContext db = new RCContext();

        // GET: /History/

        public ActionResult Index()
        {
       
            return View();
        }
        public ActionResult List(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var camera = db.UserCamera.Include(c => c.Camera);
            if (User.IsInRole("admin"))
                return View(camera.OrderBy(i => i.Id).ToPagedList(pageNumber, pageSize));

            return View(camera.Where(c => c.UserName == User.Identity.Name).OrderBy(i => i.Id).ToPagedList(pageNumber, pageSize));
        }

        // GET: /History/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCamera usercamera = db.UserCamera.Find(id);
            if (usercamera == null)
            {
                return HttpNotFound();
            }
            return View(usercamera);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
