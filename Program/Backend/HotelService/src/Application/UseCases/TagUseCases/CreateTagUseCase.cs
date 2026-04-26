using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class CreateTagUseCase(
    IUnitOfWork unitOfWork,
    ITagRepository tagRepository) : IAction<TagDTOs.Create>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(TagDTOs.Create input)
    {
        Tag tag = input.GetTag();
        await _tagRepository.AddAsync(tag);
        await _unitOfWork.SaveChangesAsync();
    }
}
