namespace ActivityChain.Link
{
    public interface IAct<T> : IActivity<T> where T : ISourceItem
    {
        void Execute(IContext<T> context);
    }
}