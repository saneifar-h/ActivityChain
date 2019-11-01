namespace ActivityChain.Link
{
    public interface IActWithRollback<T> : IAct<T>, IActivityWithRollback<T>
        where T : ISourceItem
    {
    }
}