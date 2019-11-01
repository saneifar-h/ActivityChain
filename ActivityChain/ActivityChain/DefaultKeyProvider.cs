using System;
using System.Collections.Generic;
using ActivityChain.Link;

namespace ActivityChain
{
    public class DefaultKeyProvider<TChainObj> : IKeyProvider<TChainObj>
        where TChainObj : ISourceItem
    {
        private readonly Dictionary<Type, string> _dicOfTypes;

        public DefaultKeyProvider()
        {
            _dicOfTypes = new Dictionary<Type, string>();
        }

        public string GetKey<TFunction>() where TFunction : IFunc<TChainObj>
        {
            if (!_dicOfTypes.ContainsKey(typeof(TFunction)))
                throw new KeyNotFoundException();
            return _dicOfTypes[typeof(TFunction)];
        }

        public string CreateKey<TFunction>() where TFunction : IFunc<TChainObj>
        {
            var key = Guid.NewGuid().ToString();
            if (_dicOfTypes.ContainsKey(typeof(TFunction)))
            {
                _dicOfTypes[typeof(TFunction)] = key;
                return key;
            }

            _dicOfTypes.Add(typeof(TFunction), key);
            return key;
        }
    }
}