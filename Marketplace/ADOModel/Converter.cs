using Marketplace.ADOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace
{
    public static class Converter
    {
        public static Product ConvertToProduct(ViewProduct viewProduct)
        {
            Product product = new Product()
            {
                idProduct = viewProduct.idProduct,
                Title = viewProduct.Title,
                Description = viewProduct.Description,
                idUser = viewProduct.idUser,
                idProductBirthRate = viewProduct.idProductBirthRate,
                idProductCategory = viewProduct.idProductCategory,
                onSell = viewProduct.onSell,
                Cost = viewProduct.Cost,
                isApproved = viewProduct.isApproved,
                image = viewProduct.image,
                AmountOfSales = viewProduct.AmountOfSales,
                OldCost = viewProduct.OldCost,
            };

            return product;
        }

        public static List<ViewProduct> ConvertToListViewProducts(List<Product> list)
        {
            List<ViewProduct> viewProductList = new List<ViewProduct>();

            foreach (Product product in list)
                viewProductList.Add(new ViewProduct(product));

            return viewProductList;
        }
    }
}
