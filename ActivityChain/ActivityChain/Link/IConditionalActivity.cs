namespace ActivityChain.Link
{
    public interface IConditionalActivity<T> : IActivity<T> where T : ISourceItem
    {
        bool Condition(IContext<T> context);
    }
}