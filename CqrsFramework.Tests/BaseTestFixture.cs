using NUnit.Framework;

namespace CqrsFramework.Tests
{
    public abstract class BaseTestFixture
    {
        [SetUp]
        protected void Setup()
        {
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();
    }
}