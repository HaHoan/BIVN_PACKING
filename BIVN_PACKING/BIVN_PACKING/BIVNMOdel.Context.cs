﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIVN_PACKING
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BIVNEntities : DbContext
    {
        public BIVNEntities()
            : base("name=BIVNEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Produce> Produces { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Repair> Repairs { get; set; }
    }
}
