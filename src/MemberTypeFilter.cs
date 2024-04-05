using System.Reflection;

namespace Optique.Reflection;

public readonly struct MemberTypeFilter : IMemberFilter
{
    private readonly MemberTypes _filterCriteria;


    public MemberTypeFilter(MemberTypes memberTypes)
    {
        _filterCriteria = memberTypes;
    }

    public bool IsMatch(MemberInfo memberInfo)
    {
        switch (memberInfo.MemberType) {
            case MemberTypes.Property: return _filterCriteria.HasFlag(MemberTypes.Property);
            case MemberTypes.Field: return _filterCriteria.HasFlag(MemberTypes.Field);
            case MemberTypes.Method: return _filterCriteria.HasFlag(MemberTypes.Method);
            case MemberTypes.Event: return _filterCriteria.HasFlag(MemberTypes.Event);
            case MemberTypes.NestedType: return _filterCriteria.HasFlag(MemberTypes.NestedType);
            case MemberTypes.Constructor: return _filterCriteria.HasFlag(MemberTypes.Constructor);
        }

        return false;
    }
}