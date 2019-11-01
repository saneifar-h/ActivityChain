using System.Collections.Generic;
using ActivityChain.Link;

namespace ActivityChain
{
    public interface IContext<TIn> where TIn : ISourceItem
    {
        TIn SourceObject { get; }
        IEnumerable<KeyValuePair<string, FuncResult>> NodeResults { get; }
        IEnumerable<LinkExecutionInfo<TIn>> ExecutionInfos { get; }
        FuncResult GetNodeResult<TFunctionType>() where TFunctionType : IFunc<TIn>;
        void AddResult<TNodeFunction>(object result) where TNodeFunction : IFunc<TIn>;
        void AddGlobalResult(string key, object value);
        object GetGlobalResult(string key);
        void PushSuccessNode(ILink<TIn> successNode);
        void PushExecuteNode(ILink<TIn> executedNode);
        void RollBack();
    }
}