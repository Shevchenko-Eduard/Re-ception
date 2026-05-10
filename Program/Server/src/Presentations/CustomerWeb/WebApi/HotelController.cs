using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.HotelUseCases;
using Domain.Interfaces.Repositories.HotelRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class HotelController(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelDTOs.Create request)
    {
        var useCase = new CreateHotelUseCase(_hotelRepository, _unitOfWork);
        var hotel = await useCase.Execute(request);
        return Ok(hotel);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] HotelDTOs.Update request)
    {
        var useCase = new UpdateHotelUseCase(_hotelRepository, _unitOfWork);
        var hotel = await useCase.Execute(request);
        return Ok(hotel);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(HotelDTOs.Delete request)
    {
        var useCase = new DeleteHotelUseCase(_hotelRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
