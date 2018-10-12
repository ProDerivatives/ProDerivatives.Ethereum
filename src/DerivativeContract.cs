using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProDerivatives.Ethereum
{
    public class DerivativeContract : IContract
    {
        private string _filePath;

        public DerivativeContract(string filePath)
        {
            _filePath = filePath;
        }

        public string Abi => GetFromKey("abi");

        public string ByteCode => GetFromKey("bytecode");

        private string GetFromKey(string key)
        {
            using (StreamReader file = File.OpenText(_filePath))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    return o[key].ToString();
                }
            }
        }

    }
}
