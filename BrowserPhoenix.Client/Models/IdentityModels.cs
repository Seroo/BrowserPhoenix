using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BrowserPhoenix.Shared;
using System;
using BrowserPhoenix.Shared.Domain;

namespace BrowserPhoenix.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Beachten Sie, dass der "authenticationType" mit dem in "CookieAuthenticationOptions.AuthenticationType" definierten Typ übereinstimmen muss.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Benutzerdefinierte Benutzeransprüche hier hinzufügen
            return userIdentity;
        }

        public static ApplicationUser Create(string username,string email)
        {
            var result = new ApplicationUser();

            result.UserName = username;
            
            result.Email = email;

            return result;
        }

        public Player CreatePlayer()
        {
            var result = new Player();
            result.CreateDate = DateTime.UtcNow;
            result.Username = this.UserName;
            result.ApplicationId = this.Id;

            using (var db = new PetaPoco.Database("BrowserPhoenixDB"))
            {
                db.Save(result);
            }

            return result;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}