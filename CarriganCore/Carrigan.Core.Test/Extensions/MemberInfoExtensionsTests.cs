using System;
using System.Reflection;
using Xunit;
using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class MemberInfoExtensionsTests
{
    private class SampleClass
    {
        public int Property { get; set; }
        public void Method() { }
        public int Field;
    }

    [Fact]
    public void GetQualifiedName_ReturnsFullName_ForType()
    {
        Type type = typeof(SampleClass);

        string result = type.GetQualifiedName();

        string expected = "Carrigan.Core.Test.Extensions.MemberInfoExtensionsTests+SampleClass";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetQualifiedName_ReturnsQualifiedName_ForProperty()
    {
        PropertyInfo property = typeof(SampleClass).GetProperty("Property")!;

        string result = property.GetQualifiedName();

        string expected = "Carrigan.Core.Test.Extensions.MemberInfoExtensionsTests+SampleClass.Property";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetQualifiedName_ReturnsQualifiedName_ForField()
    {
        FieldInfo field = typeof(SampleClass).GetField("Field")!;

        string result = field.GetQualifiedName();

        string expected = "Carrigan.Core.Test.Extensions.MemberInfoExtensionsTests+SampleClass.Field";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetQualifiedName_ReturnsQualifiedName_ForMethod()
    {
        MethodInfo method = typeof(SampleClass).GetMethod("Method")!;

        string result = method.GetQualifiedName();

        string expected = "Carrigan.Core.Test.Extensions.MemberInfoExtensionsTests+SampleClass.Method";
        Assert.Equal(expected, result);
    }
}
