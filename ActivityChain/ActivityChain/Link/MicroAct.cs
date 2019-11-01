using System;

namespace ActivityChain.Link
{
    internal class MicroAct<T> : IAct<T> where T : ISourceItem
    {
        private readonly Action<T> _action;
        protected IContext<T> Context;

        public MicroAct(Action<T> action)
        {
            _action = action;
        }

        public void Execute(IContext<T> context)
        {
            Context = context;
            _action(context.SourceObject);
        }
    }
}