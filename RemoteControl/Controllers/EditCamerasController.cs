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
    [Authorize]
    public class EditCamerasController : Controller
    {
        private RCContext db = new RCContext();

        // GET: /EditCameras/
         
        public ActionResult Index()
        {
            return View();
            }
        public ActionResult List(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var camera = db.Camera.Include(c => c.LANController);
            if (User.IsInRole("admin"))
                return View(camera.OrderByDescending(i => i.Id).ToPagedList(pageNumber, pageSize));



            return View(camera.Where(c => c.UserOwner == User.Identity.Name).OrderByDescending(i => i.Id).ToPagedList(pageNumber, pageSize));
       
        }

        // GET: /EditCameras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = db.Camera.Find(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            return View(camera);
        }

        // GET: /EditCameras/Create
        public ActionResult Create()
        {
            
            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name");
            return View();
        }

        // POST: /EditCameras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,CameraPath,Category,ControllerID_Id,UserManager,IsPublic,IsWork")] Camera camera)
        {
            /* проверку нужно самому сделать оказывается*/
            if (string.IsNullOrEmpty(camera.Name))
                ModelState.AddModelError("Name", "Type a name");
           /*также можно сделать атрибутами в модели*/

            if (ModelState.IsValid)
            {
                camera.UserOwner = User.Identity.Name;
                camera.State = State.NotUsed;
                camera.CameraAction = CameraAction.Connect;
                db.Camera.Add(camera);
                db.SaveChanges();
                return  RedirectToAction("Index");
            }

            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name", camera.ControllerID_Id);
            return View(camera);
        }

        // GET: /EditCameras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = db.Camera.Find(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name", camera.ControllerID_Id);
           
            return View(camera);
        }

        // POST: /EditCameras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,CameraPath,State,Category,ControllerID_Id,CameraAction,UserManager,UserOwner,IsPublic,IsWork")] Camera camera)
        {
            if (ModelState.IsValid)
            {

                //var cam = db.Camera.First(c => c.Id == camera.Id);
                //if (cam.UserManager != null)
                //    camera.IsWork = true;
                
                //    cam = null;
                    //Падает на проверке модификации, т.к. Busy но ведь налл стоит
                db.Entry(camera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name", camera.ControllerID_Id);
            return View(camera);
        }

        // GET: /EditCameras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = db.Camera.Find(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            return View(camera);
        }

        // POST: /EditCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Camera camera = db.Camera.Find(id);
            db.Camera.Remove(camera);
            db.SaveChanges();
            return RedirectToAction("Index");
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
