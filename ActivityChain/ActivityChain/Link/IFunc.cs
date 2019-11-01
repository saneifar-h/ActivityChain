namespace ActivityChain.Link
{
    public interface IFunc<TIn> : IActivity<TIn> where TIn : ISourceItem
    {
        object Execute(IContext<TIn> ctx);
    }
}