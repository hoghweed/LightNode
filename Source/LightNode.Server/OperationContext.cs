﻿using LightNode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNode.Server
{
    public class OperationContext
    {
        public IDictionary<string, object> Environment { get; private set; }

        public string ContractName { get; private set; }
        public string OperationName { get; private set; }

        public AcceptVerbs Verb { get; private set; }

        // internal use

        internal IContentFormatter ContentFormatter { get; set; }

        internal object[] Parameters { get; set; }

        // Type as typeof(Attribute)
        internal ILookup<Type, Attribute> Attributes { get; set; }

        internal OperationContext(IDictionary<string, object> environment, string contractName, string operationName, AcceptVerbs verb)
        {
            Environment = environment;
            ContractName = contractName;
            OperationName = operationName;
            Verb = verb;
        }

        public bool IsAttributeDefined(Type attributeType)
        {
            return Attributes.Contains(attributeType);
        }

        public bool IsAttributeDefined<T>() where T : Attribute
        {
            return Attributes.Contains(typeof(T));
        }

        public IEnumerable<Attribute> GetAttributes(Type attributeType)
        {
            return Attributes[attributeType];
        }

        public IEnumerable<T> GetAttributes<T>() where T : Attribute
        {
            return Attributes[typeof(T)].Cast<T>();
        }

        public IEnumerable<Attribute> GetAllAttributes()
        {
            return Attributes.SelectMany(xs => xs);
        }
    }
}