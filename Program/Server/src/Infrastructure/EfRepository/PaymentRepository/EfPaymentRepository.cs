using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentRepository(ProgramContext context) : IPaymentRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(Payment entity) => await _context.Payments.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.Payments.Where(p => p.Id == id).ExecuteDeleteAsync();

    public async Task<Payment?> GetByIdAsync(int id) => await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);

}