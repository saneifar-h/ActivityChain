using System;

namespace ActivityChain.Link
{
    internal class MicroConditionalFunc<T, TOut> : IConditionalFunc<T> where T : ISourceItem
    {
        private readonly Func<IContext<T>, bool> _condition;
        private readonly Func<T, TOut> _function;
        protected IContext<T> Context;

        public MicroConditionalFunc(Func<IContext<T>, bool> condition, Func<T, TOut> function)
        {
            _condition = condition;
            _function = function;
        }

        object IFunc<T>.Execute(IContext<T> ctx)
        {
            Context = ctx;
            return _function(ctx.SourceObject);
        }

        public bool Condition(IContext<T> context)
        {
            Context = context;
            return _condition(context);
        }

        public TOut Execute(IContext<T> context)
        {
            Context = context;
            return (TOut) ((IFunc<T>) this).Execute(context);
        }
    }
}