using System;

namespace ActivityChain
{
    public class FuncResult
    {
        public FuncResult(Type resultType, object value)
        {
            Value = value;
            ResultType = resultType;
        }

        public object Value { get; }

        public Type ResultType { get; }

        public T As<T>()
        {
            return (T) Value;
        }
    }
}