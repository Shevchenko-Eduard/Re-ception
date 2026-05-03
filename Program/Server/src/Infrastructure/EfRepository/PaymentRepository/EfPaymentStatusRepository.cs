using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentStatusRepository(ProgramContext context) : IPaymentStatusRepository
{
    private readonly ProgramContext _context = context;

    public async Task<PaymentStatus?> GetByIdAsync(int id) => await _context.PaymentStatuses.FirstOrDefaultAsync(ps => ps.Id == id);

    public async Task<PaymentStatus?> GetByNameAsync(string name) => await _context.PaymentStatuses.FirstOrDefaultAsync(ps => ps.Name == name);

    public IQueryable<PaymentStatus> GetQueryable() => _context.PaymentStatuses.AsQueryable();
}