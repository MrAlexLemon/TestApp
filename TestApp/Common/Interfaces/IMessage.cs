using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IMessage
    {
        Task ShowMessage(ComputerInfo computerInfo);
        Task Ping();
    }
}
