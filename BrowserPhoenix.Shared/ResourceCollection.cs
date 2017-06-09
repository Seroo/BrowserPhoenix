using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    public class ResourceCollection
    {
        public float Sand { get; set; }

        public float Sugar { get; set; }
        
        public float Chitin { get; set; }
        
        public float Leave { get; set; }
        
        public float Larvae { get; set; }
        
        public float Food { get; set; }   
        
        public float GetResource(ResourceType type)
        {
            switch(type)
            {
                case ResourceType.Chitin:
                    return Chitin;

                case ResourceType.Food:
                    return Food;

                case ResourceType.Larvae:
                    return Larvae;

                case ResourceType.Leave:
                    return Leave;

                case ResourceType.Sand:
                    return Sand;

                case ResourceType.Sugar:
                    return Sugar;

                default:
                    return 1000;
            }
        }
    }
}