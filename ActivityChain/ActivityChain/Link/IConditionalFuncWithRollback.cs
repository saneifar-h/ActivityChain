namespace ActivityChain.Link
{
    public interface IConditionalFuncWithRollback<T> : IConditionalFunc<T>, IActivityWithRollback<T>
        where T : ISourceItem
    {
    }
}