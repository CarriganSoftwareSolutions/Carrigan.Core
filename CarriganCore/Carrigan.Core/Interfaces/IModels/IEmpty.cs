namespace Carrigan.Core.Interfaces.IModels;


/// <summary>
/// Note: this interface meant for use with Entity Framework models. This will be of little interest to anyone besides myself.
/// It is often used in conjunction with <see cref="IModel"/> to define when certain objects are saved.
/// </summary>
public interface IEmpty
{
    public bool IsEmpty();
    public bool IsNotEmpty();
}
