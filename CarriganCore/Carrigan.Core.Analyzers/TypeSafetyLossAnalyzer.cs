using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
//IGNORE SPELLING: callee
namespace Carrigan.Core.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class TypeSafetyLossAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CARRIGAN0002"; 
    private const string MessageFormat = "Member '{0}' is marked [TypeSafetyLoss] and could result in a loss of type safety";
    private static readonly DiagnosticDescriptor Rule = new (
        id: DiagnosticId,
        title: "Loss of Type Safety",
        messageFormat: MessageFormat,
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "APIs marked [TypeSafetyLoss] could result in a loss of type safety.");

    public TypeSafetyLossAnalyzer()
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
            if (HasTypeSafetyLoss(symbol))
            {
                Location location = context.Operation.Syntax.GetLocation();
                string displayString = symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

                Diagnostic diagnostic = Diagnostic.Create(Rule, location, displayString);

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

    private static bool HasTypeSafetyLoss(ISymbol symbol)
    {
        foreach (AttributeData attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass is INamedTypeSymbol attributeClass)
            {
                if (attributeClass.Name == "TypeSafetyLossAttribute")
                    return true;
            }
        }
        return false;
    }
}
