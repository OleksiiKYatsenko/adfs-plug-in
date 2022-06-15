using ADFS_Plug_in.Pipeline;

namespace ADFS_Plug_in.Extensions
{
    internal static class PipelineExtensions
    {
        public static TOutput Step2<TInput, TOutput, TInputOuter, TOutputOuter>(this TInput inputType,
            AuthPipeline<TInputOuter, TOutputOuter> pipelineBuilder,
            Func<TInput, TOutput> step)
        {
            var pipelineStep = pipelineBuilder.GenerateStep<TInput, TOutput>();
            pipelineStep.StepAction = step;
            return default;
        }
    }
}
