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

        public static Product ConvertToProduct(BusketProduct busketProduct)
        {
            Product product = new Product()
            {
                idProduct = busketProduct.idProduct,
                Title = busketProduct.Title,
                Description = busketProduct.Description,
                idUser = busketProduct.idUser,
                idProductBirthRate = busketProduct.idProductBirthRate,
                idProductCategory = busketProduct.idProductCategory,
                onSell = busketProduct.onSell,
                Cost = busketProduct.Cost,
                isApproved = busketProduct.isApproved,
                image = busketProduct.image,
                AmountOfSales = busketProduct.AmountOfSales,
                OldCost = busketProduct.OldCost,
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

        public static List<BusketProduct> ConvertToListBusketProducts(List<Product> list)
        {
            List<BusketProduct> viewProductList = new List<BusketProduct>();

            foreach (Product product in list)
                viewProductList.Add(new BusketProduct(product));

            return viewProductList;
        }
    }
}
