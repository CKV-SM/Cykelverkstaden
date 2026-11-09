using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services;

public class WorkshopService
{
    private IWorkorderRepository dataRepo;

    public WorkshopService(IWorkorderRepository dataRepo)
    {
        this.dataRepo = dataRepo;
    }

    public WorkOrder CreateWorkOrder(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Ingen beskrivning", nameof(description));
        }
        WorkOrder orderToCreate = new WorkOrder(description);

        dataRepo.Add(orderToCreate);

        return orderToCreate;
    }

    public List<WorkOrder> GetOrders()
    {
        return dataRepo.GetAll();
    }
}
