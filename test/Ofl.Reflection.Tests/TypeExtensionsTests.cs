using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ofl.Reflection.Tests
{
    public class TypeExtensionsTests
    {
        private class Test
        {
            internal object StaticInternalReadOnly { get; }
            internal object StaticInternalWriteOnly { set { } }

            private object StaticPrivateReadOnly { get; }
            private object StaticPrivateWriteOnly { set { } }

            internal object InternalReadOnly { get; }
            internal object InternalWriteOnly { set { } }

            private object PrivateReadOnly { get; }
            private object PrivateWriteOnly { set { } }

            public static object StaticReadOnly { get; }
            public static object StaticWriteOnly { set { } }

            public object ReadOnly { get; }
            public object WriteOnly { set { }}
        }

        [Fact]
        public void Test_GetPropertiesWithPublicInstanceGetters()
        {
            // Get the gettable properties.
            IEnumerable<PropertyInfo> properties = typeof(Test).GetPropertiesWithPublicInstanceGetters();

            // Get the single item.
            PropertyInfo property = properties.Single();

            // Same as read only.
            Assert.Equal(typeof(Test).GetProperty(nameof(Test.ReadOnly)), property);
        }

        [Fact]
        public void Test_GetPropertiesWithPublicInstanceSetters()
        {
            // Get the gettable properties.
            IEnumerable<PropertyInfo> properties = typeof(Test).GetPropertiesWithPublicInstanceSetters();

            // Get the single item.
            PropertyInfo property = properties.Single();

            // Same as read only.
            Assert.Equal(typeof(Test).GetProperty(nameof(Test.WriteOnly)), property);
        }
    }
}
