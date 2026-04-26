using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Application.UseCases.HotelTagUseCases;

public class DeleteTagUseCase(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IAction<TagDTOs.Delete>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(TagDTOs.Delete input)
    {
        await _tagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
