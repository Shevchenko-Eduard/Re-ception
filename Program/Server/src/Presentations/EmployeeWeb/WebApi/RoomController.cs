using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class RoomController(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomDTOs.Create request)
    {
        var useCase = new CreateRoomUseCase(_unitOfWork, _roomRepository);
        var room = await useCase.Execute(request);
        return Ok(room);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoomDTOs.Update request)
    {
        var useCase = new UpdateRoomUseCase(_roomRepository, _unitOfWork);
        var room = await useCase.Execute(request);
        return Ok(room);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(RoomDTOs.Delete request)
    {
        var useCase = new DeleteRoomUseCase(_roomRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
