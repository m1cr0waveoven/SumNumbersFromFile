namespace SumNumbersFromFile
{
    internal static class FunctionalExtensions
    {
        public static TOut MapDefaultIfException<TIn, TOut, TException>(this TIn @in, Func<TIn, TOut> func) where TException : Exception
        {
            try
            {
                return func(@in);
            }
            catch (TException)
            {
                return default;
            }
        }
    }
}
