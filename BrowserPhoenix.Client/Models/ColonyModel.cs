using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Client.Models
{
    public class ColonyModel
    {
        public Colony Colony { get; set; }
        public IEnumerable<Building> Buildings { get; set; }
    }
}