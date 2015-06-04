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
    public class LANEditController : Controller
    {
        private RCContext db = new RCContext();

        // GET: /LANEdit/
        public ActionResult Index()
        {
            return View(db.LANController.ToList());
        }

        // GET: /LANEdit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANController lancontroller = db.LANController.Find(id);
            if (lancontroller == null)
            {
                return HttpNotFound();
            }
            return View(lancontroller);
        }

        // GET: /LANEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /LANEdit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,ControllerPath,UserManager,UserOwner,IsPublic,IsWork")] LANController lancontroller)
        {
            if (string.IsNullOrEmpty(lancontroller.Name))
                ModelState.AddModelError("Name", "Type a name");
         
            if (ModelState.IsValid)
            {
                lancontroller.UserOwner = User.Identity.Name;
             
                db.LANController.Add(lancontroller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lancontroller);
        }

        // GET: /LANEdit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANController lancontroller = db.LANController.Find(id);
            if (lancontroller == null)
            {
                return HttpNotFound();
            }
            return View(lancontroller);
        }

        // POST: /LANEdit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,ControllerPath,UserManager,UserOwner,IsPublic,IsWork")] LANController lancontroller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lancontroller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lancontroller);
        }

        // GET: /LANEdit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANController lancontroller = db.LANController.Find(id);
            if (lancontroller == null)
            {
                return HttpNotFound();
            }
            return View(lancontroller);
        }

        // POST: /LANEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LANController lancontroller = db.LANController.Find(id);
            db.LANController.Remove(lancontroller);
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
