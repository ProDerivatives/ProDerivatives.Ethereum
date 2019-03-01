using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProDerivatives.Ethereum
{
    public class FeePaidEvent
    {
        [Parameter("address", "account", 1, false)]
        public string Sender {get; set;}

        [Parameter("uint", "amount", 2, false)]
        public uint Amount {get; set;}
    }
}
