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
    
    public partial class BasketProduct
    {
        public int idBasketProduct { get; set; }
        public int idBasket { get; set; }
        public int idProduct { get; set; }
        public int Count { get; set; }
    
        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}