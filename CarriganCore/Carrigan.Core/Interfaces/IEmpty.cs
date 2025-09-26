namespace Carrigan.Core.Interfaces;

/// <summary>
/// Note: This will be of little interest to anyone besides myself.
/// A class implementing this interface must have the <see cref="IsEmpty"/> 
/// method and <see cref="IsNotEmpty"/> method
/// </summary>
public interface IEmpty
{
    public bool IsEmpty();
    public bool IsNotEmpty();
}
