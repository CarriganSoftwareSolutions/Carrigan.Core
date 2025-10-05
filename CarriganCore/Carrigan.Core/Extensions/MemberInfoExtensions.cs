using System.Reflection;

namespace Carrigan.Core.Extensions;
public static class MemberInfoExtensions
{
    public static string GetQualifiedName(this MemberInfo member)
    {
        if (member is Type type)
            return type.FullName ?? type.Name;

        string declaring = member.DeclaringType?.FullName ?? "<global>";
        return $"{declaring}.{member.Name}";
    }
}
