using System;

namespace ActivityChain
{
    public class ChainExecuteException : Exception
    {
        public ChainExecuteException(string message) : base(message)
        {
        }
    }
}