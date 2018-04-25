using Pricing.Models.Abstract;
using Xunit;

namespace Pricing.Tests.Models
{
    public class EntityTests
    {
        private class TestClass : Entity
        {

        }

        [Fact]
        public void InitializesWithId()
        {
            var testClass = new TestClass();

            Assert.NotEmpty(testClass.Id);
        }
    }
}
