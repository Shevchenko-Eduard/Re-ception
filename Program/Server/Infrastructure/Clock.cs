using Domain.Interfaces;

namespace Infrastructure;

public class Clock : IClock
{
    DateTimeOffset IClock.Now => Now();
    public DateTimeOffset Now() => DateTimeOffset.Now;
}