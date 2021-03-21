using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Entities;

namespace WebService.Interfaces
{
    public interface IMessage
    {
        Task ShowMessage(ComputerInfo computerInfo);
        Task Ping();
    }
}
