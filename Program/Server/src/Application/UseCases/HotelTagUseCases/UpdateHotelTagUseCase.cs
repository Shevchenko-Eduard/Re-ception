using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class UpdateTagUseCase(
    IHotelTagRepository tagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelTagDTOs.Update>
{
    private readonly IHotelTagRepository _tagRepository = tagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(HotelTagDTOs.Update input)
    {
        HotelTag tag = await _tagRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        HotelTag updatedTag = input.GetUpdateHotelTag(tag);
        await _tagRepository.UpdateAsync(updatedTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
