using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Data;

public class MemoryWorkorderRepository : IWorkorderRepository
{
    private List<WorkOrder> _orders = [];

    public void Add(WorkOrder order)
    {
        _orders.Add(order);
    }

    public List<WorkOrder> GetAll()
    {
        return _orders;
    }
}
