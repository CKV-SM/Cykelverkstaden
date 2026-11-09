using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public interface IWorkorderRepository
    {
        void Add(WorkOrder order);
        List<WorkOrder> GetAll();
    }
}
