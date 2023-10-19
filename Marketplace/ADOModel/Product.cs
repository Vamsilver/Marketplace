//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Marketplace.ADOModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.BasketProduct = new HashSet<BasketProduct>();
            this.Like = new HashSet<Like>();
            this.ProductAddRequest = new HashSet<ProductAddRequest>();
        }
    
        public int idProduct { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int idUser { get; set; }
        public int idProductCategory { get; set; }
        public int idProductBirthRate { get; set; }
        public bool onSell { get; set; }
        public decimal Cost { get; set; }
        public bool isApproved { get; set; }
        public byte[] image { get; set; }
        public Nullable<int> AmountOfSales { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketProduct> BasketProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Like { get; set; }
        public virtual ProductBirthRate ProductBirthRate { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAddRequest> ProductAddRequest { get; set; }
    }
}
