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
    
    public partial class Ship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ship()
        {
            this.BimehAfrad = new HashSet<BimehAfrad>();
            this.DebtHistory = new HashSet<DebtHistory>();
            this.Note = new HashSet<Note>();
            this.Order = new HashSet<Order>();
            this.PackageToShip = new HashSet<PackageToShip>();
            this.Personnel = new HashSet<Personnel>();
            this.ShipToPay = new HashSet<ShipToPay>();
            this.ShipToSahamDar = new HashSet<ShipToSahamDar>();
            this.Soom = new HashSet<Soom>();
            this.TankhaGardanToShip = new HashSet<TankhaGardanToShip>();
        }
    
        public int Id { get; set; }
        public long IdPersonnel { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public System.DateTime YearProduction { get; set; }
        public Nullable<int> Tul { get; set; }
        public Nullable<int> Arz { get; set; }
        public string NameMotor { get; set; }
        public Nullable<int> HajMotor { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string ShomareBime { get; set; }
        public string Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BimehAfrad> BimehAfrad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DebtHistory> DebtHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Note { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageToShip> PackageToShip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personnel> Personnel { get; set; }
        public virtual Personnel Personnel1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipToPay> ShipToPay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipToSahamDar> ShipToSahamDar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Soom> Soom { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TankhaGardanToShip> TankhaGardanToShip { get; set; }
    }
}
