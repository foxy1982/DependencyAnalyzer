// Guids.cs
// MUST match guids.h
using System;

namespace KitsuneSoft.DependencyAnalyzer
{
    static class GuidList
    {
        public const string guidDependencyAnalyzerPkgString = "14e18505-a7ef-4bc2-8bed-d49c0b5062a7";
        public const string guidDependencyAnalyzerCmdSetString = "3af5c766-798c-429e-8414-17ab25fd996e";
        public const string guidToolWindowPersistanceString = "ed24c3ba-1e06-492c-9c83-40632d21eaf7";

        public static readonly Guid guidDependencyAnalyzerCmdSet = new Guid(guidDependencyAnalyzerCmdSetString);
    };
}