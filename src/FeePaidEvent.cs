using System;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProDerivatives.Ethereum
{
    [Event("FeePaid")]
    public class FeePaidEvent
    {
        [Parameter("address", "account", 1, false)]
        public string Sender {get; set;}

        [Parameter("uint256", "amount", 2, false)]
        public BigInteger Amount {get; set;}
    }
}
