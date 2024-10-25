using InteractiveDashboard.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveDashboard.Application.InfrastructureServices
{
    public interface IPriceTickerClient
    {
        Task PriceUpdate(TickerDto ticker);
    }
}
