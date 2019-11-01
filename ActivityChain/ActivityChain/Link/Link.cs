using System;
using System.Threading.Tasks;

namespace ActivityChain.Link
{
    public class Link<TIn> : ILink<TIn> where TIn : ISourceItem
    {
        protected ILink<TIn> NextNode;

        public Link(IActivity<TIn> activity)
        {
            if (!(activity is IAct<TIn>) && !(activity is IFunc<TIn>))
                throw new ArgumentException(nameof(IActivity<TIn>));
            Activity = activity;
            ExecutionInfo = new LinkExecutionInfo<TIn>(this);
        }

        public IActivity<TIn> Activity { get; }

        public ILink<TIn> WhenSuccessExecute(ILink<TIn> afterNode)
        {
            NextNode = afterNode;
            return afterNode;
        }

        public Task Execute(IContext<TIn> ctx)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (Activity is IConditionalActivity<TIn> activity)
                        if (!activity.Condition(ctx))
                        {
                            if (NextNode == null) return;
                            await NextNode.Execute(ctx);
                            return;
                        }

                    ExecutionInfo.StartExecuteTime = DateTime.Now;
                    ctx.PushExecuteNode(this);
                    switch (Activity)
                    {
                        case IFunc<TIn> func:
                            ctx.GetType().GetMethod("AddResult")
                                ?.MakeGenericMethod(func.GetType())
                                .Invoke(ctx, new[] {func.Execute(ctx)});
                            break;
                        case IAct<TIn> action:
                            action.Execute(ctx);
                            break;
                    }

                    ctx.PushSuccessNode(this);
                    ExecutionInfo.EndOfExecutionTime = DateTime.Now;
                }
                catch (Exception ex)
                {
                    ExecutionInfo.Exception = ex;
                    ExecutionInfo.EndOfExecutionTime = DateTime.Now;
                    ctx.RollBack();
                    throw;
                }

                if (NextNode != null)
                    await NextNode.Execute(ctx);
            });
        }

        public void ExecuteSync(IContext<TIn> ctx)
        {
            try
            {
                if (Activity is IConditionalActivity<TIn> activity)
                    if (!activity.Condition(ctx))
                    {
                        NextNode?.ExecuteSync(ctx);
                        return;
                    }

                ExecutionInfo.StartExecuteTime = DateTime.Now;
                ctx.PushExecuteNode(this);
                switch (Activity)
                {
                    case IFunc<TIn> func:
                        ctx.GetType().GetMethod("AddResult")
                            ?.MakeGenericMethod(func.GetType())
                            .Invoke(ctx, new[] {func.Execute(ctx)});
                        break;
                    case IAct<TIn> action:
                        action.Execute(ctx);
                        break;
                }

                ctx.PushSuccessNode(this);
                ExecutionInfo.EndOfExecutionTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                ExecutionInfo.Exception = ex;
                ExecutionInfo.EndOfExecutionTime = DateTime.Now;
                ctx.RollBack();
                throw;
            }

            NextNode?.ExecuteSync(ctx);
        }


        public void RollBack(IContext<TIn> ctx)
        {
            if (Activity is IActivityWithRollback<TIn> rollBackableActivity)
                rollBackableActivity.RollBack(ctx);
        }

        public LinkExecutionInfo<TIn> ExecutionInfo { get; }
    }
}