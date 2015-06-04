using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RemoteControl.Models;

namespace RemoteControl
{
    [HubName("myHub")]
    public class MyHub : Hub
    {
        IRepository context = new CameraRepository();

        const string red = "red";
        const string green = "#37CB39";
        const string spanId = "span#";

       public void Connect(string camera)
        {

            Clients.Others.changeState(spanId + camera, "Busy", "red", "input#connect", "Take to the Queue");
           
            var id = Convert.ToInt32(camera);
            var queue = context.Camera.First(c => c.Id == id).Queue;
            var usersCount = queue.Count;
            if (usersCount > 0)
                Clients.Others.changeQueue(spanId + "q" + id, (--usersCount != 0) ? "In the queue - " + usersCount : "", red);
            var firstUserInQueue = (usersCount > 0) ? queue.ToArray()[0].UserName : "All";
           Clients.Others.changeButton("input#connect", "Take to the Queue", "#5cb85c", "takeToQueue",firstUserInQueue);
         }
        
        public void Disconnect(string camera)
        {
            Clients.Others.changeState(spanId + camera, "Free", green, "#quitFromQueue", "Connect");
            var id = Convert.ToInt32(camera);
            var queue = context.Camera.First(c => c.Id == id).Queue;
             var firstUserInQueue = (queue.Count > 0) ? queue.ToArray()[0].UserName : "All";
            Clients.Others.changeButton("input#quitFromQueue", "Connect", "#5cb85c", "connect", firstUserInQueue);
            Clients.Others.changeButton("input#takeToQueue", "Connect", "#5cb85c", "connect", firstUserInQueue);
       
        }
        public void TakeToQueue(string camera)
        {
            var id = Convert.ToInt32(camera);
            var queue = context.Camera.First(c => c.Id == id).Queue.Count +1;
            Clients.Others.changeQueue(spanId + "q" + id, (queue != 0) ? "In the queue - " + queue : "", red);
            
           
        }
        public void QuitFromQueue(string camera)
        {
            var id = Convert.ToInt32(camera);
            var queue = context.Camera.First(c => c.Id == id).Queue.Count -1;
            Clients.Others.changeQueue(spanId + "q" + id, (queue != 0) ? "In the queue - " + queue : "", red);
        }

       
    }
}