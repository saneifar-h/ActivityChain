using System;

namespace ActivityChain.Link
{
    internal class MicroFunc<T, TOut> : IFunc<T> where T : ISourceItem
    {
        private readonly Func<T, TOut> _function;
        protected IContext<T> Context;

        public MicroFunc(Func<T, TOut> function)
        {
            _function = function;
        }

        object IFunc<T>.Execute(IContext<T> ctx)
        {
            return _function(ctx.SourceObject);
        }

        public TOut Execute(IContext<T> context)
        {
            Context = context;
            return (TOut) ((IFunc<T>) this).Execute(context);
        }
    }
}