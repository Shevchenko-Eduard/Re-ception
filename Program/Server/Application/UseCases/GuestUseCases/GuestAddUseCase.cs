using Application.Dto.GuestDto;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entity.Guest;

namespace Application.UseCases.GuestUseCases;

public sealed class GuestAddUseCase
{
    private readonly IGuestRepository _repository;
    private readonly IHasher _hasher;
    private readonly GuestAddDto _guestAddDto;
    private readonly Guest _guest;
    public GuestAddUseCase(
        IGuestRepository repository,
        IHasher hasher,
        GuestAddDto guestAddDto)
    {
        _repository = repository;
        _hasher = hasher;
        _guestAddDto = guestAddDto;
        _guest = guestAddDto.ToEntity(_hasher);
    }
    public async Task ExecuteAsync()
    {
        await _repository.AddAsync(_guest);
    }
}