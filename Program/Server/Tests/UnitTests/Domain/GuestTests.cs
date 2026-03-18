using Domain.Entity.Guest;
using Domain.Interfaces;
using Moq;

namespace UnitTests.Domain;

public class GuestTests
{
    [Fact]
    public void Guest_New_ReturnTrueDate()
    {
        var mockClock = new Mock<IClock>();
        mockClock.SetupGet(c => c.Now).Returns(new DateTimeOffset(2024, 6, 1, 0, 0, 0, TimeSpan.Zero));
        Guest guest = new(
            userId: Guid.NewGuid(),
            clock: mockClock.Object
        );
        Assert.True(guest.CreateAt.Hour == mockClock.Object.Now.Hour);
    }
}