using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.ReservationUseCases;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.ReservationRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    ICalculatorReservationPrice calculatorReservationPrice) : ControllerBase
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICalculatorReservationPrice _calculatorReservationPrice = calculatorReservationPrice;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationDTOs.Create request)
    {
        var useCase = new CreateReservationUseCase(_reservationRepository, _unitOfWork, _calculatorReservationPrice);
        var reservation = await useCase.Execute(request);
        return Ok(reservation);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ReservationDTOs.Update request)
    {
        var useCase = new UpdateReservationUseCase(_reservationRepository, _unitOfWork);
        var reservation = await useCase.Execute(request);
        return Ok(reservation);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(ReservationDTOs.Delete request)
    {
        var useCase = new DeleteReservationUseCase(_reservationRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
