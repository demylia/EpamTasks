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
    public class HistoryController : Controller
    {
        private RCContext db = new RCContext();

        // GET: /History/
        public ActionResult Index()
        {
            return View(db.UserCamera.ToList());
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

        // GET: /History/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /History/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,StartUse,EndUse,CommonTimeUse,CameraId,UserId")] UserCamera usercamera)
        {
            if (ModelState.IsValid)
            {
                db.UserCamera.Add(usercamera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usercamera);
        }

        // GET: /History/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /History/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,StartUse,EndUse,CommonTimeUse,CameraId,UserId")] UserCamera usercamera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usercamera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usercamera);
        }

        // GET: /History/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: /History/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCamera usercamera = db.UserCamera.Find(id);
            db.UserCamera.Remove(usercamera);
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
