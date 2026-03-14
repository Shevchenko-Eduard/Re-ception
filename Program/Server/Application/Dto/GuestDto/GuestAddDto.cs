using Application.Interfaces;
using Domain.Entity;
using Domain.Entity.Guest;

namespace Application.Dto.GuestDto;

public sealed class GuestAddDto
{
    public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
    public string? PhoneValue { get; init; }
    public string EmailValue { get; init; } = null!;
    public DateOnly DateOfBirth { get; init; }
    public string Password { get; init; } = null!;
    public int? GenderId { get; init; }
    public Guest ToEntity(IHasher hasher)
    {
        return new Guest(
            firstName: FirstName,
            email: new Email(EmailValue),
            passwordHash: hasher.Hash(Password),
            dateOfBirth: DateOfBirth,
            lastName: LastName,
            phone: PhoneValue is not null ? new Phone(PhoneValue) : null,
            genderId: GenderId);
    }
}