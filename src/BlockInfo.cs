using Nethereum.BlockchainProcessing.BlockStorage.Entities.Mapping;
using Nethereum.Hex.HexTypes;
using Nethereum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProDerivatives.Ethereum
{
    public class BlockInfo
    {
        public long BlockNumber { get; private set; }
        public DateTime Timestamp { get; private set; }

        public BlockInfo(HexBigInteger blockNumber, HexBigInteger timestamp)
        {
            BlockNumber = blockNumber.ToLong();
            Timestamp = new DateTime(timestamp.ToLong() * 1000 * 10000 + new DateTime(1970, 1, 1).Ticks);
        }

        public BlockInfo(HexBigInteger blockNumber, DateTime timestamp)
        {
            BlockNumber = blockNumber.ToLong();
            Timestamp = timestamp;
        }
    }
}
