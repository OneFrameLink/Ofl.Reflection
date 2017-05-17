using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ofl.Reflection.Tests
{
    public class PropertyInfoTests
    {
        [Fact]
        public void Test_Exclude()
        {
            // Create a type.
            var test = new { Property1 = "hello", Property2 = 120 };

            // Get the public properties.
            IEnumerable<PropertyInfo> properties = test.GetType().GetPropertiesWithPublicInstanceGetters();

            // Exclude.
            properties = properties.Exclude(test, t => t.Property1, t => t.Property2);

            // Nothing in there.
            Assert.False(properties.Any(), $"There were items in the { nameof(properties) } sequence.");
        }
    }
}
