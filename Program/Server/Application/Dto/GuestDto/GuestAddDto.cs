using Application.Interfaces;
using Domain.Entity;
using Domain.Entity.Guest;

namespace Application.Dto.GuestDto;

public sealed class GuestAddDto
{
    public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
    public string? Patronymic { get; init; }
    public string? Nickname { get; init; }
    public string? PhoneValue { get; init; }
    public string? EmailValue { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Password { get; init; } = null!;
    public int? GenderId { get; init; }
    public Guest ToEntity(IHasher hasher)
    {
        return new Guest(
            firstName: FirstName,
            passwordHash: hasher.Hash(Password),
            dateOfBirth: DateOfBirth,
            lastName: LastName,
            patronymic: Patronymic,
            nickname: Nickname,
            phone: PhoneValue is not null ? new Phone(PhoneValue) : null,
            email: EmailValue is not null ? new Email(EmailValue) : null,
            genderId: GenderId);
    }
}