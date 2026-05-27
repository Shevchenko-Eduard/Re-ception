using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.HotelUseCases;
using Domain.Interfaces.Repositories.HotelRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HotelController(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost(Name = "HotelCreate")]
    [Authorize(Roles = "Hotel-Create")]
    public async Task<IActionResult> Create([FromBody] HotelDTOs.Create request)
    {
        var useCase = new CreateHotelUseCase(_hotelRepository, _unitOfWork);
        var hotel = await useCase.Execute(request);
        return Ok(hotel);
    }

    [HttpPut(Name = "HotelUpdate")]
    [Authorize(Roles = "Hotel-Update")]
    public async Task<IActionResult> Update([FromBody] HotelDTOs.Update request)
    {
        var useCase = new UpdateHotelUseCase(_hotelRepository, _unitOfWork);
        var hotel = await useCase.Execute(request);
        return Ok(hotel);
    }

    [HttpDelete(Name = "HotelDelete")]
    [Authorize(Roles = "Hotel-Delete")]
    public async Task<IActionResult> Delete(HotelDTOs.Delete request)
    {
        var useCase = new DeleteHotelUseCase(_hotelRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
