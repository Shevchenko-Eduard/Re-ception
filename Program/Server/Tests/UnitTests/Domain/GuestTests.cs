using Domain.Entity;
using Domain.Entity.Guest;

namespace UnitTests.Domain;

public class GuestTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(51, 0, 0)]
    [InlineData(0, 51, 0)]
    [InlineData(0, 0, 1)]
    public void Guest_New_ThrowException(
        int firstNameLength, 
        int lastNameLength, 
        int daysAddFromToday)
    {
        Assert.Throws<ArgumentException>(() => new Guest(
            firstName: StringTests.Length(firstNameLength),
            email: new ("name@email.domen"),
            lastName: StringTests.Length(lastNameLength),
            dateOfBirth: DateTime.Now.AddDays(daysAddFromToday),
            passwordHash: GetHashCode().ToString()));
    }
    [Fact]
    public void Guest_New_ReturnTrueDate()
    {
        string firstName = "x";
        DateTime dateOfBirth = new (2000, 1, 1);
        Guest guest = new(
            firstName: firstName,
            email: new ("name@email.domen"),
            dateOfBirth: dateOfBirth,
            passwordHash: GetHashCode().ToString()
        );
        Assert.True(guest.CreateAt.Hour == DateTime.Now.Hour);
    }
}