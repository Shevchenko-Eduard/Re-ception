using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.HotelImageUseCases;
using Domain.Interfaces.Repositories.HotelRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class HotelImageController(
    IHotelImageRepository hotelImageRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IHotelImageRepository _hotelImageRepository = hotelImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelImageDTOs.Create request)
    {
        var useCase = new CreateImageUseCase(_hotelImageRepository, _unitOfWork);
        var image = await useCase.Execute(request);
        return Ok(image);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] HotelImageDTOs.Update request)
    {
        var useCase = new UpdateImageUseCase(_hotelImageRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(HotelImageDTOs.Delete request)
    {
        var useCase = new DeleteImageUseCase(_hotelImageRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
