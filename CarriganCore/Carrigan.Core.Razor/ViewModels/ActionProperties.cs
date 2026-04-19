namespace Carrigan.Core.Razor.ViewModels;

public class ActionProperties
{
    public string Action { get; set; }
    public string? Controller { get; set; }
    public string? Area { get; set; }
    public Dictionary<string, string?>? RouteValues { get; set; }

    public ActionProperties(string action, string? controller = null, string? area = null,  Dictionary<string, string?>? routeValues = null)
    {
        Action = action;
        Controller = controller;
        Area = area;
        RouteValues = routeValues;
    }
}
