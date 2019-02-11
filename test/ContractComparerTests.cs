using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProDerivatives.Ethereum;

namespace EthereumTests
{
    [TestClass]
    public class ContractComparerTests
    {

        private JObject LoadContractFile(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    return o;
                }
            }
        }

        [TestCategory("Mock")]
        [TestMethod]
        public void CompareCompatibleContracts()
        {
            var contractFile = LoadContractFile("Contracts/Account.json");
            var contract1 = contractFile["abi"].ToString();
            var contract2 = contractFile["abi"].ToString();

            var contractComparer = new ContractComparer(contract1, contract2);
            var contractsAreEqual = contractComparer.IsInterfaceImplemented();
            Assert.IsTrue(contractsAreEqual);
        }

        [TestCategory("Mock")]
        [TestMethod]
        public void CompareIncompatibleContracts()
        {
            var contract1File = LoadContractFile("Contracts/Account.json");
            var contract2File = LoadContractFile("Contracts/Account1.json");
            var contract1 = contract1File["abi"].ToString();
            var contract2 = contract2File["abi"].ToString();

            var contractComparer = new ContractComparer(contract1, contract2);
            var contractsAreEqual = contractComparer.IsInterfaceImplemented();
            Assert.IsFalse(contractsAreEqual);
        }

    }
}
