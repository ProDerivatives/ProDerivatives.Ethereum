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
        public void CompareContracts()
        {
            var contractFile = LoadContractFile("Contracts/Account.json");
            var contract1 = new Contract(null, contractFile["abi"].ToString(), string.Empty);
            var contract2 = new Contract(null, contractFile["abi"].ToString(), string.Empty);

            var contractComparer = new ContractComparer(contract1, contract2);
            var contractsAreEqual = contractComparer.IsAbiEqual();
            Assert.IsTrue(contractsAreEqual);
        }

    }
}
