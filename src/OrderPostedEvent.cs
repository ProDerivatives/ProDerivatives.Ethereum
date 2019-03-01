using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProDerivatives.Ethereum
{
    public class OrderPostedEvent
    {
        [Parameter("address", "account", 1, false)]
        public string Account { get; set; }

        [Parameter("int", "notional", 2, false)]
        public int Notional { get; set; }

        [Parameter("int", "xp", 3, false)]
        public int Price { get; set; }
    }
}
