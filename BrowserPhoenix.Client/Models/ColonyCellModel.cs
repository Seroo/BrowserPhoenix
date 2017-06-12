using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Client.Models
{
    public class ColonyCellModel
    {
        public Building Building { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public ColonyCellModel(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public ColonyCellModel(Int32 x, Int32 y, Building building)
        {
            X = x;
            Y = y;
            Building = building;
        }
    }
}