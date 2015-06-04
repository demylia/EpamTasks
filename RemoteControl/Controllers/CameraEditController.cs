using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RemoteControl.Models;

namespace RemoteControl.Controllers
{
    [Authorize(Roles="admin")]
    public class CameraEditController : Controller
    {
        private RCContext db = new RCContext();

        // GET: /CameraEdit/
        public ActionResult Index()
        {
            var camera = db.Camera.Include(c => c.LANController);
            return View(camera.ToList());
        }

        // GET: /CameraEdit/Details/5
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

        // GET: /CameraEdit/Create
        public ActionResult Create()
        {
            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name");
            return View();
        }

        // POST: /CameraEdit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,CameraPath,Category,ControllerID_Id")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                camera.State = State.NotUsed;
                camera.CameraAction = CameraAction.Connect;
                db.Camera.Add(camera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name", camera.ControllerID_Id);
            return View(camera);
        }

        // GET: /CameraEdit/Edit/5
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

        // POST: /CameraEdit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,CameraPath,State,Category,ControllerID_Id,CameraAction")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(camera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ControllerID_Id = new SelectList(db.LANController, "Id", "Name", camera.ControllerID_Id);
            return View(camera);
        }

        // GET: /CameraEdit/Delete/5
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

        // POST: /CameraEdit/Delete/5
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
