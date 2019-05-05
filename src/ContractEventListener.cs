using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nethereum.Hex.HexTypes;

namespace ProDerivatives.Ethereum
{
    public class ContractEventListener<T>
    where T : new()
    {
        private readonly IEthereumClient _client;
        private readonly string _abi;
        private readonly string _contractAddress;
        private readonly string _eventName;
        private readonly Action<string, T> _callback;
        private readonly ILogger _logger;

        private Nethereum.Contracts.Event _event;
        private HexBigInteger _filter;

        public ContractEventListener(IEthereumClient client, string abi, string contractAddress, string eventName, Action<string, T> callback, ILogger logger)
        {
            _client = client;
            _abi = abi;
            _contractAddress = contractAddress;
            _eventName = eventName;
            _callback = callback;
            _logger = logger;
        }

        public async Task Start()
        {
            var contract = _client.GetContract(_abi, _contractAddress);
            _event = contract.GetEvent(_eventName);
            _filter = await _event.CreateFilterAsync();
        }

        public async Task GetChanges()
        {
            var newEvents = await _event.GetFilterChanges<T>(_filter);
            foreach (var newEvent in newEvents)
            {
                _callback(_contractAddress, newEvent.Event);
            }
        }
    }
}

