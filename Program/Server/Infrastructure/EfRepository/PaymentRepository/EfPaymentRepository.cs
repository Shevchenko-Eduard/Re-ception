using Domain.Entity.Payment;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.PaymentRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentRepository(ProgramContext context) : IPaymentRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(Payment entity)
    {
        await _context.Payments.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Payment, bool>> predicate)
    {
        return await _context.Payments.CountAsync(predicate);
    }

    public async Task DeleteAsync(uint id)
    {
        await _context.Payments.Where(p => p.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Payment, bool>> predicate)
    {
        return await _context.Payments.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Payment>?> FindAsync(Expression<Func<Payment, bool>> specification)
    {
        return await _context.Payments.Where(specification).ToListAsync();
    }

    public async Task<Payment?> FirstAsync(Expression<Func<Payment, bool>> predicate)
    {
        return await _context.Payments.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(uint id)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
    }
}