namespace Carrigan.Core.Interfaces.IModels;

public interface ICountersModel
{
    public Guid Id { get; set; }
    public long Counter { get; set; }
}
