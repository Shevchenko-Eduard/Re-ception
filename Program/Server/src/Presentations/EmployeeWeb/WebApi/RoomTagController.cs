using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomTagUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class RoomTagController(
    IRoomTagRepository roomTagRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomTagDTOs.Create request)
    {
        var useCase = new CreateRoomTagUseCase(_unitOfWork, _roomTagRepository);
        var tag = await useCase.Execute(request);
        return Ok(tag);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoomTagDTOs.Update request)
    {
        var useCase = new UpdateRoomTagUseCase(_roomTagRepository, _unitOfWork);
        var tag = await useCase.Execute(request);
        return Ok(tag);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(RoomTagDTOs.Delete request)
    {
        var useCase = new DeleteRoomTagUseCase(_roomTagRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
