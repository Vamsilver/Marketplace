﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marketplace.ADOModel
{
    public class OldBusketProduct : Product
    {
        int count = 1;

        public int GetCountInBasket
        {
            get { return count; }
            set { count = value; }
        }

        Visibility VisibiliityOldCost = Visibility.Visible;
        public Visibility GetVisibilityOldCost => VisibiliityOldCost;

        public OldBusketProduct(Product product)
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

            if (OldCost == null)
                VisibiliityOldCost = Visibility.Hidden;

            BasketProduct = product.BasketProduct;
            Like = product.Like;
            ProductBirthRate = product.ProductBirthRate;
            ProductCategory = product.ProductCategory;
            User = product.User;
            ProductAddRequest = product.ProductAddRequest;

            var basket = App.Connection.Basket.Where(z => z.idUser.Equals(App.CurrentUser.idUser) && !z.PurchaseDate.Equals(null)).FirstOrDefault();
            var basketProduct = App.Connection.BasketProduct.Where(z => z.idProduct.Equals(idProduct) && z.idBasket.Equals(basket.idBasket)).FirstOrDefault();
            if (basketProduct != null)
                GetCountInBasket = basketProduct.Count;
        }
    }
}
