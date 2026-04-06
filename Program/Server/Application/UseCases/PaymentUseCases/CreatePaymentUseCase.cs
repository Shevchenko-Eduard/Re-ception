using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Payment;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Application.UseCases.PaymentUseCases;

public class CreatePaymentUseCase(
    IAuthorization authorization,
    IUnitOfWork unitOfWork,
    IPaymentRepository paymentRepository) : IUseCase<PaymentDto.Create>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Payment, PermissionFlag.Self);

    public async Task Execute(PaymentDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        Payment payment = input.GetPayment();
        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }
}
