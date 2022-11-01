using Contracts.RepositoryInterfaces;

namespace Contracts
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }

        Task<bool> CompleteAsync();
        void Dispose();
    }
}
