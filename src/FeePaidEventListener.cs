using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace ProDerivatives.Ethereum
{
    public class FeePaidEventListener
    {
        private string _owner;
        private IEthereumClient _client;
        private IContract _derivative;  
        private string _referenceAccountAddress;
        private ICollection<string> _derivativeAddresses;

        public FeePaidEventListener(IEthereumClient client, string owner, IContract derivativeContract, string referenceAccountAddress, ICollection<string> derivativeContractAddresses)
        {
            _owner = owner;
            _client = client;
            _derivative = derivativeContract;
            _referenceAccountAddress = referenceAccountAddress;
            _derivativeAddresses = derivativeContractAddresses;
        }

        public Task Start() {
            return Task.Run(() =>
            {
                foreach (var contractAddress in _derivativeAddresses)
                {
                    Task.Run(() => Listen(contractAddress));
                }
            });
        }

        private async Task Listen(string address) {
            Console.WriteLine($"Start listening for events on {address}");
            var contract = _client.GetContract(_derivative.Abi, address);
            var feePaidEvent = contract.GetEvent("FeePaid");
            var getAll = await feePaidEvent.CreateFilterAsync();
            while (true)
            {
                await Task.Delay(5000);
                Console.WriteLine("Ping");
                var fees = await feePaidEvent.GetFilterChanges<FeePaidEvent>(getAll);
                foreach (var fee in fees) {
                    var sender = fee.Event.Sender;
                    Console.WriteLine(sender);
                    var feePaidInFull = await FeePaidInFull(address, sender);
                    if (feePaidInFull)
                    {
                        Console.WriteLine("Fee paid");
                        var accountIsValid = await AccountContractIsValid(sender);
                        if (accountIsValid) {
                            Console.WriteLine("Account is valid");
                            await ConfirmAccount(address, sender);
                        }
                    }
                }
            }
        }

        private async Task<bool> FeePaidInFull(string derivativeAddress, string accountAddress) {
            var contract = _client.GetContract(_derivative.Abi, derivativeAddress);
            var feeFunction = contract.GetFunction("fee");
            var feePaidFunction = contract.GetFunction("fees");
            var fee = await feeFunction.CallAsync<uint>();
            var feePaid = await feePaidFunction.CallAsync<uint>(accountAddress);
            return feePaid >= fee;
        }

        private async Task<bool> AccountContractIsValid(string address)
        {
            string referenceCode = await _client.GetContractCode(_referenceAccountAddress);
            string code = await _client.GetContractCode(address);
            return code.Equals(referenceCode);
        }

        private async Task ConfirmAccount(string derivativeAddress, string accountAddress) {
            try
            {
                var contract = _client.GetContract(_derivative.Abi, derivativeAddress);
                var setVerifiedFunction = contract.GetFunction("setVerified");
                var gas = new HexBigInteger(50000);
                var s = await setVerifiedFunction.SendTransactionAsync(_owner, gas, null, null, accountAddress, true);
                Console.WriteLine(s);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
