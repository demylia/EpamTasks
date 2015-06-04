using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace RemoteControl.Models
{
    public class CameraRepository: IRepository, IDisposable
    {
        private RCContext context = new RCContext();

        public IEnumerable<UserCamera> UserCamera { get { return context.UserCamera; } }
        public IEnumerable<Camera> Camera { get { return context.Camera.Where(c => c.IsWork == true); }}
        public IEnumerable<Queue> Queue { get { return context.QueueSet; } }

        public void ConnectToCamera(Camera cam, IPrincipal User)
        {
            RemoveFromQueue(cam,User);
            StartUsingCamera(cam, User);
            cam.UserManager = User.Identity.Name;
            cam.State = State.Used;
            cam.CameraAction = CameraAction.Disconnect;
            context.SaveChanges();
        }
        public void DisconnectFromCamera(Camera cam, IPrincipal User)
        {
            EndUsingCamera(cam, User);
            cam.UserManager = null;
            cam.State = State.NotUsed;
            cam.CameraAction = CameraAction.Disconnect;
            context.SaveChanges();
        }

        public void TakeToQueue(Camera camera, IPrincipal User)
        {
            context.QueueSet.Add(new Queue { UserName = User.Identity.Name, CameraId = camera.Id});
            context.SaveChanges();
         
        }
        public void QuitFromQueue(Camera cam, IPrincipal User)
        {
            RemoveFromQueue(cam, User);
        }

        private void StartUsingCamera(Camera camera, IPrincipal User)
        {

            UserCamera uc = new UserCamera();
            uc.CameraId = camera.Id;
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            uc.UserId = userManager.Users.First(u => u.UserName == User.Identity.Name).Id;
            uc.UserName = userManager.Users.First(u => u.UserName == User.Identity.Name).UserName;
            uc.StartUse = DateTime.Now;

            context.UserCamera.Add(uc);

            context.SaveChanges();
           
        }
        private void EndUsingCamera(Camera camera, IPrincipal User)
        {

            var uc = context.UserCamera.First(c => c.CameraId == camera.Id && c.CommonTimeUse == null);
            uc.EndUse = DateTime.Now;
            var duration = (uc.EndUse.Value - uc.StartUse.Value);
            //ограничение в sqlDB time может хранить только до 1 суток (23.59.59)
            // как вариант хранить строку в бд, т.е. поменять тип в модели и  конвертировать перед сохранением
            uc.CommonTimeUse = (duration > new TimeSpan(23, 59, 55)) ? new TimeSpan(23, 59, 55) : duration;


            context.SaveChanges();
           
        }
        private void RemoveFromQueue(Camera cam, IPrincipal User)
        {
          
            var queuer = context.QueueSet.FirstOrDefault(q => q.UserName == User.Identity.Name);
            if (queuer != null)
            {
                context.QueueSet.Remove(queuer);
                context.SaveChanges();
            }

        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}