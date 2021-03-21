using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interfaces
{
    public interface IMessage
    {
        Task ShowMessage(ComputerInfo computerInfo);
        Task Ping();
    }
}
