using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProD.Integration.Blockchain.Ethereum;

namespace ProDerivatives.Ethereum.Test
{
    [TestClass]
    public class EthereumTests
    {
        private Web3Client _ethereumClient;

        [TestInitialize]
        public void Init() {
            _ethereumClient = new Web3Client("http://localhost:8545");
        }


        [TestMethod]
        public async Task AccountTest()
        {
            var accounts = await _ethereumClient.GetAccounts();
            Assert.AreEqual(2, accounts.Length);
        }

        [TestCategory("Live1")]
        [TestMethod]
        public async Task CodeTest()
        {
            var code = await _ethereumClient.GetContractCode("0x2717a910f459ab238c6b37a994543cc2efcbc7ac");
            Console.WriteLine(code);
            Assert.IsTrue(code.Length > 1000);
        }

        [TestCategory("Test")]
        [TestMethod]
        public void SignatureTest() {
            var signer = new Signer();
            var message = "Some data";

            var hash = $"0x{signer.Hash(message)}";
            Console.WriteLine($"Hash: {hash}");
            var s1 = signer.Sign("0xb4c05079c8e78bd4b92fdf16e9de65235c704e1abfd157f214246eb3dca3c2a7", signer.ConvertToByteArray(hash));
            Console.WriteLine($"S1: {s1}");
                                             
            var signedMessage = signer.Sign("0xb4c05079c8e78bd4b92fdf16e9de65235c704e1abfd157f214246eb3dca3c2a7", message);
            Console.WriteLine(signedMessage);
            var valid = signer.IsValid("0xf35a9f84e7bdceb3ac31da2c5841df7ecfc7b267", signedMessage, message);
            Console.WriteLine(valid);
        }

    }
}
