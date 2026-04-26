using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class UpdateTagUseCase(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IAction<TagDTOs.Update>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(TagDTOs.Update input)
    {
        Tag tag = await _tagRepository.GetByIdAsync(input.Id) ?? throw new ArgumentException();
        Tag updatedTag = input.GetUpdateHotelTag(tag);
        await _tagRepository.UpdateAsync(updatedTag);
        await _unitOfWork.SaveChangesAsync();
    }
}
