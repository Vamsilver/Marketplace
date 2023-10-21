using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.ADOModel
{
    public class ViewProduct : Product
    {
        int AmountOfLikes = 0;

        public int GetAmountOfLikes => AmountOfLikes;

        public ViewProduct(Product product)
        {
            idProduct = product.idProduct;

            Title = product.Title;
            Description = product.Description;
            idUser = product.idUser;
            idProductCategory = product.idProductCategory;
            idProductBirthRate = product.idProductBirthRate;
            onSell = product.onSell;
            Cost = product.Cost;
            isApproved = product.isApproved;
            image = product.image;
            AmountOfSales = product.AmountOfSales;
            OldCost = product.OldCost;
            BasketProduct = product.BasketProduct;
            Like = product.Like;
            ProductBirthRate = product.ProductBirthRate;
            ProductCategory = product.ProductCategory;
            User = product.User;
            ProductAddRequest = product.ProductAddRequest;

            AmountOfLikes = App.Connection.Like.Where(z => z.idProduct.Equals(product.idProduct)).Count();
        }

    }
}
