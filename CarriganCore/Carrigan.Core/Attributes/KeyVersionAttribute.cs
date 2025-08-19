namespace Carrigan.Core.Attributes;

/// <summary>
/// KeyVersionAttribute is used to store which version of the encryption key should be used, when using Carrigan.SqlTools
/// The property should be an Int. I probably should have used it with uint, but that is a breaking change for another day.
/// This will likely be move to Carrigan.SqlTools in the future.
/// Recommended to use in conjunction with Carrigan.SqlTools.IndexedAttribute, if you are primarily using the annotation in conjunction with Entity Framework.
/// ToDo: Move to Carrigan.SqlTools
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class KeyVersionAttribute : Attribute
{
}
