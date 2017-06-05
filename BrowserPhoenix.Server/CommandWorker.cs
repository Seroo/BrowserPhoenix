using BrowserPhoenix.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Server
{
    public class CommandWorker
    {
        // This method will be called when the thread is started.
        public void DoWork()
        {
            while (!_shouldStop)
            {
                CommandPortal.ProcessNext();
                
                Console.WriteLine("worker thread: working...");
            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
    }

}