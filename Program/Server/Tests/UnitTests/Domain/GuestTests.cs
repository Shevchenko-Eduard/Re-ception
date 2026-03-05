using Domain;

namespace UnitTests.Domain;

public class GuestTests
{
    [Theory]
    [InlineData("", "", "", "", "", "", 0)]
    [InlineData("123456789012345678902345678901234567890123456789012345678901", "0", "0", "0", "0", "0", 0)]
    [InlineData("0", "123456789012345678901234567890123456789012345678901", "0", "0", "0", "0", 0)]
    [InlineData("0", "0", "123456789012345678901234567890123456789012345678901", "0", "0", "0", 0)]
    [InlineData("0", "0", "0", "123456789012345678901234567890123456789012345678901", "0", "0", 0)]
    [InlineData("0", "0", "0", "0", "123456789012345678901234567890123456789012345678901", "0", 0)]
    [InlineData("0", "0", "0", "0", "0", "123456789012345678901234567890123456789012345678901", 0)]
    [InlineData("0", "0", "0", "0", "0", "0", 1)]
    public void Guest_New_ThrowException(
        string firstName, 
        string lastName, 
        string patronymic,
        string nickname,
        string phone,
        string email,
        int daysAddFromToday)
    {
        Assert.Throws<ArgumentException>(() => new Guest(
            firstName: firstName,
            lastName: lastName,
            patronymic: patronymic,
            dateOfBirth: DateTime.Now.AddDays(daysAddFromToday),
            nickname: nickname,
            phone: phone,
            email: email
        ));
    }
    [Fact]
    public void Guest_New_ReturnTrueDate()
    {
        string firstName = "x";
        DateTime dateOfBirth = new (2000, 1, 1);
        Guest guest = new(
            firstName: firstName,
            dateOfBirth: dateOfBirth
        );
        Assert.True(guest.CreateAt.Second == DateTime.Now.Second);
    }
}