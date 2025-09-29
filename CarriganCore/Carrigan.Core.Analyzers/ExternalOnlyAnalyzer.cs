using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Carrigan.Core.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class ExternalOnlyAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "EXTO001";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "External-only API invoked internally",
            messageFormat: "Member '{0}' is marked [ExternalOnly] and should not be called from within assembly '{1}'.",
            category: "Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "APIs marked [ExternalOnly] are intended for external consumers only; internal calls are disallowed.");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(Rule); }
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
                if (attributeClass == null)
                {
                    continue;
                }

                string name = attributeClass.Name;
                if (name == "ExternalOnlyAttribute")
                {
                    return true;
                }

                string full = attributeClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                if (full.EndsWith(".ExternalOnlyAttribute"))
                {
                    return true;
                }
            }
            return false;
        }

        private static void AnalyzeObjectCreation(OperationAnalysisContext context)
        {
            IObjectCreationOperation operation = (IObjectCreationOperation)context.Operation;
            IMethodSymbol? constructor = operation.Constructor;
            if (constructor == null)
            {
                return;
            }

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
}
