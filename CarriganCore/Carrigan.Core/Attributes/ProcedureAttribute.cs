namespace Carrigan.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ProcedureAttribute : Attribute
{
    public string Name { get; }
    public string Schema { get; }

    public ProcedureAttribute (string Name, string Schema = "")
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("Procedure name cannot be null or empty.", nameof(Name));
        }
        this.Name = Name;
        this.Schema = Schema;
    }
}
