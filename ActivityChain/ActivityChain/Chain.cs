using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityChain.Link;

namespace ActivityChain
{
    public class Chain<TIn> where TIn : ISourceItem
    {
        private Context<TIn> _chainContext;

        public Chain(ILink<TIn> initLink)
        {
            InitialLink = initLink;
        }

        public ILink<TIn> InitialLink { get; }

        public IEnumerable<LinkExecutionInfo<TIn>> ExecutionInfos => _chainContext.ExecutionInfos;

        public Task Execute(TIn sourceItem)
        {
            _chainContext = new Context<TIn>(sourceItem);
            return InitialLink.Execute(_chainContext);
        }

        public void ExecuteSync(TIn sourceItem)
        {
            _chainContext = new Context<TIn>(sourceItem);
            InitialLink.ExecuteSync(_chainContext);
        }
    }
}