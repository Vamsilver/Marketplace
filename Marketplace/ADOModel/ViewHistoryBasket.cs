using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.ADOModel
{
    public class ViewHistoryBasket : Basket
    {
        decimal Summ = 0;

        public ViewHistoryBasket( Basket basket)
        {
            var list = App.Connection.BasketProduct.Where(z => z.idBasket.Equals(basket.idBasket)).ToList();

            foreach (var item in list)
            {
                var list1 = App.Connection.Product.Where(z => z.idProduct.Equals(item.idProduct)).ToList();

                foreach(var item1 in list1)
                {
                    Summ += item1.Cost * item.Count;
                }
            }

            idBasket = basket.idBasket;
            idUser = basket.idUser;
            PurchaseDate = basket.PurchaseDate;
        }

        public decimal GetSumm 
        { 
            get { return Summ; } 
            set { Summ = value; } 
        }
    }
}
