using System;
using System.Reflection;

namespace Optique.Reflection.Extensions;

public static class MethodInfoExtensions
{
    public static AccessModifiers GetAccessModifier(this MethodInfo methodInfo)
    {
        if (methodInfo.IsPrivate) return AccessModifiers.Private;
        if (methodInfo.IsFamily) return AccessModifiers.Protected;
        if (methodInfo.IsFamilyOrAssembly) return AccessModifiers.ProtectedInternal;
        if (methodInfo.IsAssembly) return AccessModifiers.Internal;
        if (methodInfo.IsPublic) return AccessModifiers.Public;
        if (methodInfo.IsAssembly && methodInfo.IsFamily) return AccessModifiers.PrivateProtected;

        throw new Exception($"MethodInfo {methodInfo} has unknown access modifier");
    }
}