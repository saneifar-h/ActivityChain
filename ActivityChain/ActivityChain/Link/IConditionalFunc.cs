namespace ActivityChain.Link
{
    public interface IConditionalFunc<T> : IFunc<T>, IConditionalActivity<T>
        where T : ISourceItem
    {
    }
}