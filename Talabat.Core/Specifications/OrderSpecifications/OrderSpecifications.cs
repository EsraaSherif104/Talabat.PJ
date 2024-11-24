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

        public OrderSpecifications(string email):base(o=>o.BuyerEmail==email) 
        {

            Include.Add(o => o.DeliveryMethod);
            Include.Add(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }

    }
}
