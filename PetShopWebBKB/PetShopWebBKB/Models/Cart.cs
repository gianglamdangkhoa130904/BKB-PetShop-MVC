using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShopWebBKB.Models
{
    public class CartItem
    {
        public Product _pro { get; set; }
        public int _quantity { get; set; }
    }

    public class Cart
    {
        List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add_Product_Cart(Product _pe, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._pro.ID == _pe.ID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _pro = _pe,
                    _quantity = _quan
                });
            }
            else
            {
                item._quantity += _quan;
            }
        }

        public int Total_quantity()
        {
            return items.Sum(s => s._quantity);
        }

        public decimal Total_money()
        {
            var total = items.Sum(s => s._quantity * s._pro.Price);
            return (decimal)total;
        }

        public void Update_quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._pro.ID == id);
            if (item != null)
            {
                if (items.Find(s => s._pro.RemainQuantity > _new_quan) != null)
                    item._quantity = _new_quan;
                else item._quantity = 1;
            }
        }

        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._pro.ID == id);
        }

        public void ClearCart()
        {
            items.Clear();
        }
        public class CartItem
        {
            public Product _pro  { get; set; }
            public int _quantity { get; set; }
        }

    }
}