﻿using LingDev.EntityFrameworkCore.SourceGeneration.Reflection;
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace LingDev.SourceGeneration.Reflection;

internal class AssemblyWrapper : Assembly
{
    private readonly MetadataLoadContextInternal _metadataLoadContext;

    internal IAssemblySymbol Symbol { get; }

    public override string FullName => Symbol.Identity.Name;

    public AssemblyWrapper(IAssemblySymbol assembly, MetadataLoadContextInternal metadataLoadContext)
    {
        Symbol = assembly;
        _metadataLoadContext = metadataLoadContext;
    }

    public override Type[] GetExportedTypes()
    {
        return GetTypes();
    }

    public override Type[] GetTypes()
    {
        var types = new List<Type>();
        var stack = new Stack<INamespaceSymbol>();
        stack.Push(Symbol.GlobalNamespace);
        while (stack.Count > 0)
        {
            var current = stack.Pop();

            foreach (INamedTypeSymbol type in current.GetTypeMembers())
            {
                types.Add(type.AsType(_metadataLoadContext)!);
            }

            foreach (INamespaceSymbol ns in current.GetNamespaceMembers())
            {
                stack.Push(ns);
            }
        }
        return types.ToArray();
    }

    public override Type? GetType(string name)
    {
        return Symbol.GetTypeByMetadataName(name)!.AsType(_metadataLoadContext);
    }
}
