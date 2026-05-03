using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.PaymentRepository;

public class EfPaymentMethodRepository(ProgramContext context) : IPaymentMethodRepository
{
    private readonly ProgramContext _context = context;

    public async Task<PaymentMethod?> GetByIdAsync(int id) => await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == id);

    public async Task<PaymentMethod?> GetByNameAsync(string name) => await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Name == name);

    public IQueryable<PaymentMethod> GetQueryable() => _context.PaymentMethods.AsQueryable();
}