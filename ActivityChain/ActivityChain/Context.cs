using System.Collections.Generic;
using System.Linq;
using ActivityChain.Link;

namespace ActivityChain
{
    public class Context<TIn> : IContext<TIn> where TIn : ISourceItem
    {
        private readonly List<LinkExecutionInfo<TIn>> _executionInfo;
        private readonly Dictionary<string, object> _globalResult;
        private readonly Dictionary<string, FuncResult> _results;

        public Context(TIn sourceObject) : this(sourceObject, null)
        {
        }

        public Context(TIn sourceObject, IKeyProvider<TIn> keyProvider)
        {
            SourceObject = sourceObject;
            SuccessorNodesStack = new Stack<ILink<TIn>>();
            ExecutedNodesStack = new Stack<ILink<TIn>>();
            _results = new Dictionary<string, FuncResult>();
            _globalResult = new Dictionary<string, object>();
            KeyProvider = keyProvider ?? new DefaultKeyProvider<TIn>();
            _executionInfo = new List<LinkExecutionInfo<TIn>>();
        }

        public IKeyProvider<TIn> KeyProvider { get; }
        public Stack<ILink<TIn>> SuccessorNodesStack { get; }
        public Stack<ILink<TIn>> ExecutedNodesStack { get; }
        public IEnumerable<LinkExecutionInfo<TIn>> ExecutionInfos => _executionInfo;

        public TIn SourceObject { get; }

        public FuncResult GetNodeResult<TFunctionType>() where TFunctionType : IFunc<TIn>
        {
            var key = KeyProvider.GetKey<TFunctionType>();
            return !_results.ContainsKey(key) ? null : _results[key];
        }

        public IEnumerable<KeyValuePair<string, FuncResult>> NodeResults => _results;

        public void AddResult<TNodeFunction>(object result) where TNodeFunction : IFunc<TIn>
        {
            var key = KeyProvider.CreateKey<TNodeFunction>();
            if (_results.ContainsKey(key))
            {
                _results[key] = new FuncResult(result.GetType(), result);
                return;
            }

            _results.Add(KeyProvider.CreateKey<TNodeFunction>(), new FuncResult(result.GetType(), result));
        }

        public void AddGlobalResult(string key, object value)
        {
            if (_globalResult.ContainsKey(key))
            {
                _globalResult[key] = value;
                return;
            }

            _globalResult.Add(key, value);
        }

        public object GetGlobalResult(string key)
        {
            return !_globalResult.ContainsKey(key) ? null : _globalResult[key];
        }

        public void PushSuccessNode(ILink<TIn> successNode)
        {
            SuccessorNodesStack.Push(successNode);
        }

        public void PushExecuteNode(ILink<TIn> executedNode)
        {
            _executionInfo.Add(executedNode.ExecutionInfo);
            ExecutedNodesStack.Push(executedNode);
        }

        public void RollBack()
        {
            while (ExecutedNodesStack.Any())
            {
                var popItem = ExecutedNodesStack.Pop();
                popItem.RollBack(this);
            }
        }
    }
}