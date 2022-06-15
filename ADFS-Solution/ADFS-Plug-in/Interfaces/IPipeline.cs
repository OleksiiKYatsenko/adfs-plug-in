namespace ADFS_Plug_in.Interfaces
{
    internal interface IPipeline<TPipeIn, TPipeOut>
    {
        Task<TPipeOut> Execute(TPipeIn input);
    }
}
