//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Personnel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personnel()
        {
            this.BimehAfrad = new HashSet<BimehAfrad>();
            this.DebtHistory = new HashSet<DebtHistory>();
            this.Kala = new HashSet<Kala>();
            this.Note = new HashSet<Note>();
            this.Order = new HashSet<Order>();
            this.RolePernonnel = new HashSet<RolePernonnel>();
            this.Safar = new HashSet<Safar>();
            this.SafarToAfrad = new HashSet<SafarToAfrad>();
            this.SafarToHandMoney = new HashSet<SafarToHandMoney>();
            this.SafarToSahamDar = new HashSet<SafarToSahamDar>();
            this.Ship1 = new HashSet<Ship>();
            this.ShipToSahamDar = new HashSet<ShipToSahamDar>();
            this.Soom = new HashSet<Soom>();
            this.TankhaGardan = new HashSet<TankhaGardan>();
        }
    
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ShebaNumber { get; set; }
        public string ShomareCart14 { get; set; }
        public string Image { get; set; }
        public Nullable<long> IdParent { get; set; }
        public Nullable<int> IdShip { get; set; }
        public string FatherName { get; set; }
        public string Adress { get; set; }
        public string BimehNumber { get; set; }
        public string EstekhdamDate { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAdress { get; set; }
        public string SmsActive { get; set; }
        public Nullable<int> IdBank { get; set; }
        public string ShomareHesab { get; set; }
    
        public virtual Bank Bank { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BimehAfrad> BimehAfrad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DebtHistory> DebtHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kala> Kala { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Note { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        public virtual Ship Ship { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePernonnel> RolePernonnel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Safar> Safar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SafarToAfrad> SafarToAfrad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SafarToHandMoney> SafarToHandMoney { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SafarToSahamDar> SafarToSahamDar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ship> Ship1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipToSahamDar> ShipToSahamDar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Soom> Soom { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TankhaGardan> TankhaGardan { get; set; }
    }
}
