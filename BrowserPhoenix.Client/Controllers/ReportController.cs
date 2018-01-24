using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;
using PetaPoco;

namespace BrowserPhoenix.Client.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            using (var db = DatabasePortal.Open())
            {
                var result = db.Fetch<BattleReport>(
                    Sql.Builder
                    .Append("SELECT * FROM battle_report")
                    .Append(" WHERE battle_report.attacker_id  =@0", Player.Current.Id)
                    .Append(" OR battle_report.defender_id =@0 ", Player.Current.Id)
                    );

                return View(result);
            }
        }

        public ActionResult Detail(Int64 id)
        {
            using (var db = DatabasePortal.Open())
            {
                var result = db.Fetch<BattleReport>(
                    Sql.Builder
                    .Append("SELECT * FROM battle_report ")
                    .Append(" WHERE battle_report.id =@0 ", id)
                    );

                return View(result.First());
            }
        }
    }
}