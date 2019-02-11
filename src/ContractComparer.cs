using System;
using System.Collections;
using System.Collections.Generic;
using Nethereum.Contracts;
using System.Linq;
using Nethereum.ABI.Model;

namespace ProDerivatives.Ethereum
{
    public class ContractComparer
    {
        private readonly Contract _reference;
        private readonly Contract _candidate;

        public ContractComparer(Contract reference, Contract candidate)
        {
            _reference = reference;
            _candidate = candidate;
        }


        public bool IsAbiEqual()
        {
            if (_reference == null)
                return _candidate == null;

            if (_candidate == null)
                return false;

            var parameterComparer = new MultiSetComparer<Parameter>();

            var referenceFunctions = _reference.ContractBuilder.ContractABI.Functions;
            foreach (var referenceFunction in referenceFunctions)
            {
                var candidateFunction = _candidate.ContractBuilder.GetFunctionAbi(referenceFunction.Name);
                if (candidateFunction == null)
                    return false;

                if (!parameterComparer.Equals(referenceFunction.InputParameters.ToList(), candidateFunction.InputParameters.ToList()))
                    return false;

                if (!parameterComparer.Equals(referenceFunction.OutputParameters.ToList(), candidateFunction.OutputParameters.ToList()))
                    return false;

            }
            return true;
        }


        public int GetHashCode(Contract obj)
        {
            return obj.GetHashCode();
        }

        
    }
}
