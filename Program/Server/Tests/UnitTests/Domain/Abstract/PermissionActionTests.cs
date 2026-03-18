using Domain.Abstract;

namespace UnitTests.Domain;

public class PermissionActionTests
{
    private class Test : StatusObjectAbstract<Test>
    {
        private Test(int id, string name): base(id, name){}
        public static Test Test1 = new(0, nameof(Test1));
        public static Test Test2 = new(1, nameof(Test2));
        public static Test Test3 = new(2, nameof(Test3));
    }
    [Fact]
    public void PermissionAction_FromId_ReturnValue()
    {
        Assert.Equal(Test.FromId(0), Test.Test1);
        Assert.Equal(Test.FromId(1), Test.Test2);
        Assert.Equal(Test.FromId(2), Test.Test3);
    }
    [Fact]
    public void PermissionAction_FromName_ReturnValue()
    {
        Assert.Equal(Test.FromName(nameof(Test.Test1)), Test.Test1);
        Assert.Equal(Test.FromName(nameof(Test.Test2)), Test.Test2);
        Assert.Equal(Test.FromName(nameof(Test.Test3)), Test.Test3);
    }
}