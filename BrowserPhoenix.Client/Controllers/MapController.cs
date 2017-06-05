using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowserPhoenix.Shared.Domain;
using BrowserPhoenix.Shared;
using PetaPoco;
using BrowserPhoenix.Client.Models;

namespace BrowserPhoenix.Client.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        
        // GET: Map
        public ActionResult Index(Int32? x, Int32? y)
        {
            if (!x.HasValue)
                x = 0;

            if (!y.HasValue)
                y = 0;

            var result = new MapModel();
            result.XStart = x.Value;
            result.YStart = y.Value;
            result.Colonies = GetColonies(x.Value, y.Value);
            
            return View(result);
        }

        public IEnumerable<Colony> GetColonies(Int32 x, Int32 y)
        {
            using (var db = DatabasePortal.Open())
            {
                var colonyList = db.Fetch<Colony, Player>(
                    Sql.Builder
                    .Append("SELECT Colony.id, Colony.name, Colony.x_cord, Colony.y_cord, Player.id, Player.username FROM Colony ")
                    .Append(Colony.JoinPlayer())
                    .Append("WHERE x_cord >= @0", x)
                    .Append("AND y_cord >= @0", y)
                    .Append("AND x_cord < @0", x + Map.XRange)
                    .Append("AND y_cord < @0", y + Map.YRange)
                    );

                return colonyList;
            }
        }
        
    }
}