using System;
using System.Threading.Tasks;
using Nethereum.Contracts;

namespace ProDerivatives.Ethereum
{
    public interface IEthereumClient
    {
        Task<string[]> GetAccounts();

        Task<string> GetContractCode(string contractAddress);

        Contract GetContract(string abi, string contractAddress);

        Task<string> Sign(string signerAddress, string message);
    }
}
