namespace ActivityChain.Link
{
    public interface IFuncWithRollback<T> : IFunc<T>, IActivityWithRollback<T>
        where T : ISourceItem
    {
    }
}