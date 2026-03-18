using Domain.Entity.User.Role;

namespace UnitTests.Domain
{
    public class RoleTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        [InlineData(0, 10)]
        [InlineData(51)]
        [InlineData(10, 101)]
        public void Role_New_ThrowException(int nameLength, int? descriptionLength = null)
        {
            string name = StringTests.Length(nameLength);
            string? description = StringTests.Length(descriptionLength);
            Assert.Throws<ArgumentException>(() => new Role(name, description));
        }
        [Theory]
        [InlineData(10)]
        [InlineData(10, 10)]
        public void Role_New_ReturnTrue(int nameLength, int? descriptionLength = null)
        {
            string name = StringTests.Length(nameLength);
            string? description = StringTests.Length(descriptionLength);
            Role role = new(name, description);
            bool isTrueValue = true;
            if (name != role.Name)
            {
                isTrueValue = false;
            }
            if (description is not null && description != role.Description)
            {
                isTrueValue = false;
            }
            Assert.True(isTrueValue);
        }
    }
}