using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ProDerivatives.Ethereum
{
    public class EventListener<T>
    where T : new()
    {
        private readonly IEthereumClient _client;
        private readonly string _abi;
        private readonly string _contractAddress;
        private readonly string _eventName;
        private readonly int _pollingIntervall;
        private readonly Action<T> _callback;
        private readonly ILogger<T> _logger;


        public EventListener(IEthereumClient client, string abi, string contractAddress, string eventName, int pollingIntervall, Action<T> callback, ILogger<T> logger)
        {
            _client = client;
            _abi = abi;
            _contractAddress = contractAddress;
            _eventName = eventName;
            _pollingIntervall = pollingIntervall;
            _callback = callback;
            _logger = logger;
        }

        public Task Start()
        {
            return Task.Run(Listen);
        }

        private async Task Listen()
        {
            _logger.LogInformation($"Start listening for {_eventName} events on {_contractAddress}");
            var contract = _client.GetContract(_abi, _contractAddress);
            var contractEvent = contract.GetEvent(_eventName);
            var getAll = await contractEvent.CreateFilterAsync();
            while (true)
            {
                await Task.Delay(_pollingIntervall);
                _logger.LogInformation("Ping");
                var newEvents = await contractEvent.GetFilterChanges<T>(getAll);
                foreach (var newEvent in newEvents)
                {
                    _callback(newEvent.Event);
                }
            }
        }

    }
}

