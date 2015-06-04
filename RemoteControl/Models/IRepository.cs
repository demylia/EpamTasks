using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace RemoteControl.Models
{
    public interface IRepository
    { 
       
        IEnumerable<Camera> Camera { get; }
        IEnumerable<UserCamera> UserCamera { get; }
        IEnumerable<Queue> Queue { get; }

        void ConnectToCamera(Camera cam, IPrincipal User);
        void DisconnectFromCamera(Camera cam, IPrincipal User);
        void TakeToQueue(Camera camera, IPrincipal User);
        void QuitFromQueue(Camera cam, IPrincipal User);
       
      
    }
}