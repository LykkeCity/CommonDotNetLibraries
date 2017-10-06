namespace Common.Log
{
    public static class LogExtensions
    {
        public static ILog CreateComponentScope(this ILog log, string component)
        {
            return new LogComponentScope(component, log);
        }
    }
}