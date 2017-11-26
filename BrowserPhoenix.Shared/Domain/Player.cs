using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("player")]
    [PrimaryKey("id")]
    public class Player
    {
        [Column("id")]
        public Int64 Id { get; set; }
        [Column("username")]
        public String Username { get; set; }
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
        [Column("application_id")]
        public String ApplicationId { get; set; }
        [Column("is_deleted")]
        public Boolean IsDeleted { get; set; }
        [Column("score")]
        public Int64 Score { get; set; }
        
        
        public static String JoinResource()
        {
            return " LEFT JOIN player_resource ON player.player_resource_id = player_resource.id ";
        }

        private static Player _current { get; set; }

        public static Player Current {
            get
            {
                if (_current == null)
                {
                    _current = new Player { Username = "Anonymous" };
                }
                return _current;
            }
            set
            {
                if(value == null)
                {
                    _current = new Player { Username = "Anonymous" };
                }
                else
                {
                    _current = value;
                }
            }
        }

        [Ignore]
        private Colony _colony { get; set; }
        [Ignore]
        private DateTime _lastColonyUpdate { get; set; }


        [Ignore]
        public Colony Colony
        {
            get
            {

                if (_colony == null)
                {
                    using (var db = DatabasePortal.Open())
                    {
                        _colony = Colony.GetPlayersFirst(db, this.Id);
                        _lastColonyUpdate = DateTime.Now;
                    }
                }
                else
                {
                    //experimentel
                    if (_lastColonyUpdate.AddSeconds(5) < DateTime.Now)
                    {

                        var command = new RecalculateResourcesCommand(_colony.Id, DateTime.Now);
                        CommandPortal.Send(command);
                        using (var db = DatabasePortal.Open())
                        {
                            _colony = Colony.GetById(db, _colony.Id);
                            _lastColonyUpdate = DateTime.Now;
                        }
                    }
                }

                return _colony;
            }
            set
            {
                _lastColonyUpdate = DateTime.Now;
                _colony = value;
            }
        }

        [Ignore]
        public Boolean IsAnonymous { get { return Player.Current.Username == "Anonymous"; } private set { } }

        
        public static Player Authentificate(String id)
        {
            Player player = null;



            if (Player.Current.IsAnonymous || Player.Current.ApplicationId != id)
            {
                using (var db = new PetaPoco.Database("BrowserPhoenixDB"))
                {
                    player = db.FirstOrDefault<Player>(Sql.Builder.Append("SELECT * FROM Player ")
                        .Append(" where application_id = '" + id + "'"));
                    Player.Current = player;
                }
            }
            else
            {
                player = Player.Current;
            }
            

            if (player == null || player.IsDeleted)
            {
                //ausloggen den guy
                return null;
            }

           
            return player;
        }

        public static IEnumerable<Player> GetAll(Database db)
        {
            return db.Fetch<Player>(
                    Sql.Builder
                    .Append("SELECT * FROM Player")
                    .Append(" order by player.create_date asc")
                    );
        }

        public static void Deauthenticate()
        {
            Player.Current = null;
        }

        public void CreateFirstColony()
        {
            using (var db = DatabasePortal.Open())
            {
                var random = new Random();
                var colony = Colony.Create(db, this.Id, this.Username + " Colony", random.Next(1, 9), random.Next(1, 9));

                colony = Colony.GetById(db,colony.Id);

                colony.Resources.AddStartingResources(db);
                
            }
               
        }
       
        public void RecalculateScore(Database db)
        {
            var score = 0;

            var colonies = Colony.GetByPlayerId(db, this.Id);

            foreach(var colony in colonies)
            {
                var buildings = Building.GetByColonyId(db, Colony.Id);

                foreach(var building in buildings)
                {
                    score += building.Level;
                }
            }

            this.Score = score;
            db.Save(this);
        }
    }

}