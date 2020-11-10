using System;
using CrudRepository.Data;
using Microsoft.Extensions.Logging;

namespace CrudRepository.Providers
{
    public class MyLogger : ILogger
    {
        protected ErrorService E { get; }

        public MyLogger(ErrorService e) => E = e;

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (E != null)
                E.Add($"{R(logLevel)} {state.ToString()}");
        }

        string R(LogLevel l)
        {
            switch (l)
            {
                case LogLevel.Error:
                    return $"<span class='badge badge-danger'>Error</span>";
                case LogLevel.Information:
                    return $"<span class='badge badge-primary'>Info</span>";
            }

            return "";
        }
    }
}