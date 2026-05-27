using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.PaymentUseCases;
using Domain.Interfaces.Repositories.PaymentRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController(
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost(Name = "PaymentCreate")]
    [Authorize(Roles = "Payment-Create")]
    public async Task<IActionResult> Create([FromBody] PaymentDTOs.Create request)
    {
        var useCase = new CreatePaymentUseCase(_unitOfWork, _paymentRepository);
        var payment = await useCase.Execute(request);
        return Ok(payment);
    }

    [HttpDelete(Name = "PaymentDelete")]
    [Authorize(Roles = "Payment-Delete")]
    public async Task<IActionResult> Delete(PaymentDTOs.Delete request)
    {
        var useCase = new DeletePaymentUseCase(_paymentRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
