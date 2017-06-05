using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    

    public static class DatabasePortal
    {
        private static Database dbConn = null;
        public static Database Open()
        {
            var result = new PetaPoco.Database("BrowserPhoenixDB");
          //  dbConn.OpenSharedConnection();
      //      dbConn.CloseSharedConnection();
            
            return result;
        }
        
    }
}