namespace Common.Log
{
    public static class LogExtensions
    {
        /// <summary>
        /// Creates component scoped log
        /// </summary>
        /// <remarks>
        /// You can use it when you need to specify the same component for group of log writes.
        /// If you specify component in the particular log write, it will be concatenated with <paramref name="component"/>
        /// </remarks>
        /// <param name="log">Log to wrap</param>
        /// <param name="component">Component name for which scope will be created</param>
        /// <returns></returns>
        public static ILog CreateComponentScope(this ILog log, string component)
        {
            return new LogComponentScope(component, log);
        }
    }
}