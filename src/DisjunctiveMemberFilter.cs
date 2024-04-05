using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Optique.Reflection;

public readonly struct DisjunctiveMemberFilter : IMemberFilter
{
    private readonly IEnumerable<IMemberFilter> _filters;

    public DisjunctiveMemberFilter(IEnumerable<IMemberFilter> filters)
    {
        _filters = filters;
    }

    public bool IsMatch(MemberInfo targetObject)
    {
        return _filters.Any(filter => filter.IsMatch(targetObject));
    }
}