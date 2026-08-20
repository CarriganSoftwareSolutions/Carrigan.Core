using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
//IGNORE SPELLING: callee
namespace Carrigan.Core.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ExternalOnlyAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CARRIGAN0001"; 
    private const string MessageFormat = "Member '{0}' is marked [ExternalOnly] and should not be used from within assembly '{1}'";
    private static readonly DiagnosticDescriptor Rule = new (
        id: DiagnosticId,
        title: "External-only API used internally",
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

        context.RegisterOperationAction(
            AnalyzeOperation,
            OperationKind.Invocation,
            OperationKind.ObjectCreation,
            OperationKind.PropertyReference,
            OperationKind.FieldReference,
            OperationKind.EventReference,
            OperationKind.MethodReference,
            OperationKind.Binary,
            OperationKind.Unary,
            OperationKind.Conversion,
            OperationKind.Increment,
            OperationKind.Decrement,
            OperationKind.CompoundAssignment);
    }

    private static void AnalyzeOperation(OperationAnalysisContext context)
    {
        if (GetReferencedSymbol(context.Operation) is ISymbol symbol)
        {
            if (HasExternalOnly(symbol) && IsInternalCall(context.Compilation, symbol))
            {
                Location location = context.Operation.Syntax.GetLocation();
                string displayString = symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                string containingAssemblyName = symbol.ContainingAssembly?.Name ?? "<unknown>";

                Diagnostic diagnostic = Diagnostic.Create(Rule, location, displayString, containingAssemblyName);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    private static ISymbol? GetReferencedSymbol(IOperation operation) =>
        operation switch
        {
            IInvocationOperation invocation =>
                invocation.TargetMethod,

            IObjectCreationOperation objectCreation =>
                objectCreation.Constructor,

            IPropertyReferenceOperation propertyReference =>
                 propertyReference.Property,

            IFieldReferenceOperation fieldReference =>
                fieldReference.Field,

            IEventReferenceOperation eventReference =>
                 eventReference.Event,

            IMethodReferenceOperation methodReference =>
                 methodReference.Method,

            IBinaryOperation binary =>
                binary.OperatorMethod,

            IUnaryOperation unary =>
                 unary.OperatorMethod,

            IConversionOperation conversion =>
                 conversion.OperatorMethod,

            IIncrementOrDecrementOperation incrementOrDecrement =>
                 incrementOrDecrement.OperatorMethod,

            ICompoundAssignmentOperation compoundAssignment =>
                compoundAssignment.OperatorMethod,


            _ =>
                null
        };

    private static bool HasExternalOnly(ISymbol symbol)
    {
        foreach (AttributeData attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass is INamedTypeSymbol attributeClass)
            {
                if (attributeClass.Name == "ExternalOnlyAttribute")
                    return true;
            }
        }
        return false;
    }


    private static bool IsInternalCall(Compilation compilation, ISymbol callee)
    {
        IAssemblySymbol callerAssembly = compilation.Assembly;
        IAssemblySymbol? calleeAssembly = callee.ContainingAssembly;
        return SymbolEqualityComparer.Default.Equals(callerAssembly, calleeAssembly);
    }
}
