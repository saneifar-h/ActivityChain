using System;

namespace ActivityChain.Link
{
    public class LinkExecutionInfo<TIn> where TIn : ISourceItem
    {
        public LinkExecutionInfo(ILink<TIn> link)
        {
            Link = link;
        }

        public ILink<TIn> Link { get; }

        public double? ExecutionTime => StartExecuteTime.HasValue && EndOfExecutionTime.HasValue
            ? (double?) (EndOfExecutionTime.Value - StartExecuteTime.Value).TotalMilliseconds
            : null;

        public DateTime? StartExecuteTime { get; set; }
        public DateTime? EndOfExecutionTime { get; set; }
        public Exception Exception { get; set; }
        public bool IsSuccess => Exception == null;
    }
}