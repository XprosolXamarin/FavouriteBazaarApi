using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
namespace FavouriteBazaarApi.Models.DAL.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int? UserId { get; set; }
        public string Name { get; set; }
        public string[] roles { get; set; }
        public int View { get; set; }
        public int Save { get; set; }
        public int Change { get; set; }
        public int Delete { get; set; }
        public int ProjectId { get; set; }
        public int BranchId { get; set; }
        public string Email { get; set; }

        public string modulename { get; set; }
        public string UserImage { get; set; }

        public int? FiscalYearId { get; set; }

        public string FiscalYearName { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int? UserId { get; set; }
        public int? Multi { get; set; }
        public int? FiscalYearId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string BranchName { get; set; }
        public string FiscalYearName { get; set; }
        public string[] roles { get; set; }
        public int View { get; set; }
        public int Save { get; set; }
        public int Change { get; set; }
        public int Delete { get; set; }
        public string Email { get; set; }
        public string modulename { get; set; }
        public string UserImage { get; set; }


    }
}