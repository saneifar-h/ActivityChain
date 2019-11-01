using System;

namespace ActivityChain.Link
{
    public class LinkFactory<T> where T : ISourceItem
    {
        private LinkFactory()
        {
        }

        public static Link<T> Create(Action<T> action)
        {
            return new Link<T>(new MicroAct<T>(action));
        }

        public static Link<T> Create(Func<IContext<T>, bool> condition, Action<T> action)
        {
            return new Link<T>(new MicroConditionalAct<T>(condition, action));
        }

        public static Link<T> Create<TOut>(Func<IContext<T>, bool> condition, Func<T, TOut> function)
        {
            return new Link<T>(new MicroConditionalFunc<T, TOut>(condition, function));
        }

        public static Link<T> Create<TOut>(Func<T, TOut> function)
        {
            return new Link<T>(new MicroFunc<T, TOut>(function));
        }
    }
}