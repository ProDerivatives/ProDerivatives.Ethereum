﻿using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace ProDerivatives.Ethereum
{
    public class Web3Client : IEthereumClient
    {
        private Web3 _web3;

        public Web3Client(string endpointAddress)
        {
            _web3 = new Web3(endpointAddress);
        }

        public Web3Client(string endpointAddress, string privateKey)
        {
            _web3 = new Web3(new Account(privateKey), endpointAddress);
        }

        public async Task<string[]> GetAccounts()
        {
            var accounts = await _web3.Eth.Accounts.SendRequestAsync();
            return accounts;
        }

        public async Task<string> GetContractCode(string contractAddress)
        {
            var contract = await _web3.Eth.GetCode.SendRequestAsync(contractAddress).ConfigureAwait(true);
            return contract;
        }

        public Contract GetContract(string abi, string contractAddress) {
            return _web3.Eth.GetContract(abi, contractAddress);
        }

        public async Task<string> Sign(string signerAddress, string message)
        {
            var converter = new Nethereum.Hex.HexConvertors.HexUTF8StringConvertor();
            var signature = await _web3.Eth.Sign.SendRequestAsync(signerAddress, converter.ConvertToHex(message));
            return signature;
        }

    }

}
