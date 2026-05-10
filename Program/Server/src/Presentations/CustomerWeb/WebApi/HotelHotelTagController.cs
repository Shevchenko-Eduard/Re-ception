using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.HotelHotelTagUseCases;
using Domain.Interfaces.Repositories.HotelRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class HotelHotelTagController(
    IHotelHotelTagRepository hotelHotelTagRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IHotelHotelTagRepository _hotelHotelTagRepository = hotelHotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelHotelTagDTOs.Create request)
    {
        var useCase = new CreateHotelHotelTagUseCase(_hotelHotelTagRepository, _unitOfWork);
        var hotelTag = await useCase.Execute(request);
        return Ok(hotelTag);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(HotelHotelTagDTOs.Delete request)
    {
        var useCase = new DeleteHotelHotelTagUseCase(_hotelHotelTagRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
