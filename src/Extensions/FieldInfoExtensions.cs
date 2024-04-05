using System;
using System.Reflection;

namespace Optique.Reflection.Extensions;

public static class FieldInfoExtensions
{
    public static AccessModifiers GetAccessModifier(this FieldInfo fieldInfo)
    {
        if (fieldInfo.IsPrivate) return AccessModifiers.Private;
        if (fieldInfo.IsFamily) return AccessModifiers.Protected;
        if (fieldInfo.IsFamilyOrAssembly) return AccessModifiers.ProtectedInternal;
        if (fieldInfo.IsAssembly) return AccessModifiers.Internal;
        if (fieldInfo.IsPublic) return AccessModifiers.Public;
        if (fieldInfo.IsAssembly && fieldInfo.IsFamily) return AccessModifiers.PrivateProtected;

        throw new Exception($"FieldInfo {fieldInfo} has unknown access modifier");
    }
}