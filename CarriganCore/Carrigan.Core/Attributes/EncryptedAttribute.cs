namespace Carrigan.Core.Attributes;

/// <summary>
/// EncryptedAttribute used to indicate that a string property will be encrypted when using Carrigan.SqlTool
/// Be sure to include a int property that uses <see cref="KeyVersionAttribute"/> to indicate what version number of the encryption key.
/// ToDo: Move to Carrigan.SqlTools
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class EncryptedAttribute : Attribute
{
}
