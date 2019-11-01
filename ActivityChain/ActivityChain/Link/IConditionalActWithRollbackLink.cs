namespace ActivityChain.Link
{
    public interface IConditionalActWithRollbackLink<T> : IConditionalAct<T>, IActivityWithRollback<T>
        where T : ISourceItem
    {
    }
}