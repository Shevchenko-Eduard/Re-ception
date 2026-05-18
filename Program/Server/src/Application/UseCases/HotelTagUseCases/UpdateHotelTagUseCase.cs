using Application.DTOs;
using Application.Exception;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class UpdateHotelTagUseCase(
    IHotelTagRepository tagRepository,
    IUnitOfWork unitOfWork) : IAction<HotelTagDTOs.Update, HotelTag>
{
    private readonly IHotelTagRepository _tagRepository = tagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<HotelTag> Execute(HotelTagDTOs.Update input)
    {
        HotelTag tag = await _tagRepository.GetByIdAsync(input.Id) 
            ?? throw new ApplicationExternalException();
        HotelTag updatedTag = input.GetUpdateHotelTag(tag);
        await _tagRepository.UpdateAsync(updatedTag);
        await _unitOfWork.SaveChangesAsync();
        return updatedTag;
    }
}
