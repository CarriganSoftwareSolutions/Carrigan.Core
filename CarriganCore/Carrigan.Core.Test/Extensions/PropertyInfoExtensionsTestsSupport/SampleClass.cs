using System.ComponentModel.DataAnnotations;

namespace Carrigan.Core.Test.Extensions.PropertyInfoExtensionsTestsSupport;

/// <summary>
/// A sample class with various properties to test GetDisplayName.
/// </summary>
public class SampleClass
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public SampleClass() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    [Display(Name = "Identifier")]
    public int Id { get; set; }

    [Display(Name = "Full Name")]
    public string Name { get; set; }

    public string Description { get; set; }

    [Display(Name = "")]
    public string EmptyDisplayName { get; set; }

    [Display(Name = null)]
    public string NullDisplayName { get; set; }
}
