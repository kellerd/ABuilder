﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABuilder.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ABuilderContainer : DbContext
    {
        public ABuilderContainer()
            : base("name=ABuilderContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<SingleModel> SingleModels { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<SingleModel_Stat> SingleModel_Stat { get; set; }
        public DbSet<Equation> Equations { get; set; }
    }
}