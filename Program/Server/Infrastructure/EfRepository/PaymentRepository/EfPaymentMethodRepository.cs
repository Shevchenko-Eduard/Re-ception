using System.Linq.Expressions;
using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentMethodRepository(ProgramContext context) : IPaymentMethodRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<PaymentMethod?> GetByIdAsync(int id)
    {
        return await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == id);
    }

    public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
    {
        return await _context.PaymentMethods.ToListAsync();
    }

    public async Task<IEnumerable<PaymentMethod>?> FindAsync(Expression<Func<PaymentMethod, bool>> specification)
    {
        return await _context.PaymentMethods.Where(specification).ToListAsync();
    }

    public async Task<PaymentMethod?> FirstAsync(Expression<Func<PaymentMethod, bool>> predicate)
    {
        return await _context.PaymentMethods.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<PaymentMethod, bool>> predicate)
    {
        return await _context.PaymentMethods.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<PaymentMethod, bool>> predicate)
    {
        return await _context.PaymentMethods.CountAsync(predicate);
    }

    public async Task<PaymentMethod?> GetByNameAsync(string name)
    {
        return await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Name == name);
    }
}