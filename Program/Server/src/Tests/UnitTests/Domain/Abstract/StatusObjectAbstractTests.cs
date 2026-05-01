using Domain.Abstract;

namespace UnitTests.Domain.Abstract;

public class StatusObjectAbstractTests
{
    private class Test : StatusObjectAbstract<Test>
    {
        private Test(string name): base(name){}
        public readonly static Test Test1 = new(nameof(Test1));
        public readonly static Test Test2 = new(nameof(Test2));
        public readonly static Test Test3 = new(nameof(Test3));
    }
    [Fact]
    public void StatusObjectAbstract_FromId_ReturnValue()
    {
        Assert.Equal(Test.FromId(0), Test.Test1);
        Assert.Equal(Test.FromId(1), Test.Test2);
        Assert.Equal(Test.FromId(2), Test.Test3);
    }
    [Fact]
    public void StatusObjectAbstract_FromName_ReturnValue()
    {
        Assert.Equal(Test.FromName(nameof(Test.Test1)), Test.Test1);
        Assert.Equal(Test.FromName(nameof(Test.Test2)), Test.Test2);
        Assert.Equal(Test.FromName(nameof(Test.Test3)), Test.Test3);
    }
}