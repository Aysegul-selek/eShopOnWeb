﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Infrastructure.Data.Queries;
public class OrderStatusQueryService : IOrderStatusQueryService
{
    private readonly CatalogContext _dbContext;

    public OrderStatusQueryService(CatalogContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderStatus> ChangeOrderStatusAsync(int orderStatusId, string newStatus)
    {
        var order = await _dbContext.OrderStatuses.FindAsync(orderStatusId);
         
        order.Name = newStatus;

        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<List<OrderStatus>> GetAllOrderStatuses()
    {
        return await _dbContext.OrderStatuses.ToListAsync();
    }

    public async Task<string> GetOrderStatusById(int orderStatusId)
    {
        var orderStatus = await _dbContext.OrderStatuses
                                        .Where(os => os.OrderStatusId == orderStatusId)
                                        .Select(os => os.Name)
                                        .FirstOrDefaultAsync();

        return orderStatus;
    }

    
}
