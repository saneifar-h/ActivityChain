using System;

namespace ActivityChain.Link
{
    internal class MicroConditionalAct<T> : IConditionalAct<T> where T : ISourceItem
    {
        private readonly Action<T> _action;
        private readonly Func<IContext<T>, bool> _condition;
        protected IContext<T> Context;

        public MicroConditionalAct(Func<IContext<T>, bool> condition, Action<T> action)
        {
            _condition = condition;
            _action = action;
        }

        public bool Condition(IContext<T> ctx)
        {
            Context = ctx;
            return _condition(Context);
        }

        public void Execute(IContext<T> ctx)
        {
            Context = ctx;
            _action(Context.SourceObject);
        }
    }
}