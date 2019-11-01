namespace ActivityChain.Link
{
    public interface IActivityWithRollback<T> : IActivity<T> where T : ISourceItem
    {
        void RollBack(IContext<T> context);
    }
}