using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
//IGNORE SPELLING: callee
namespace Carrigan.Core.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ExternalOnlyAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CARRIGAN0001";
    private const string MessageFormat = "Member '{0}' is marked [ExternalOnly] and should not be called from within assembly '{1}'";
    private static readonly DiagnosticDescriptor Rule = new (
        id: DiagnosticId,
        title: "External-only API invoked internally",
        messageFormat: MessageFormat,
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "APIs marked [ExternalOnly] are intended for external consumers only; internal calls are disallowed.");

    public ExternalOnlyAnalyzer()
    {
    }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
    {
        get { return [Rule]; }
    }

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        context.RegisterOperationAction(AnalyzeInvocation, OperationKind.Invocation);
        context.RegisterOperationAction(AnalyzeObjectCreation, OperationKind.ObjectCreation);
    }

    private static bool HasExternalOnly(ISymbol symbol)
    {
        foreach (AttributeData attribute in symbol.GetAttributes())
        {
            INamedTypeSymbol? attributeClass = attribute.AttributeClass;
            if (attributeClass is not null)
            {
                if (attributeClass.Name == "ExternalOnlyAttribute"
                    || attributeClass
                        .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                        .EndsWith(".ExternalOnlyAttribute"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static void AnalyzeObjectCreation(OperationAnalysisContext context)
    {
        IObjectCreationOperation operation = (IObjectCreationOperation)context.Operation;
        IMethodSymbol? constructor = operation.Constructor;
        if (constructor is not null)
        {
            if (HasExternalOnly(constructor) && IsInternalCall(context.Compilation, constructor))
            {
                Diagnostic diagnostic = Diagnostic.Create(
                    Rule,
                    operation.Syntax.GetLocation(),
                    constructor.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                    constructor.ContainingAssembly?.Name ?? "<unknown>");
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    private static void AnalyzeInvocation(OperationAnalysisContext context)
    {
        IInvocationOperation operation = (IInvocationOperation)context.Operation;
        IMethodSymbol target = operation.TargetMethod;

        if (HasExternalOnly(target) && IsInternalCall(context.Compilation, target))
        {
            Diagnostic diagnostic = Diagnostic.Create(
                Rule,
                operation.Syntax.GetLocation(),
                target.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                target.ContainingAssembly?.Name ?? "<unknown>");
            context.ReportDiagnostic(diagnostic);
        }
    }

    private static bool IsInternalCall(Compilation compilation, ISymbol callee)
    {
        IAssemblySymbol callerAssembly = compilation.Assembly;
        IAssemblySymbol? calleeAssembly = callee.ContainingAssembly;
        return SymbolEqualityComparer.Default.Equals(callerAssembly, calleeAssembly);
    }
}
