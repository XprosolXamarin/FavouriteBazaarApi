using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FavouriteBazaarApi.Model
{
    public class clsUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
       
        public string StatusString { get; set; }
       
    }
}