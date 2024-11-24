using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggra;

namespace Talabat.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications:BaseSpecification<Order>
    {
        //get orders for user
        public OrderSpecifications(string email):base(o=>o.BuyerEmail==email) 
        {

            Include.Add(o => o.DeliveryMethod);
            Include.Add(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
        //used to get order for user
        public OrderSpecifications(string email,int id):base(o=>o.Id==id && o.BuyerEmail==email)
        {
            Include.Add(o => o.DeliveryMethod);
            Include.Add(o => o.Items);
        }
    }
}
