using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ofl.Reflection.Tests
{
    public class ExpressionExtensions
    {
        [Fact]
        public void Test_GetPropertyInfo()
        {
            // Get the test.
            var test = new { Property = "Hello" };

            // Get the actual property info.
            PropertyInfo actual = test.GetPropertyInfo(t => t.Property);

            // Get expected property info.
            PropertyInfo expected = test.GetType().GetProperty(nameof(test.Property));

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetPropertyInfos()
        {
            // Get the test.
            var test = new { Property1 = "hello", Property2 = 2 };

            // Get the actual property info.
            IEnumerable<PropertyInfo> actual = test.GetPropertyInfos(t => t.Property1, t => t.Property2);

            // Get expected property info.
            IEnumerable<PropertyInfo> expected = new[] {
                test.GetType().GetProperty(nameof(test.Property1)),
                test.GetType().GetProperty(nameof(test.Property2)),
            };

            // Assert.
            Assert.True(actual.SequenceEqual(expected), "The sequence of properties do not match.");
        }
    }
}
