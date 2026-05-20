using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.HotelImageUseCases;
using Domain.Interfaces.Repositories.HotelRepository;
using HotChocolate.Types.Composite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.WebApi;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HotelImageController(
    IHotelImageRepository hotelImageRepository,
    IS3HotelImageRepository s3HotelImageRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IHotelImageRepository _hotelImageRepository = hotelImageRepository;
    private readonly IS3HotelImageRepository _s3HotelImageRepository = s3HotelImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpPost]
    [Authorize(Roles = "HotelImage-Create")]
    public async Task<IActionResult> Create(IFormFile file, [FromBody] HotelImageDTOs.Request.Create request)
    {
        var useCase = new CreateImageUseCase(
            _hotelImageRepository,
            _s3HotelImageRepository,
            _unitOfWork);

        HotelImageDTOs.Inner.Create innerRequest = new(
            HotelId: request.HotelId,
            Extension: Path.GetExtension(file.FileName),
            ContentType: file.ContentType,
            Stream: file.OpenReadStream()
        );

        var image = await useCase.Execute(innerRequest);
        return Ok(image);
    }

    [HttpPut]
    [Authorize(Roles = "HotelImage-Update")]
    public async Task<IActionResult> Update(IFormFile file, [FromBody] HotelImageDTOs.Request.Update request)
    {
        var useCase = new UpdateImageUseCase(
            _hotelImageRepository,
            _s3HotelImageRepository,
            _unitOfWork);

        HotelImageDTOs.Inner.Update innerRequest = new(
            Id: request.Id,
            Extension: Path.GetExtension(file.FileName),
            ContentType: file.ContentType,
            Stream: file.OpenReadStream()
        );
        await useCase.Execute(innerRequest);
        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "HotelImage-Delete")]
    public async Task<IActionResult> Delete(HotelImageDTOs.Request.Delete request)
    {
        var useCase = new DeleteImageUseCase(
            _hotelImageRepository,
            _unitOfWork,
            _s3HotelImageRepository);

        await useCase.Execute(request);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Read(HotelImageDTOs.Request.Read request)
    {
        var useCase = new ReadImageUseCase(
            _hotelImageRepository,
            _s3HotelImageRepository);

        var image = await useCase.Ask(request);
        return File(image.Stream, image.ContentType, image.FileName);
    }
}
