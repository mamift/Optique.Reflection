using System;
using System.Reflection;

namespace Optique.Reflection
{
    public static class ConstructorInfoExtensions
    {
        public static AccessModifiers GetAccessModifier(this ConstructorInfo constructorInfo)
        {
            if (constructorInfo.IsPrivate) return AccessModifiers.Private;
            if (constructorInfo.IsFamily) return AccessModifiers.Protected;
            if (constructorInfo.IsFamilyOrAssembly) return AccessModifiers.ProtectedInternal;
            if (constructorInfo.IsAssembly) return AccessModifiers.Internal;
            if (constructorInfo.IsPublic) return AccessModifiers.Public;
            if (constructorInfo.IsAssembly && constructorInfo.IsFamily) return AccessModifiers.PrivateProtected;

            throw new Exception($"ConstructorInfo {constructorInfo} has unknown access modifier");
        }
        
    }
}