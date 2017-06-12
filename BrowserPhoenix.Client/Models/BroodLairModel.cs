﻿using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Client.Models
{
    public class BroodLairModel
    {
        public Building Building { get; set; }
        public IEnumerable<Troop> Troops { get; set; }
    }
}