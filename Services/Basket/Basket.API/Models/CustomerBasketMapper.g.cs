using System.Collections.Generic;
using Basket.API.Models;
using Basket.DAL.Entities;

namespace Basket.API.Models
{
    public static partial class CustomerBasketMapper
    {
        public static CustomerBasket AdaptToCustomerBasket(this CustomerBasketDto p1)
        {
            return p1 == null ? null : new CustomerBasket()
            {
                BuyerId = p1.BuyerId,
                Items = funcMain1(p1.Items)
            };
        }
        public static CustomerBasketDto AdaptToDto(this CustomerBasket p3)
        {
            return p3 == null ? null : new CustomerBasketDto()
            {
                BuyerId = p3.BuyerId,
                Items = funcMain2(p3.Items)
            };
        }
        
        private static List<BasketItem> funcMain1(List<BasketItemDto> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<BasketItem> result = new List<BasketItem>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                BasketItemDto item = p2[i];
                result.Add(item == null ? null : new BasketItem()
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    PictureUrl = item.PictureUrl,
                    ProductId = item.ProductId
                });
                i++;
            }
            return result;
            
        }
        
        private static List<BasketItemDto> funcMain2(List<BasketItem> p4)
        {
            if (p4 == null)
            {
                return null;
            }
            List<BasketItemDto> result = new List<BasketItemDto>(p4.Count);
            
            int i = 0;
            int len = p4.Count;
            
            while (i < len)
            {
                BasketItem item = p4[i];
                result.Add(item == null ? null : new BasketItemDto()
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    PictureUrl = item.PictureUrl,
                    ProductId = item.ProductId
                });
                i++;
            }
            return result;
            
        }
    }
}