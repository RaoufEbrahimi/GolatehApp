﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ship.Model.Db
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GolatehEntities : DbContext
    {
        public GolatehEntities()
            : base("name=GolatehEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BankFileDeteil> BankFileDeteil { get; set; }
        public virtual DbSet<BankFileMster> BankFileMster { get; set; }
        public virtual DbSet<Bimeh> Bimeh { get; set; }
        public virtual DbSet<BimehAfrad> BimehAfrad { get; set; }
        public virtual DbSet<BimehDeteil> BimehDeteil { get; set; }
        public virtual DbSet<BimehMaster> BimehMaster { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<DebtHistory> DebtHistory { get; set; }
        public virtual DbSet<JoinIt> JoinIt { get; set; }
        public virtual DbSet<Kala> Kala { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<OptionParameter> OptionParameter { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageToShip> PackageToShip { get; set; }
        public virtual DbSet<Personnel> Personnel { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePernonnel> RolePernonnel { get; set; }
        public virtual DbSet<Safar> Safar { get; set; }
        public virtual DbSet<SafarToAfrad> SafarToAfrad { get; set; }
        public virtual DbSet<SafarToFactor> SafarToFactor { get; set; }
        public virtual DbSet<SafarToHandMoney> SafarToHandMoney { get; set; }
        public virtual DbSet<SafarToSahamDar> SafarToSahamDar { get; set; }
        public virtual DbSet<Semat> Semat { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }
        public virtual DbSet<ShipToPay> ShipToPay { get; set; }
        public virtual DbSet<ShipToSahamDar> ShipToSahamDar { get; set; }
        public virtual DbSet<ShomareOrder> ShomareOrder { get; set; }
        public virtual DbSet<SiteSetting> SiteSetting { get; set; }
        public virtual DbSet<Soom> Soom { get; set; }
        public virtual DbSet<Subscribe> Subscribe { get; set; }
        public virtual DbSet<TankhaGardan> TankhaGardan { get; set; }
        public virtual DbSet<TankhaGardanToShip> TankhaGardanToShip { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
    }
}
