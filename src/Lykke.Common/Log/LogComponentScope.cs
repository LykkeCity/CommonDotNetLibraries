using System;
using System.Threading.Tasks;

namespace Common.Log
{
    internal class LogComponentScope : ILog
    {
        private readonly string _component;
        private readonly ILog _impl;

        public LogComponentScope(string component, ILog impl)
        {
            _component = component;
            _impl = impl;
        }

        public Task WriteInfoAsync(string component, string process, string context, string info, DateTime? dateTime = null)
        {
            return _impl.WriteInfoAsync($"{_component}:{component}", process, context, info, dateTime);
        }

        public Task WriteWarningAsync(string component, string process, string context, string info, DateTime? dateTime = null)
        {
            return _impl.WriteWarningAsync($"{_component}:{component}", process, context, info, dateTime);
        }

        public Task WriteErrorAsync(string component, string process, string context, Exception exception, DateTime? dateTime = null)
        {
            return _impl.WriteErrorAsync($"{_component}:{component}", process, context, exception, dateTime);
        }

        public Task WriteFatalErrorAsync(string component, string process, string context, Exception exception,
            DateTime? dateTime = null)
        {
            return _impl.WriteFatalErrorAsync($"{_component}:{component}", process, context, exception, dateTime);
        }

        public Task WriteInfoAsync(string process, string context, string info, DateTime? dateTime = null)
        {
            return _impl.WriteInfoAsync(_component, process, context, info, dateTime);
        }

        public Task WriteWarningAsync(string process, string context, string info, DateTime? dateTime = null)
        {
            return _impl.WriteWarningAsync(_component, process, context, info, dateTime);
        }

        public Task WriteErrorAsync(string process, string context, Exception exception, DateTime? dateTime = null)
        {
            return _impl.WriteErrorAsync(_component, process, context, exception, dateTime);
        }

        public Task WriteFatalErrorAsync(string process, string context, Exception exception, DateTime? dateTime = null)
        {
            return _impl.WriteFatalErrorAsync(_component, process, context, exception, dateTime);
        }
    }
}