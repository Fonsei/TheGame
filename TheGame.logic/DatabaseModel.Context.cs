﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheGame.logic
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MmorpgTheGameEntities : DbContext
    {
        public MmorpgTheGameEntities()
            : base("name=MmorpgTheGameEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Benutzer> AlleBenutzer { get; set; }
        public virtual DbSet<BenutzerProfil> AlleBenutzerProfile { get; set; }
        public virtual DbSet<BenutzerSettings> BenutzerSettings { get; set; }
    }
}
