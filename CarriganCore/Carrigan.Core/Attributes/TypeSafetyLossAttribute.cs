using System;

namespace Carrigan.Core.Attributes;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, AllowMultiple = false, Inherited = false)]
public sealed class TypeSafetyLossAttribute : Attribute
{
}