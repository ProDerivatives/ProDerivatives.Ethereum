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
        private readonly Action<string, BlockInfo, T> _callback;
        private readonly ILogger _logger;

        private Nethereum.Contracts.Event _event;
        private HexBigInteger _filter;

        public ContractEventListener(IEthereumClient client, string abi, string contractAddress, string eventName, Action<string, BlockInfo, T> callback, ILogger logger)
        {
            _client = client;
            _abi = abi;
            _contractAddress = contractAddress;
            _eventName = eventName;
            _callback = callback;
            _logger = logger;
        }

        public async Task Init()
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
                _callback(_contractAddress, new BlockInfo(newEvent.Log.BlockNumber, DateTime.UtcNow), newEvent.Event);
            }
        }

        public async Task GetAllChanges()
        {
            var filter = await _event.CreateFilterAsync(new Nethereum.RPC.Eth.DTOs.BlockParameter(0));
            var newEvents = await _event.GetAllChanges<T>(filter);
            foreach (var newEvent in newEvents)
            {
                var blockInfo = await _client.GetBlockInfo(newEvent.Log.BlockNumber);
                _callback(_contractAddress, blockInfo, newEvent.Event);
            }
        }
    }
}

