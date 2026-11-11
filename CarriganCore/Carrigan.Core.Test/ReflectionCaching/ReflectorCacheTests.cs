using Carrigan.Core.ReflectionCaching;

namespace Carrigan.Core.Test.ReflectionCaching;

public class ReflectorCacheTests
{
    [Fact]
    public void Type_ReturnsGenericTypeArgument() =>
        Assert.Equal(typeof(SampleModel), ReflectorCache<SampleModel>.Type);

    [Fact]
    public void ReadablePublicInstanceProperties_ReturnsOnlyReadableInstanceProperties()
    {
        string[] names = [.. ReflectorCache<SampleModel>.ReadablePublicInstanceProperties.Select(property => property.Name)];

        Assert.Contains(nameof(SampleModel.ReadOnly), names);
        Assert.Contains(nameof(SampleModel.ReadWrite), names);
        Assert.DoesNotContain(nameof(SampleModel.WriteOnly), names);
        Assert.DoesNotContain(nameof(SampleModel.StaticProperty), names);
    }

    [Fact]
    public void WriteablePublicInstanceProperties_ReturnsOnlyWritableInstanceProperties()
    {
        string[] names = [.. ReflectorCache<SampleModel>.WriteablePublicInstanceProperties.Select(property => property.Name)];

        Assert.Contains(nameof(SampleModel.WriteOnly), names);
        Assert.Contains(nameof(SampleModel.ReadWrite), names);
        Assert.DoesNotContain(nameof(SampleModel.ReadOnly), names);
        Assert.DoesNotContain(nameof(SampleModel.StaticProperty), names);
    }

    private sealed class SampleModel
    {
        public string ReadOnly { get; } = "read";
        public string ReadWrite { get; set; } = string.Empty;
#pragma warning disable CA1822 // Mark members as static, making it static would likely break reflection for the property?
        public string WriteOnly { set { } }
#pragma warning restore CA1822 // Mark members as static
        public static string StaticProperty { get; set; } = string.Empty;
    }
}
