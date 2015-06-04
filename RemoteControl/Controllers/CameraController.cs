using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteControl.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RemoteControl.Controllers
{
   // [Authorize]
    public class CameraController : Controller
    {
       RCContext context = new RCContext();
        
        //
        // GET: /Camera/
        public ActionResult Index()
        {
            IEnumerable<Camera> cameras = context.Camera.Where(c => c.IsWork == true);
            
            if (!User.IsInRole("admin"))
                cameras =  context.Camera.Where(c => c.IsAvailable == true || c.UserOwner == User.Identity.Name);
            
            return View(cameras);
        }
        public ActionResult Detail(Camera camera)
        {
            
            return View(camera);
        }
        public ActionResult Category(Category? cat)
        {
            IEnumerable<Camera> cameras = context.Camera;
            if (cat != null)
                cameras = context.Camera.Where(c => c.Category == cat);
            return PartialView(cameras);
        }
        
        
        public ActionResult Player(Camera camera)
        {

           CameraState(camera);

           return PartialView("_JWPlayer", camera);
        }
        private void CameraState(Camera camera)
        {
            if (camera.State == State.NotUsed)
                ViewBag.CameraState = "Свободна";

            else if (camera.State == State.Used && camera.UserManager == User.Identity.Name)
                ViewBag.CameraState = "Вы используете";
            else
                ViewBag.CameraState = "Занята";

        }
       // [HttpPost]
       // [Authorize]
        public ActionResult Connect(Camera camera)
        {
            if (camera.ControllerID_Id == null)
                return null;

            if (camera.State == State.Used && camera.UserManager == User.Identity.Name)
            {
              return PartialView("_Connect", camera);
                
            }
            if (camera.State == State.Used)
            {
              return  PartialView("_Queue",camera);
            }
            if (camera.State == State.NotUsed)
            {

                var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);
                cam.UserManager = User.Identity.Name;
                cam.State = State.Used;
                cam.CameraAction = CameraAction.Disconnect;
                context.SaveChanges();

                StartUsingCamera(cam);

                return PartialView("_Connect",cam);
            }
          
                return null;
         
        }
        public ActionResult Click(Camera camera, int out_id)
        {
            if (camera.UserManager == User.Identity.Name)
                 return Redirect("http://82.209.194.198/oc?" + out_id);// Подумать чтобы не было перехода по клику
            return null;
        }
        private bool StartUsingCamera (Camera camera)
        {
           
                UserCamera uc = new UserCamera();
                uc.CameraId = camera.Id;
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                uc.UserId = userManager.Users.First(u => u.UserName == User.Identity.Name).Id;
                uc.StartUse = DateTime.Now;

                context.UserCamera.Add(uc);
               
                context.SaveChanges();
                
                return true;
            
            /*catch
            {
                return false;
            }*/
        }

        private bool EndUsingCamera(Camera camera)
        {
           
                var uc = context.UserCamera.First(c => c.CameraId == camera.Id && c.CommonTimeUse ==null);
                uc.EndUse = DateTime.Now;
               uc.CommonTimeUse = uc.EndUse.Value -  uc.StartUse.Value;
                context.SaveChanges();
                return true;
           
        }
        
        public ActionResult Disconnect(Camera camera)
        {
            
            if (camera.UserManager != User.Identity.Name)
                return null;

            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);

            EndUsingCamera(camera);

            cam.UserManager = null;
            cam.State = State.NotUsed;
            cam.CameraAction = CameraAction.Disconnect;
            context.SaveChanges();

           

            return PartialView("_Disconnect",cam);
        }
       
        public ActionResult QuitQueue(Camera camera)
        {
            /*Подумать как сделать очередь*/
            return PartialView("_Queue", camera);
        }
       
        public ActionResult TakeQueue(Camera camera)
        {
            /*Подумать как сделать очередь
            Stack<Camera> queue = new Stack<Camera>();
            queue.Push(camera);
            */
            return PartialView("_QuitQueue", camera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}