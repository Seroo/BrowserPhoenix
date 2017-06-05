using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Client.Models
{
    public class MapModel
    {
        public IEnumerable<Colony> Colonies { get; set; }
        public Int32 XStart { get; set; }
        public Int32 YStart { get; set; }
    }
}