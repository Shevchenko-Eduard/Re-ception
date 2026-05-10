using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomRoomTagUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class RoomRoomTagController(
    IRoomRoomTagRepository roomRoomTagRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IRoomRoomTagRepository _roomRoomTagRepository = roomRoomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RoomRoomTagDTOs.Create request)
    {
        var useCase = new AddRoomRoomTagUseCase(_roomRoomTagRepository, _unitOfWork);
        var roomTag = await useCase.Execute(request);
        return Ok(roomTag);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RoomRoomTagDTOs.Delete request)
    {
        var useCase = new DeleteRoomRoomTagUseCase(_roomRoomTagRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
