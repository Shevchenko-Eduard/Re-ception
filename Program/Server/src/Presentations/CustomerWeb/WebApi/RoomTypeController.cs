using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomTypeUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class RoomTypeController(
    IRoomTypeRepository roomTypeRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomTypeDTOs.Create request)
    {
        var useCase = new CreateRoomTypeUseCase(_unitOfWork, _roomTypeRepository);
        var roomType = await useCase.Execute(request);
        return Ok(roomType);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoomTypeDTOs.Update request)
    {
        var useCase = new UpdateRoomTypeUseCase(_roomTypeRepository, _unitOfWork);
        var roomType = await useCase.Execute(request);
        return Ok(roomType);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(RoomTypeDTOs.Delete request)
    {
        var useCase = new DeleteRoomTypeUseCase(_roomTypeRepository, _unitOfWork);
        await useCase.Execute(request);
        return NoContent();
    }
}
