# mamift.Reflequery

This is a fork of the original: https://github.com/OptiqueGames/Optique.Reflection.

It has been ported to .NET Standard 2.0 (the original targeted .NET Framework 4.7.2).

It contains a set of extensions and utility-features to easily working with C# reflection.

## Features
- Search of types and extracting type's members by customizable filters
- Simplified getting information about `MemberInfo` and it's inheritors
- Accessing to a deep child object through a chain of `MemberInfo`s

## Usage examples

Getting all non-abstract inheritors of a type:
```csharp
Type targetType = typeof(SomeType);
var inheritorsFilter = new UnderlyingTypeFilter(targetType, covariance: true, includeTargetType: false);
var nonAbstractFilter = new CustomTypeFilter(type => type.IsAbstract == false);
Type[] inheritors = ReflectionUtility.GetTypes(inheritorsFilter, nonAbstractFilter);
```

Getting all internal fields and properties of a type:
```csharp
var propertiesAndFieldsFilter = new MemberTypeFilter(MemberTypes.Property | MemberTypes.Field);
var internalMembersFilter = new AccessModifierFilter(AccessModifiers.Internal);

MemberInfo[] internalMembers = typeof(SomeType).GetMembers(propertiesAndFieldsFilter, internalMembersFilter);
```

Getting detailed information of `MemberInfo`'s instance via extensions methods.
```csharp
bool IsPublicStaticMethod(MemberInfo memberInfo)
{
    bool isMethod = memberInfo.IsMethod();
    bool isStatic = memberInfo.IsStatic();
    bool isPublic = memberInfo.GetAccessModifier() == AccessModifiers.Public;
    
    return isMethod && isStatic && isPublic;
}
```
