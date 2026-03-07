using Domain.Entity.Guest;

namespace UnitTests.Domain;

public class GuestTests
{
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(51, 0, 0, 0, 0)]
    [InlineData(0, 51, 0, 0, 0)]
    [InlineData(0, 0, 51, 0, 0)]
    [InlineData(0, 0, 0, 51, 0)]
    [InlineData(0, 0, 0, 0, 1)]
    public void Guest_New_ThrowException(
        int firstNameLength, 
        int lastNameLength, 
        int patronymicLength,
        int nicknameLength,
        int daysAddFromToday)
    {
        Assert.Throws<ArgumentException>(() => new Guest(
            firstName: StringTests.Length(firstNameLength),
            lastName: StringTests.Length(lastNameLength),
            patronymic: StringTests.Length(patronymicLength),
            dateOfBirth: DateTime.Now.AddDays(daysAddFromToday),
            nickname: StringTests.Length(nicknameLength),
            passwordHash: GetHashCode().ToString()));
    }
    [Fact]
    public void Guest_New_ReturnTrueDate()
    {
        string firstName = "x";
        DateTime dateOfBirth = new (2000, 1, 1);
        Guest guest = new(
            firstName: firstName,
            dateOfBirth: dateOfBirth,
            passwordHash: GetHashCode().ToString()
        );
        Assert.True(guest.CreateAt.Hour == DateTime.Now.Hour);
    }
}