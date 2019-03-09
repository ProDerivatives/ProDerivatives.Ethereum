using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.JsonRpc.Client;

namespace ProDerivatives.Ethereum.Test
{
    [TestClass]
    public class EthereumTests
    {
        private Web3Client _ethereumClient;

        private const string _accountContractAbi = "[\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"operators\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x13e7c9d8\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [],\r\n      \"name\": \"owner\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x8da5cb5b\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"derivatives\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xc0046e39\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"constructor\",\r\n      \"signature\": \"constructor\"\r\n    },\r\n    {\r\n      \"payable\": true,\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"fallback\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [],\r\n      \"name\": \"destroy\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x83197ef0\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"newOwner\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"transferOwnership\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xf2fde38b\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"operatorAddress\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"addOperator\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x9870d7fe\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"operatorAddress\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"removeOperator\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xac8a584a\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"contractAddress\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"addDerivative\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xc002475a\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"destination\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"name\": \"amount\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"transfer\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xa9059cbb\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"derivative\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"name\": \"recipientAccount\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"name\": \"notional\",\r\n          \"type\": \"int32\"\r\n        }\r\n      ],\r\n      \"name\": \"transferNotional\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xfdefe0c9\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"derivative\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"getRequiredFee\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"uint64\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xd0d5de61\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"contractAddress\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"registerDerivativeAndPayFees\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x6d9eec5c\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [],\r\n      \"name\": \"getDerivatives\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"address[]\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x327731fb\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"derivative\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"getCollateralRequirement\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"int128\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x4dfe06b6\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [],\r\n      \"name\": \"getTotalAllocated\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"int128\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x82299a5c\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [],\r\n      \"name\": \"getTotalAvailable\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x69674ec9\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [],\r\n      \"name\": \"closeOut\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x543fc62a\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"destination\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"name\": \"amountToSend\",\r\n          \"type\": \"int128\"\r\n        }\r\n      ],\r\n      \"name\": \"settle\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x1f46b84e\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"derivative\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"name\": \"notional\",\r\n          \"type\": \"int32\"\r\n        },\r\n        {\r\n          \"name\": \"xp\",\r\n          \"type\": \"int64\"\r\n        }\r\n      ],\r\n      \"name\": \"goLong\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xefca94bb\"\r\n    },\r\n    {\r\n      \"constant\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"name\": \"derivative\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"name\": \"notional\",\r\n          \"type\": \"int32\"\r\n        },\r\n        {\r\n          \"name\": \"xp\",\r\n          \"type\": \"int64\"\r\n        }\r\n      ],\r\n      \"name\": \"goShort\",\r\n      \"outputs\": [],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0x5d4e22da\"\r\n    },\r\n    {\r\n      \"constant\": true,\r\n      \"inputs\": [],\r\n      \"name\": \"isInUse\",\r\n      \"outputs\": [\r\n        {\r\n          \"name\": \"\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"payable\": false,\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\",\r\n      \"signature\": \"0xc9ba8e20\"\r\n    }\r\n  ]";

        [TestInitialize]
        public void Init()
        {
            // Run against Kovan
            _ethereumClient = new Web3Client("https://eth-ropsten.proderivatives.com", "52a9fff08e03e298ce95dee54441dd00b81393b60634ccf3bb557716c3071e21");  /* new Web3Client("http://localhost:8545"); */
        }

        [TestCategory("Live")]
        [TestMethod]
        public async Task CodeTest()
        {
            var code = await _ethereumClient.GetContractCode("0xc1720360cf7688b807865757dc8a7f3bf8bbc72e");
            Console.WriteLine(code);
            Assert.IsTrue(code.Length > 1000);
        }

        [TestCategory("Mock")]
        [TestMethod]
        public void SignatureTest1()
        {
            var signer = new Signer();
            var message = "Some data";

            var hash = $"0x{signer.Hash(message)}";
            Console.WriteLine($"Hash: {hash}");
            var s1 = signer.Sign("52a9fff08e03e298ce95dee54441dd00b81393b60634ccf3bb557716c3071e21", signer.ConvertToByteArray(hash));
            Console.WriteLine($"S1: {s1}");

            var s2 = signer.EncodeUTF8AndSign("52a9fff08e03e298ce95dee54441dd00b81393b60634ccf3bb557716c3071e21", message);
            Console.WriteLine($"S2: {s2}");

            // Assert.Equals(s1, s2);

            //Assert.IsTrue(signer.IsValid("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", s1, message));
            Assert.IsTrue(signer.IsValid("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", s2, message));
        }

        [TestCategory("Mock")]
        [TestMethod]
        public void SignatureTest2()
        {
            var signer = new Signer();
            var message = "1550954444168|GET|/accounts|";

            var s1 = signer.EncodeUTF8AndSign("52a9fff08e03e298ce95dee54441dd00b81393b60634ccf3bb557716c3071e21", message);
            Console.WriteLine($"S1: {s1}");

            var s2 = "0x417e703cae6f9bc67932ae8c80e58955d8abb2e98789f3a9913ef70de639bb3f316af5406af32e8a1470916a1b3c3e0600c73b35ba47313f1e5a5e9dc6c71ab01b";

            Assert.IsTrue(s1 == s2);
        }


        // Only works with unlocked accounts
        /*
        [TestCategory("Live")]
        [TestMethod]
        public async Task SignatureTest3()
        {
            var message = "1550954444168|GET|/accounts|";
            var s1 = await _ethereumClient.Sign("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", message);

            var s2 = "0x59d4622330b5cdb383d0e649cc6228ca85477d27608fe7a858e4ba86cd3598b453b5199b2301f499b163b9fd17d8eeedd948b36cd3e57f0312f26f41aaa145ee1b";
            Console.WriteLine(s1);

            Assert.IsTrue(s1 == s2);
        }
        */

        [TestCategory("Live")]
        [TestMethod]
        public async Task PrivilegedFunctionTest3()
        {
            _ethereumClient.SetDefaultGas(50000);
            var contract = _ethereumClient.GetContract(_accountContractAbi, "0xc1720360cf7688b807865757dc8a7f3bf8bbc72e");
            var addOperator = contract.GetFunction("addOperator");
            //var gas = new Nethereum.Hex.HexTypes.HexBigInteger(50000);
            //var a = await addOperator.SendTransactionAsync("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", gas, null, null, "0x59a11ed570c0b963d75cd176dcc4e0abc2584858");
            var a = await addOperator.SendTransactionAsync("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", "0x59a11ed570c0b963d75cd176dcc4e0abc2584858");

            // Wait for transaction to be mined
            await Task.Delay(30000);

            var operatorsFunction = contract.GetFunction("operators");
            var isOperator = await operatorsFunction.CallAsync<bool>("0x59a11ed570c0b963d75cd176dcc4e0abc2584858");
            Assert.IsTrue(isOperator);

            var removeOperator = contract.GetFunction("removeOperator");
            // var r = await removeOperator.SendTransactionAsync("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", gas, null, null, "0x59a11ed570c0b963d75cd176dcc4e0abc2584858");
            var r = await removeOperator.SendTransactionAsync("0x59a11ed570c0b963d75cd176dcc4e0abc2584858", "0x59a11ed570c0b963d75cd176dcc4e0abc2584858");

            // Wait for transaction to be mined
            await Task.Delay(30000);

            isOperator = await operatorsFunction.CallAsync<bool>("0x59a11ed570c0b963d75cd176dcc4e0abc2584858");
            Assert.IsFalse(isOperator);
        }

    }
}
