using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Application.UseCases.PaymentUseCases;

public class DeletePaymentUseCase(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork,
    IAuthorization authorization) : IUseCase<PaymentDto.Delete>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Payment, PermissionFlag.Self);

    public async Task Execute(PaymentDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        
        await _paymentRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
