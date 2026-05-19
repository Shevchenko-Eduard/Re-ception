using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.HotelTagUseCases;
using Domain.Interfaces.Repositories.HotelRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HotelTagController(
    IHotelTagRepository hotelTagRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IHotelTagRepository _hotelTagRepository = hotelTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    [Authorize(Roles = "HotelTag-Create")]
    public async Task<IActionResult> Create([FromBody] HotelTagDTOs.Create request)
    {
        var useCase = new CreateHotelTagUseCase(_unitOfWork, _hotelTagRepository);
        var tag = await useCase.Execute(request);
        return Ok(tag);
    }

    [HttpPut]
    [Authorize(Roles = "HotelTag-Update")]
    public async Task<IActionResult> Update([FromBody] HotelTagDTOs.Update request)
    {
        var useCase = new UpdateHotelTagUseCase(_hotelTagRepository, _unitOfWork);
        var tag = await useCase.Execute(request);
        return Ok(tag);
    }

    [HttpDelete]
    [Authorize(Roles = "HotelTag-Delete")]
    public async Task<IActionResult> Delete(HotelTagDTOs.Delete request)
    {
        var useCase = new DeleteHotelTagUseCase(_hotelTagRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
