using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RemoteControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RemoteControl.Controllers
{
    public class MainController : Controller
    {
        IRepository context;
        public static Mutex mtx = new Mutex();
       
        public MainController(IRepository r)
        {
            context = r;
            
        }
        // GET: /Main/
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Detail(Camera camera)
        {
            return View(camera);
        }
        public ActionResult Category(Category? cat, IEnumerable<Camera> cameras)
        {
            if (cameras == null)
                cameras = context.Camera;

            if (!User.IsInRole("admin"))
                cameras = cameras.Where(c => c.IsPublic == true || c.UserOwner == User.Identity.Name);

            if (cat != null)
                cameras = cameras.Where(c => c.Category == cat);
            
            return PartialView(cameras);
        }
        public ActionResult GetPlayer(Camera camera)
        {
            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);
            if (cam == null)
                return View("Index");
            //the order of checking has meaning
            if (!Request.IsAuthenticated)
                return PartialView("_LoginConnect",cam);

            if (cam.ControllerID_Id == null)
                return PartialView("_Player", new Player { camera = camera, width = 700, height = 500 });

            if (cam.State == State.Used && cam.UserManager == User.Identity.Name)
                 return PartialView("_Connect", cam);

            if (cam.State == State.NotUsed && ( cam.Queue.Count == 0 || cam.Queue.ToArray()[0].UserName == User.Identity.Name))
                 return PartialView("_Disconnect", cam);

            if (cam.Queue.FirstOrDefault(q=>q.UserName == User.Identity.Name) != null)
                 return PartialView("_QuitFromQueue", cam);
         
           if (cam.State == State.Used)
                return PartialView("_TakeToQueue", cam);

            if (cam.State == State.NotUsed &&  cam.Queue.Count > 0)
                return PartialView("_TakeToQueue", cam);
           
            return null;
        }

       [System.Web.Mvc.AuthorizeAttribute]
        public ActionResult Connect(Camera camera)
        {

            mtx.WaitOne();
            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);
            
            // if you are already connected to the same user, but left the old tab is not updated 
            if (cam.UserManager == User.Identity.Name)
                return PartialView("_Connect", cam);

            //blocking off other people's user
            if (cam.UserManager != null && cam.UserManager != User.Identity.Name)
                return RedirectToAction("TakeToQueue", cam);
            // connected
            if (cam.State == State.NotUsed && (cam.Queue.Count == 0 || cam.Queue.ToArray()[0].UserName == User.Identity.Name))
            { 
               
                context.ConnectToCamera(cam, User);
               
                //SendMessage(cam, "Busy", "red");
                return PartialView("_Connect", cam);
            }
            //if after SignalR has changed state on TaketoQueue
            if (cam.State == State.Used)
                return RedirectToAction("TakeToQueue", cam);
            if (cam.State == State.NotUsed && cam.Queue.Count > 0)
                return RedirectToAction("TakeToQueue", cam);
         mtx.ReleaseMutex();
            return null; 
          
        }

       [System.Web.Mvc.AuthorizeAttribute]
        public ActionResult Disconnect(Camera camera)
        {

            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);

            //blocking off other people's user
            if (cam.UserManager != null && cam.UserManager != User.Identity.Name)
                return PartialView("_TakeToQueue", cam);

            // if you are already disconnected to the same user, but left the old tab is not updated 
            if (cam.State == State.NotUsed)
                return PartialView("_Disconnect", cam);

            //disconnected
            mtx.WaitOne();
            context.DisconnectFromCamera(cam,User);
            mtx.ReleaseMutex();
            if (cam.Queue.Count > 0)
                return PartialView("_TakeToQueue", cam);

//            SendMessage(cam, "Free", "#37CB39");
            return PartialView("_Disconnect", cam);
        }
        [System.Web.Mvc.AuthorizeAttribute]
        public ActionResult TakeToQueue(Camera camera)
        {
           
            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);
            //if after SignalR has changed state on  Connect - your queue
            if (cam.State == State.NotUsed && (cam.Queue.Count == 0 || cam.Queue.ToArray()[0].UserName == User.Identity.Name))
                return RedirectToAction("Connect", cam);
           
            if (cam.Queue.FirstOrDefault(q => q.UserName == User.Identity.Name) != null)
                return RedirectToAction("QuitFromQueue",cam);
            mtx.WaitOne();
            context.TakeToQueue(cam, User);
            mtx.ReleaseMutex();
           // SendMessageQueue(cam);
            
            return PartialView("_QuitFromQueue", cam);
        }

       [System.Web.Mvc.AuthorizeAttribute]
        public ActionResult QuitFromQueue(Camera camera)
        {
            
            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);
            //if after SignalR has changed state on  Connect
            if (cam.State == State.NotUsed && (cam.Queue.Count == 0 || cam.Queue.ToArray()[0].UserName == User.Identity.Name))
                return RedirectToAction("Connect", cam);
            mtx.WaitOne();
            context.QuitFromQueue(cam, User);
            mtx.ReleaseMutex();
           // SendMessageQueue(cam);
         
            return PartialView("_TakeToQueue", cam);
        }
       
       //blocking off player's controls 
        public ActionResult BlockScreen(Camera camera)
        {
            var cam = context.Camera.FirstOrDefault(c => c.Id == camera.Id);
            CheckCameraState(cam);
            CheckQueueState(cam);

            return PartialView(cam);
        }
        
        public HttpStatusCodeResult Click(int out_id)
        {
           
       
          HttpWebRequest myReq =(HttpWebRequest)WebRequest.Create("http://192.168.100.105/oc?"+ out_id);
          try
          {
                 var req = myReq.GetResponse();
                 return new HttpStatusCodeResult(HttpStatusCode.OK);
          }
          catch
          {
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
          }

        }
      
        private void CheckCameraState(Camera camera)
        {
            ViewBag.StateColor = "badge badge-success";
          
             //check state & user  
            if (camera.State == State.NotUsed
                && (camera.Queue.Count == 0 || camera.Queue.ToArray()[0].UserName == User.Identity.Name))
                ViewBag.CameraState = "Free";
          

            else if (camera.State == State.Used && camera.UserManager == User.Identity.Name)
                  ViewBag.CameraState = "You are using";
            
            else
            {
                ViewBag.CameraState = "Busy";
                ViewBag.StateColor = "badge badge-danger";
            }
               
          
        }
        private void CheckQueueState(Camera camera)
        {
            if (camera.Queue.Count == 0)
                return;

            if (camera.Queue.FirstOrDefault(q => q.UserName == User.Identity.Name) != null)
            {
                var arrayQueueUserName = camera.Queue.Select(q => q.UserName).ToArray();
                var index = Array.IndexOf(arrayQueueUserName, User.Identity.Name);
                var text = (camera.Queue.Count > 1) ? " from " + camera.Queue.Count : "";
                ViewBag.QueueState = "You are "  + ++index + text + " in the queue";
                ViewBag.QueueColor = "badge-danger";
            }
            else
            {
                ViewBag.QueueState = camera.Queue.Count > 0 ? "In the queue - " + camera.Queue.Count : "";
                ViewBag.QueueColor = "badge-danger";
            }
            
        }
        private void SendMessageState(Camera cam, string message, string color)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hubContext.Clients.All.changeState("span#" + cam.Id.ToString(), message, color);

        }
        private void SendMessageQueue(Camera cam)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hubContext.Clients.All.changeQueue("span#q" + cam.Id.ToString(), (cam.Queue.Count != 0) ? "In the queue- " + cam.Queue.Count : "", "blue");

        }
         
       
	}
}