using System;

namespace Optique.Reflection
{
    [Flags]
    [Serializable]
    public enum AccessModifiers
    {
        Private = 1,
        PrivateProtected = 2,
        Protected = 4,
        ProtectedInternal = 8,
        Internal = 16,
        Public = 32
    }
}