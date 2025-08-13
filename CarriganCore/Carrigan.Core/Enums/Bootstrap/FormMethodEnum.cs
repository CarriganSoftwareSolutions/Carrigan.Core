namespace Carrigan.Core.Enums.Bootstrap;

public enum FormMethodEnum
{
    Null,
    Get,
    Post
}
public static class FormMethodEnumExtension
{
    public static string? ToHtml(this FormMethodEnum color)
    {
        return color switch
        {
            FormMethodEnum.Null => null,
            FormMethodEnum.Get => "get",
            FormMethodEnum.Post => "post",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}
