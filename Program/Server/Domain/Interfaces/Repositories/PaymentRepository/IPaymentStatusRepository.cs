using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.PaymentRepository;

public interface IPaymentStatusRepository: IBaseStatusObjectRepository<PaymentStatus>
{
    
}