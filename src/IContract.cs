using System;
namespace ProDerivatives.Ethereum
{
    public interface IContract
    {
        string Abi { get; }

        string ByteCode { get; }
    }
}
