using Microsoft.Extensions.DependencyInjection;


namespace Domain.Interfaces
{
    public interface IDatabaseImplementation
    {
        void AddDbContext();
    }
}
