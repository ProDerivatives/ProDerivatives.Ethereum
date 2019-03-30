using System;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ProDerivatives.Ethereum
{
    [Event("TradeCleared")]
    public class TradeClearedEvent
    {
        [Parameter("address", "longAccount", 1, false)]
        public string LongAccount { get; set; }

        [Parameter("address", "shortAccount", 2, false)]
        public string ShortAccount { get; set; }

        [Parameter("int32", "notional", 3, false)]
        public Int32 Notional { get; set; }

        [Parameter("int64", "xp", 4, false)]
        public Int64 Price { get; set; }
    }
}
