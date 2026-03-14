using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.PaymentRepository;

public interface IPaymentMethodRepository : IBaseStatusObjectRepository<PaymentMethod>
{

}