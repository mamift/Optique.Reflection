using System;
using System.Reflection;

namespace Optique.Reflection.Extensions;

public static class MemberInfoExtensions
{
    public static Type GetUnderlyingType(this MemberInfo memberInfo)
    {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Property: return ((PropertyInfo)memberInfo).PropertyType;
                case MemberTypes.Field: return ((FieldInfo)memberInfo).FieldType;
                case MemberTypes.Method: return ((MethodInfo)memberInfo).ReturnType;
                case MemberTypes.Event: return ((EventInfo)memberInfo).EventHandlerType;
                case MemberTypes.TypeInfo: return (Type)memberInfo;
                case MemberTypes.Constructor: return ((ConstructorInfo)memberInfo).DeclaringType;
                case MemberTypes.NestedType: return (Type)memberInfo;
                default: throw new NotImplementedException();
            }
        }

    public static bool IsProperty(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.Property;
    public static bool IsField(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.Field;
    public static bool IsMethod(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.Method;
    public static bool IsEvent(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.Event;
    public static bool IsTypeInfo(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.TypeInfo;
    public static bool IsConstructor(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.Constructor;
    public static bool IsNestedType(this MemberInfo memberInfo) => memberInfo.MemberType == MemberTypes.NestedType;

    public static bool IsStatic(this MemberInfo memberInfo)
    {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Property:
                {
                    PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                    return propertyInfo.GetMethod != null ? propertyInfo.GetMethod.IsStatic : propertyInfo.SetMethod.IsStatic;
                }
                case MemberTypes.Field: return ((FieldInfo)memberInfo).IsStatic;
                case MemberTypes.Method: return ((MethodInfo)memberInfo).IsStatic;
                case MemberTypes.Event: return ((EventInfo)memberInfo).GetAddMethod().IsStatic;
                case MemberTypes.TypeInfo:
                {
                    Type type = (Type)memberInfo;
                    return type.IsAbstract && type.IsSealed;
                }
                case MemberTypes.Constructor: return ((ConstructorInfo)memberInfo).IsStatic;
                case MemberTypes.NestedType:
                {
                    Type type = (Type)memberInfo;
                    return type.IsAbstract && type.IsSealed;
                }
                default: throw new NotImplementedException();
            }
        }
        
    public static AccessModifiers GetAccessModifier(this MemberInfo memberInfo)
    {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Property:
                {
                    PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                    if (propertyInfo.SetMethod == null) return propertyInfo.GetMethod.GetAccessModifier();
                    if (propertyInfo.GetMethod == null) return propertyInfo.SetMethod.GetAccessModifier();

                    return (AccessModifiers)Math.Max((int)propertyInfo.GetMethod.GetAccessModifier(), (int)propertyInfo.SetMethod.GetAccessModifier());
                }
                case MemberTypes.Field: return ((FieldInfo)memberInfo).GetAccessModifier();
                case MemberTypes.Method: return ((MethodInfo)memberInfo).GetAccessModifier();
                case MemberTypes.Event: return ((EventInfo)memberInfo).AddMethod.GetAccessModifier();
                case MemberTypes.TypeInfo: return ((Type)memberInfo).GetAccessModifier();
                case MemberTypes.Constructor: return ConstructorInfoExtensions.GetAccessModifier(((ConstructorInfo)memberInfo));
                case MemberTypes.NestedType: return ((Type)memberInfo).GetAccessModifier();
                default: throw new NotImplementedException();
            }
        }
}