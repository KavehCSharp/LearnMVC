using CrudRepository.Data;
using Microsoft.Extensions.Logging;

namespace CrudRepository.Providers
{
    public class MyProvider : ILoggerProvider
    {
        protected ErrorService E { get; }

        public MyProvider(ErrorService e) => E = e;

        public ILogger CreateLogger(string categoryName) => new MyLogger(E);

        public void Dispose()
        {
        }
    }
}