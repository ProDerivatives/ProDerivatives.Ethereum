using System;
namespace ProDerivatives.Ethereum
{
    public static class Extensions
    {
        public static bool Equals(this Nethereum.ABI.Model.Parameter parameter, Nethereum.ABI.Model.Parameter other)
        {
            return (parameter.Name == other.Name) && (parameter.Type == other.Type);
        }
    }
}
