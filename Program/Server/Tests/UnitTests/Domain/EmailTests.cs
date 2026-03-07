using Domain;
using Domain.Entity;

namespace UnitTests.Domain;

public class EmailTests
{
    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData("@")]
    [InlineData(".")]
    [InlineData("1234567890")]
    [InlineData("qwertyuiop")]
    [InlineData("@.")]
    [InlineData("qwertyuiop@.com")]
    [InlineData("qwertyuiop@email.")]
    [InlineData("@email.com")]
    [InlineData("@#$5@#$%^&*(>>??><<MNM<>>..)")]
    public void Email_New_ThrowsException(string emailString)
    {
        Assert.Throws<ArgumentException>(() => new Email(emailString));
    }
    [Theory]
    [InlineData("qwertyuiop@email.com")]
    [InlineData("1234567890@1234567890.123")]
    public void Email_New_ReturnValue(string emailString)
    {
        Email email = new (emailString);
        Assert.Equal(email.Value, emailString);
    }
    [Theory]
    [InlineData("qwertyuiop@email.com")]
    [InlineData("1234567890@1234567890.123")]
    public void Email_ToString_ReturnValue(string emailString)
    {
        Email email = new (emailString);
        Assert.Equal(email.ToString(), emailString);
    }
}