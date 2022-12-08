using LingDev.EntityFrameworkCore.SourceGeneration.Reflection;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace LingDev.Audit.SourceGeneration;

[DebuggerDisplay("ContextTypeRef={ContextTypeRef}")]
internal class ContextGenerationSpec
{
    public Location Location { get; set; } = null!;

    public Type ContextType { get; set; } = null!;

    public TypeGenerationSpec GenerationType { get; set; } = null!;

    public List<string> ContextClassDeclarationList { get; set; } = null!;

    public string ContextTypeRef => ContextType.GetCompilableName();
}
