using Domain.Abstract;

namespace UnitTests.Domain.Abstract;

public class StatusWithParentsObjectsAbstractTests
{
    private class Test : StatusWithParentsObjectsAbstract<Test>
    {
        private Test(string name, params IEnumerable<Test> tests) : base(name, tests) {}
        public static Test Test1 = new(nameof(Test1));
        public static Test Test2 = new(nameof(Test2), Test1);
        public static Test Test3 = new(nameof(Test3), Test2);
    }

    [Fact]
    public void StatusWithParentsObjectsAbstract_EqualsWithoutNesting_ReturnTrue()
    {
        Assert.True(Test.Test1.Equals(Test.Test1));
    }
    [Fact]
    public void StatusWithParentsObjectsAbstract_EqualsFirstNesting_ReturnTrue()
    {
        Assert.True(Test.Test2 == Test.Test1);
    }
    [Fact]
    public void StatusWithParentsObjectsAbstract_EqualsDoubleNesting_ReturnTrue()
    {
        Assert.True(Test.Test3 == Test.Test1);
    }
    [Fact]
    public void StatusWithParentsObjectsAbstract_EqualsFirstNesting_ReturnFalse()
    {
        Assert.False(Test.Test1 == Test.Test2);
    }
    [Fact]
    public void StatusWithParentsObjectsAbstract_EqualsDoubleNesting_ReturnFalse()
    {
        Assert.False(Test.Test1 == Test.Test3);
    }
}