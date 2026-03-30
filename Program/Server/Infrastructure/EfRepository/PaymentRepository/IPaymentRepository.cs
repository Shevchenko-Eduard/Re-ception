using System.Linq.Expressions;
using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentRepository : IPaymentRepository
{
    public Task AddAsync(Payment entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Payment>> FindAsync(Expression<Func<Payment, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Payment>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> GetByIdAsync(uint id)
    {
        throw new NotImplementedException();
    }
}