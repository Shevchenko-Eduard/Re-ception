using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Payment;
using Domain.Interfaces.Repositories.PaymentRepository;

namespace Application.UseCases.PaymentUseCases;

public class CreatePaymentUseCase(

    IUnitOfWork unitOfWork,
    IPaymentRepository paymentRepository) : IAction<PaymentDTOs.Create>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(PaymentDTOs.Create input)
    {
        Payment payment = input.GetPayment();
        await _paymentRepository.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }
}
