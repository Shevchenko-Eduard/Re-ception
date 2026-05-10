using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomImageUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class RoomImageController(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomImageDTOs.Create request)
    {
        var useCase = new CreateRoomImageUseCase(_roomImageRepository, _unitOfWork);
        var image = await useCase.Execute(request);
        return Ok(image);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoomImageDTOs.Update request)
    {
        var useCase = new UpdateRoomImageUseCase(_roomImageRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(RoomImageDTOs.Delete request)
    {
        var useCase = new DeleteRoomImageUseCase(_roomImageRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
