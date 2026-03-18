using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.PaymentRepository;

public interface IPaymentRepository :
    IBaseCreateRepository<Payment>,
    IBaseReadRepository<Payment, uint>,
    IBaseDeleteRepository<Payment, uint>
{

}