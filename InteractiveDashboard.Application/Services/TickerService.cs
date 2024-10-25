﻿using InteractiveDashboard.Application.InfrastructureServices;
using InteractiveDashboard.Domain.Dtos;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace InteractiveDashboard.Application.Services
{
    public class TickerService : ITickerService
    {
        ConcurrentDictionary<string, TickerDto> _tickers = new();
        private readonly IDateTimeService _dateTimeService;
        private readonly IHubContext<TickerHub, IPriceTickerClient> _hubContext;

        public TickerService(IHubContext<TickerHub, IPriceTickerClient> hubContext, IDateTimeService dateTimeService)
        {
            _hubContext = hubContext;
            _dateTimeService = dateTimeService;
        }

        public List<string> GetAllTickers()
        {
            throw new NotImplementedException();
        }

        public TickerDto GetTicker(string tickerName)
        {
            throw new NotImplementedException();
        }

        public Task PushPrice(string tickerNamne, decimal askPrice, decimal bidPrice)
        {
            throw new NotImplementedException();
        }
    }
}