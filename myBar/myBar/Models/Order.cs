//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myBar.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderID { get; set; }
        public string Notes { get; set; }
        public double Price { get; set; }
        public int PlaceID { get; set; }
        public int ProductID { get; set; }
        public int OrderStateID { get; set; }
        public int UnitID { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> InvoicePos { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual InvoicePos InvoicePos1 { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual Place Place { get; set; }
        public virtual Product Product { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
