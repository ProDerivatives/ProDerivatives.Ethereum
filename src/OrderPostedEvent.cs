using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProDerivatives.Ethereum
{
    [Event("OrderPosted")]
    public class OrderPostedEvent
    {
        [Parameter("address", "account", 1)]
        public string Account { get; set; }

        [Parameter("int32", "notional", 2)]
        public int Notional { get; set; }

        [Parameter("int64", "xp", 3)]
        public int Price { get; set; }
    }
}
