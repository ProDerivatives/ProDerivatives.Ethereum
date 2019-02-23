using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProDerivatives.Ethereum.Test
{
    [TestClass]
    public class EthereumTests
    {
        private Web3Client _ethereumClient;

        [TestInitialize]
        public void Init() {
            // Run against Kovan
            _ethereumClient = new Web3Client("https://kovan.infura.io/v3/61aa366bce0d408196dbe974dd1e642b");
        }

        [TestCategory("Live")]
        [TestMethod]
        public async Task CodeTest()
        {
            var code = await _ethereumClient.GetContractCode("0xe1773195B12627bd5fC5C2ebC0e094E28e9f564c");
            Console.WriteLine(code);
            Assert.IsTrue(code.Length > 1000);
        }

        [TestCategory("Mock")]
        [TestMethod]
        public void SignatureTest() {
            var signer = new Signer();
            var message = "Some data";

            var hash = $"0x{signer.Hash(message)}";
            Console.WriteLine($"Hash: {hash}");
            var s1 = signer.Sign("0xb4c05079c8e78bd4b92fdf16e9de65235c704e1abfd157f214246eb3dca3c2a7", signer.ConvertToByteArray(hash));
            Console.WriteLine($"S1: {s1}");
                                             
            var signedMessage = signer.EncodeUTF8AndSign("0xb4c05079c8e78bd4b92fdf16e9de65235c704e1abfd157f214246eb3dca3c2a7", message);
            Console.WriteLine(signedMessage);
            var valid = signer.IsValid("0xf35a9f84e7bdceb3ac31da2c5841df7ecfc7b267", signedMessage, message);
            Console.WriteLine(valid);
        }

    }
}
