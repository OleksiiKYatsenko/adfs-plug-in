using ADFS_Plug_in.Interfaces;
using System.Collections.Concurrent;

namespace ADFS_Plug_in.Pipeline
{
    internal class AuthPipeline<TPipeIn, TPipeOut> : IPipeline<TPipeIn, TPipeOut>
    {
        internal interface IPipelineStep<TStepIn>
        {
            BlockingCollection<Item<TStepIn>> Buffer { get; set; }
        }

        internal class AuthPipelineStep<TStepIn, TStepOut> : IPipelineStep<TStepIn>
        {
            public BlockingCollection<Item<TStepIn>> Buffer { get; set; } = new BlockingCollection<Item<TStepIn>>();
            public Func<TStepIn, TStepOut> StepAction { get; set; }
        }

        internal class Item<T>
        {
            public T Input { get; set; }
            public TaskCompletionSource<TPipeOut> TaskCompletionSource { get; set; }
        }

        List<object> _pipelineSteps = new List<object>();

        public AuthPipeline(Func<TPipeIn, AuthPipeline<TPipeIn, TPipeOut>, TPipeOut> steps)
        {
            steps.Invoke(default(TPipeIn), this);//Invoke just once to build blocking collections
        }
        public Task<TPipeOut> Execute(TPipeIn input)
        {
            var first = _pipelineSteps[0] as IPipelineStep<TPipeIn>;
            TaskCompletionSource<TPipeOut> tsk = new TaskCompletionSource<TPipeOut>();
            first.Buffer.Add(/*input*/new Item<TPipeIn>()
            {
                Input = input,
                TaskCompletionSource = tsk
            });
            return tsk.Task;
        }

        public AuthPipelineStep<TStepIn, TStepOut> GenerateStep<TStepIn, TStepOut>()
        {
            var pipelineStep = new AuthPipelineStep<TStepIn, TStepOut>();
            var stepIndex = _pipelineSteps.Count;

            Task.Run(() =>
            {
                IPipelineStep<TStepOut> nextPipelineStep = null;

                foreach (var input in pipelineStep.Buffer.GetConsumingEnumerable())
                {
                    bool isLastStep = stepIndex == _pipelineSteps.Count - 1;
                    TStepOut output;
                    try
                    {
                        output = pipelineStep.StepAction(input.Input);
                    }
                    catch (Exception e)
                    {
                        input.TaskCompletionSource.SetException(e);
                        continue;
                    }
                    if (isLastStep)
                    {
                        input.TaskCompletionSource.SetResult((TPipeOut)(object)output);
                    }
                    else
                    {
                        nextPipelineStep = nextPipelineStep ?? (isLastStep ? null : _pipelineSteps[stepIndex + 1] as IPipelineStep<TStepOut>);
                        nextPipelineStep.Buffer.Add(new Item<TStepOut>() { Input = output, TaskCompletionSource = input.TaskCompletionSource });
                    }
                }
            });

            _pipelineSteps.Add(pipelineStep);
            return pipelineStep;

        }
    }
}
