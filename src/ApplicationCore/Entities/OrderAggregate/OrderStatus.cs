using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
public class OrderStatus:IAggregateRoot
{
    [Key]
    public int OrderStatusId { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }

}

