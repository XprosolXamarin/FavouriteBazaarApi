﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FavouriteBazaarApi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ModelDb : DbContext
    {
        public ModelDb()
            : base("name=ModelDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblUser> tblUsers { get; set; }
    
        public virtual int spInsertUpdateUser(Nullable<int> id, string userName, string password, string firstName, string lastName, string email, string contact1, string contact2, Nullable<bool> status, string address, string state, string insertUpdateStatus, ObjectParameter checkReturn, ObjectParameter checkReturn2)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var contact1Parameter = contact1 != null ?
                new ObjectParameter("Contact1", contact1) :
                new ObjectParameter("Contact1", typeof(string));
    
            var contact2Parameter = contact2 != null ?
                new ObjectParameter("Contact2", contact2) :
                new ObjectParameter("Contact2", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var stateParameter = state != null ?
                new ObjectParameter("State", state) :
                new ObjectParameter("State", typeof(string));
    
            var insertUpdateStatusParameter = insertUpdateStatus != null ?
                new ObjectParameter("InsertUpdateStatus", insertUpdateStatus) :
                new ObjectParameter("InsertUpdateStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertUpdateUser", idParameter, userNameParameter, passwordParameter, firstNameParameter, lastNameParameter, emailParameter, contact1Parameter, contact2Parameter, statusParameter, addressParameter, stateParameter, insertUpdateStatusParameter, checkReturn, checkReturn2);
        }
    }
}
