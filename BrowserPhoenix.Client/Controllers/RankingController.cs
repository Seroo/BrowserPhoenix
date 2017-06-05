using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.Mvc;

namespace BrowserPhoenix.Client.Controllers
{
    public class RankingController : Controller
    {
        // GET: Ranking
        public ActionResult Index()
        {
            using (var db = new PetaPoco.Database("BrowserPhoenixDB"))
            {
                var playerList = db.Query<Player>("SELECT * FROM Player Order By score desc");

                //id / name / score
                var result = new List<Tuple<Int64, String, Int64>>();

                foreach (var p in playerList)
                {
                    result.Add(new Tuple<long, string, long>(p.Id, p.Username, p.Score));
                }

                return View(result);
            }
        }

        public ActionResult UpdatePoints(Int64 Id)
        {
           
            var command = new GetPointsCommand();

            command.PlayerId = Id;
            command.Points = DateTime.UtcNow.Minute;

            CommandPortal.Send(command);

            return Redirect("/Ranking");
        }
    }
}