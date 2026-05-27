using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomRoomTagUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoomRoomTagController(
    IRoomRoomTagRepository roomRoomTagRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IRoomRoomTagRepository _roomRoomTagRepository = roomRoomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost(Name = "RoomRoomTagCreate")]
    [Authorize(Roles = "RoomRoomTag-Create")]
    public async Task<IActionResult> Create([FromBody] RoomRoomTagDTOs.Create request)
    {
        var useCase = new AddRoomRoomTagUseCase(_roomRoomTagRepository, _unitOfWork);
        var roomTag = await useCase.Execute(request);
        return Ok(roomTag);
    }

    [HttpDelete(Name = "RoomRoomTagDelete")]
    [Authorize(Roles = "RoomRoomTag-Delete")]
    public async Task<IActionResult> Delete(RoomRoomTagDTOs.Delete request)
    {
        var useCase = new DeleteRoomRoomTagUseCase(_roomRoomTagRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
