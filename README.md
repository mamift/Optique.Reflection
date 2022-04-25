# Optique.Reflection

The library contains a set of extensions and utility-features to easily working with C# reflection.

## Features
- Search of types and extracting type's members by customizable filters
- Simplified getting information about `MemberInfo` and it's inheritors
- Accessing to a deep child object through a chain of `MemberInfo`s

## Usage examples

Getting all inheritors of a type (including the target type):
```csharp
var inheritorsFilter = new UnderlyingTypeFilter(typeof(SomeType), covariance: true);
Type[] types = ReflectionUtility.GetTypes(inheritorsFilter);
```

Getting all internal fields and properties of a type:
```csharp
IMemberFilter propertiesAndFieldsFilter = new MemberTypeFilter(MemberTypes.Property | MemberTypes.Field);
IMemberFilter internalMembersFilter = new AccessModifierFilter(AccessModifiers.Internal);

MemberInfo[] internalMembers = typeof(SomeType).GetMembers(propertiesAndFieldsFilter, internalMembersFilter);
```

Getting detailed information of `MemberInfo`'s instance via extensions methods.
```csharp
bool IsPublicStaticMethod(MemberInfo memberInfo)
{
    return memberInfo.IsMethod() && memberInfo.IsStatic() && memberInfo.GetAccessModifier() == AccessModifiers.Public;
}
```
