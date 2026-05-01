using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Application.UseCases.PaymentUseCases;

public class DeletePaymentUseCase(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork) : IAction<PaymentDTOs.Delete>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(PaymentDTOs.Delete input)
    {
        await _paymentRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
