using System;
using System.Reflection;

namespace Optique.Reflection
{
    public readonly struct UnderlyingTypeFilter : IMemberFilter, ITypeFilter
    {
        private readonly bool _covariance;
        private readonly bool _contravariance;
        private readonly bool _includeTargetType;
        private readonly Type _type;


        public UnderlyingTypeFilter(
                Type filterType,
                bool covariance = false,
                bool contravariance = false,
                bool includeTargetType = true)
        {
            _type = filterType;
            _covariance = covariance;
            _contravariance = contravariance;
            _includeTargetType = includeTargetType;
        }

        public bool IsMatch(MemberInfo memberInfo)
        {
            Type underlyingType = memberInfo.GetUnderlyingType();

            if (_includeTargetType && underlyingType == _type)
            {
                return true;
            }
            
            return (_covariance && _type.IsAssignableFrom(underlyingType))
                   || (_contravariance && underlyingType.IsAssignableFrom(_type));
        }

        public bool IsMatch(Type targetObject)
        {
            return IsMatch((MemberInfo) targetObject);
        }
    }
}