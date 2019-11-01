using System.Threading.Tasks;

namespace ActivityChain.Link
{
    public interface ILink<TIn> where TIn : ISourceItem
    {
        LinkExecutionInfo<TIn> ExecutionInfo { get; }
        ILink<TIn> WhenSuccessExecute(ILink<TIn> successNode);
        Task Execute(IContext<TIn> ctx);
        void ExecuteSync(IContext<TIn> ctx);
        void RollBack(IContext<TIn> ctx);
    }
}