using Carrigan.Core.Enums.Html;

namespace Carrigan.Core.Razor.ViewModels;

public class FormElements
{
    public string? FormId { get; set; } = null;
    public bool FormValidate { get; set; } = true;
    public FormMethodEnum FormMethod { get; set; } = FormMethodEnum.Null;
    public FormElements() 
    { }

    public FormElements(string? formId, FormMethodEnum formMethod, bool formValidate = true)
    {
        FormId = formId;
        FormValidate = formValidate;
        FormMethod = formMethod;
    }

    public string FormMethodText =>
        FormMethod.ToHtml() ?? string.Empty;
}
