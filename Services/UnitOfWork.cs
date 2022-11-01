using Contracts.RepositoryInterfaces;
using Repositories;
using Entities.Data;
using Microsoft.Extensions.Logging;

namespace Contracts.Infraestructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;

        public IPersonRepository Person { get; private set; }
        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Person = new PersonRepository(_context,_logger);
        }
        public async Task<bool> CompleteAsync()
        {
            return (await _context.SaveChangesAsync() >= 0); // 1 = Sucessful | 0 = Failed
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
