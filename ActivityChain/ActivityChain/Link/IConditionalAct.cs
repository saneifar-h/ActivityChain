namespace ActivityChain.Link
{
    public interface IConditionalAct<T> : IAct<T>, IConditionalActivity<T> where T : ISourceItem
    {
    }
}