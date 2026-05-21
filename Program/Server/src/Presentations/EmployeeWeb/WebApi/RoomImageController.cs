using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.RoomImageUseCases;
using Domain.Interfaces.Repositories.RoomRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
public class RoomImageController(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork,
    IS3RoomImageRepository s3RoomImageRepository) : ControllerBase
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IS3RoomImageRepository _s3RoomImageRepository = s3RoomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    [Authorize(Roles = "RoomImage-Create")]
    public async Task<IActionResult> Create(IFormFile file, [FromForm] RoomImageDTOs.Request.Create request)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не выбран или пустой.");
        }

        var useCase = new CreateRoomImageUseCase(
            _roomImageRepository,
            _unitOfWork,
            _s3RoomImageRepository);

        RoomImageDTOs.Inner.Create innerRequest = new(
            RoomId: request.RoomId,
            Extension: Path.GetExtension(file.FileName),
            ContentType: file.ContentType,
            Stream: file.OpenReadStream()
        );

        var image = await useCase.Execute(innerRequest);
        return Ok(image);
    }

    [HttpPut]
    [Authorize(Roles = "RoomImage-Update")]
    public async Task<IActionResult> Update(IFormFile file, [FromForm] RoomImageDTOs.Request.Update request)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не выбран или пустой.");
        }

        var useCase = new UpdateRoomImageUseCase(
            _roomImageRepository,
            _s3RoomImageRepository,
            _unitOfWork);

        RoomImageDTOs.Inner.Update innerRequest = new(
            Id: request.Id,
            Extension: Path.GetExtension(file.FileName),
            ContentType: file.ContentType,
            Stream: file.OpenReadStream()
        );

        await useCase.Execute(innerRequest);
        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "RoomImage-Delete")]
    public async Task<IActionResult> Delete(RoomImageDTOs.Request.Delete request)
    {
        var useCase = new DeleteRoomImageUseCase(
            _roomImageRepository,
            _s3RoomImageRepository,
            _unitOfWork);

        await useCase.Execute(request);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read(int id)
    {
        RoomImageDTOs.Request.Read request = new(Id: id);
        var useCase = new ReadRoomImageUseCase(
            _roomImageRepository,
            _s3RoomImageRepository);

        var image = await useCase.Ask(request);
        return File(image.Stream, image.ContentType, image.FileName);
    }
}
