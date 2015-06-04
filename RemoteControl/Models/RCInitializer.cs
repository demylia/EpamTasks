using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RemoteControl.Models
{
    public class RCInitializer : DropCreateDatabaseAlways<RCContext>
    {
        protected override void Seed(RCContext context)
        {
            var but1 = new Button
            {
                Left = "130px", Right = "0px", Bottom = "0px", Top = "295px" ,
                 Width = "50px", Height = "50px" , Out = 1
            };
            var but2 = new Button
            {
                Left = "100px",
                Right = "0px",
                Bottom = "0px",
                Top = "100px",
                Width = "50px",
                Height = "50px",
                Out = 2
            };
            var but3 = new Button
            {
                Left = "130px",
                Right = "0px",
                Bottom = "0px",
                Top = "295px",
                Width = "50px",
                Height = "50px",
                Out = 3
            };
           
            var controller = new LANController
            {

                Name = "lan1",
                ControllerPath = "http://192.168.100.61:81/oc?",
                UserOwner = "admin",
                IsWork = true,
                IsPublic = true,
                Buttons = new  List<Button> { but1,but2}


            };
            context.LANController.Add(controller);
            var controller1 = new LANController
            {

                Name = "lan2",
                ControllerPath = "http://192.168.100.61:81/oc?",
                UserOwner = "admin",
                IsWork = true,
                IsPublic = true,
                Buttons = new List<Button> { but3 }


            };
            context.LANController.Add(controller1);

            context.Camera.Add(new Camera
            {
               
                Name = "cam1",
                CameraPath = "rtmp://192.168.100.61:1935/live/ipcamera.stream",
                UserOwner = "admin",
                
                State = State.NotUsed,
                CameraAction = CameraAction.Disconnect,
                Category = Category.Machines,
                IsWork = true,
                IsPublic = true,
                LANController = controller
                
            });
            context.Camera.Add(new Camera
            {
                
                Name = "cam2",
                CameraPath = "rtmp://192.168.100.61:1935/live/livestream",
                UserOwner = "admin",
                State = State.NotUsed,
                CameraAction = CameraAction.Disconnect,
                Category = Category.Machines,
                IsWork = true,
                IsPublic = true,
                LANController = controller1
            });
            context.Camera.Add(new Camera
            {
               
                Name = "cam3",
                CameraPath = "rtmp://192.168.100.61:1935/live/myStream",
                UserOwner = "admin",
                LANController = null,
                State = State.NotUsed,
                CameraAction = CameraAction.Disconnect,
                Category = Category.Machines,
                IsWork = true,
                IsPublic = true,
            });

            context.SaveChanges();
        }
    }

}