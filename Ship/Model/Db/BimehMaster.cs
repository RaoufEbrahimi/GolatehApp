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
    
    public partial class BimehMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BimehMaster()
        {
            this.BimehDeteil = new HashSet<BimehDeteil>();
        }
    
        public decimal Id { get; set; }
        public string DSK_ID { get; set; }
        public string DSK_NAME { get; set; }
        public string DSK_FARM { get; set; }
        public string DSK_ADRS { get; set; }
        public Nullable<decimal> DSK_KIND { get; set; }
        public Nullable<decimal> DSK_YY { get; set; }
        public Nullable<decimal> DSK_MM { get; set; }
        public string DSK_LISTNO { get; set; }
        public string DSK_DISC { get; set; }
        public Nullable<decimal> DSK_NUM { get; set; }
        public Nullable<decimal> DSK_TDD { get; set; }
        public Nullable<decimal> DSK_TROOZ { get; set; }
        public Nullable<decimal> DSK_TMAH { get; set; }
        public Nullable<decimal> DSK_TMAZ { get; set; }
        public Nullable<decimal> DSK_TMASH { get; set; }
        public Nullable<decimal> DSK_TTOTL { get; set; }
        public Nullable<decimal> DSK_TBIME { get; set; }
        public Nullable<decimal> DSK_TKOSO { get; set; }
        public Nullable<decimal> DSK_BIC { get; set; }
        public Nullable<decimal> DSK_RATE { get; set; }
        public Nullable<decimal> DSK_PRATE { get; set; }
        public Nullable<decimal> DSK_BIMH { get; set; }
        public string MON_PYM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BimehDeteil> BimehDeteil { get; set; }
    }
}
