namespace Carrigan.Core.Interfaces;

/// <summary>
/// Note: This will be of little interest to anyone besides myself.
/// A class implementing this interface must have the <see cref="IsWhiteSpace"/> 
/// method and <see cref="IsNotWhiteSpace"/> method
/// </summary>
public interface IWhiteSpace : IEmpty
{
    public bool IsWhiteSpace();
    public bool IsNotWhiteSpace();
}
