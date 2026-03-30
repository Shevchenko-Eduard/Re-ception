using System.Linq.Expressions;
using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentMethodRepository : IPaymentMethodRepository
{
    public Task<IEnumerable<PaymentMethod>> FindAsync(Expression<Func<PaymentMethod, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentMethod>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PaymentMethod?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentMethod?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}