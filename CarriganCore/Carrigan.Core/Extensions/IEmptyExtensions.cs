using Carrigan.Core.Interfaces.IModels;
using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

public static class IEmptyExtensions
{
    public static bool IsNullOrEmpty(this IEmpty? value) =>
        value == null || value.IsEmpty();
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this IEmpty? value) =>
        value.IsNullOrEmpty() == false;
}
