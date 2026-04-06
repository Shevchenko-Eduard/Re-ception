using System.Linq.Expressions;
using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentStatusRepository : IPaymentStatusRepository
{
    public Task<int> CountAsync(Expression<Func<PaymentStatus, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<PaymentStatus, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentStatus>?> FindAsync(Expression<Func<PaymentStatus, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentStatus?> FirstAsync(Expression<Func<PaymentStatus, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentStatus>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PaymentStatus?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentStatus?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}