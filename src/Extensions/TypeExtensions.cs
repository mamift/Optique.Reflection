using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Optique.Reflection.Extensions;

public static class TypeExtensions
{
    public static bool IsCastableTo(this Type from, Type to)
    {
            return to.IsAssignableFrom(from) ||
                   typeof(IConvertible).IsAssignableFrom(from) && typeof(IConvertible).IsAssignableFrom(to) ||
                   from.GetMethods(BindingFlags.Public | BindingFlags.Static)
                           .Any(
                                   m => m.ReturnType == to &&
                                        (m.Name == "op_Implicit" ||
                                         m.Name == "op_Explicit")
                           );
        }

    public static MemberInfo[] GetMembers(this Type targetType, params IMemberFilter[] filters)
    {
            bool IsMatch(MemberInfo memberInfo) => filters.All(filter => filter.IsMatch(memberInfo));
            return targetType.GetMembersRecursively().Where(IsMatch).ToArray();
        }

    public static MemberInfo[] GetAllMembers(this Type targetType)
    {
            return GetMembersRecursively(targetType).ToArray();
        }
        
    private static IEnumerable<MemberInfo> GetMembersRecursively(this Type type)
    {
            if (type == null || type.IsGenericType)
            {
                return Enumerable.Empty<MemberInfo>();
            }

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

            if (type.IsInterface)
            {
                IEnumerable<MemberInfo> members = type.GetMembers(flags);

                foreach (var interfaceType in type.GetInterfaces())
                {
                    members = members.Concat(GetMembersRecursively(interfaceType));
                }

                return members;
            }

            return type.GetMembers(flags).Concat(GetMembersRecursively(type.BaseType));
        }

    public static AccessModifiers GetAccessModifier(this Type type)
    {
            if (type.IsNested)
            {
                if (type.IsNestedPrivate) return AccessModifiers.Private;
                if (type.IsNestedFamily) return AccessModifiers.Protected;
                if (type.IsNestedAssembly) return AccessModifiers.Internal;
                if (type.IsNestedPublic) return AccessModifiers.Public;
            }
            else
            {
                if (type.IsPublic) return AccessModifiers.Public;
                if(type.IsNotPublic) return AccessModifiers.Internal;
            }

            throw new NotImplementedException();
        }
}