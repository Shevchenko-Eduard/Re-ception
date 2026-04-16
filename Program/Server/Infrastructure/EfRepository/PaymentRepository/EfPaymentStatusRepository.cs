using System.Linq.Expressions;
using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentStatusRepository(ProgramContext context) : IPaymentStatusRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<PaymentStatus?> GetByIdAsync(int id)
    {
        return await _context.PaymentStatuses.FirstOrDefaultAsync(ps => ps.Id == id);
    }

    public async Task<IEnumerable<PaymentStatus>> GetAllAsync()
    {
        return await _context.PaymentStatuses.ToListAsync();
    }

    public async Task<IEnumerable<PaymentStatus>?> FindAsync(Expression<Func<PaymentStatus, bool>> specification)
    {
        return await _context.PaymentStatuses.Where(specification).ToListAsync();
    }

    public async Task<PaymentStatus?> FirstAsync(Expression<Func<PaymentStatus, bool>> predicate)
    {
        return await _context.PaymentStatuses.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<PaymentStatus, bool>> predicate)
    {
        return await _context.PaymentStatuses.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<PaymentStatus, bool>> predicate)
    {
        return await _context.PaymentStatuses.CountAsync(predicate);
    }

    public async Task<PaymentStatus?> GetByNameAsync(string name)
    {
        return await _context.PaymentStatuses.FirstOrDefaultAsync(ps => ps.Name == name);
    }
}