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
            internal static object StaticInternalReadOnly { get; }
            internal static object StaticInternalWriteOnly { set { } }

            private static object StaticPrivateReadOnly { get; }
            private static object StaticPrivateWriteOnly { set { } }

            internal object InternalReadOnly { get; }
            internal object InternalWriteOnly { set { } }

            private object PrivateReadOnly { get; }
            private object PrivateWriteOnly { set { } }

            public static object StaticReadOnly { get; }
            public static object StaticWriteOnly { set { } }

            public object ReadOnly { get; }
            public object WriteOnly { set { }}
        }

        private class DerivedTest : Test
        {
            public object ExtendedReadOnly { get; }
            public object ExtendedWriteOnly { set { } }
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

        [Fact]
        public void Test_GetAllPropertiesWithPublicInstanceGetters()
        {
            // Get the gettable properties.
            IReadOnlyCollection<PropertyInfo> properties = typeof(DerivedTest).GetPropertiesWithPublicInstanceGetters().
                ToList();

            // Make sure only two properties.
            Assert.Equal(2, properties.Count);
            Assert.Equal(typeof(DerivedTest).GetProperty(nameof(DerivedTest.ReadOnly)), properties.Single(p => p.Name == nameof(DerivedTest.ReadOnly)));
            Assert.Equal(typeof(DerivedTest).GetProperty(nameof(DerivedTest.ExtendedReadOnly)), properties.Single(p => p.Name == nameof(DerivedTest.ExtendedReadOnly)));
        }

        [Fact]
        public void Test_GetAllPropertiesWithPublicInstanceSetters()
        {
            // Get the gettable properties.
            IReadOnlyCollection<PropertyInfo> properties = typeof(DerivedTest).GetPropertiesWithPublicInstanceSetters().
                ToList();

            // Make sure only two properties.
            Assert.Equal(2, properties.Count);
            Assert.Equal(typeof(DerivedTest).GetProperty(nameof(DerivedTest.WriteOnly)), properties.Single(p => p.Name == nameof(DerivedTest.WriteOnly)));
            Assert.Equal(typeof(DerivedTest).GetProperty(nameof(DerivedTest.ExtendedWriteOnly)), properties.Single(p => p.Name == nameof(DerivedTest.ExtendedWriteOnly)));
        }
    }
}
