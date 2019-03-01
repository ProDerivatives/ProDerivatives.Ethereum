using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProDerivatives.Ethereum
{
    public class TradeClearedEvent
    {
        [Parameter("address", "longAccount", 1, false)]
        public string LongAccount { get; set; }

        [Parameter("address", "shortAccount", 2, false)]
        public string ShortAccount { get; set; }

        [Parameter("int", "notional", 3, false)]
        public int Notional { get; set; }

        [Parameter("int", "xp", 4, false)]
        public int Price { get; set; }
    }
}
