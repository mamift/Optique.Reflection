using System;
using System.Reflection;

namespace Optique.Reflection
{
    public class UnderlyingTypeFilter : IMemberFilter, ITypeFilter
    {
        private readonly bool _covariance;
        private readonly bool _contravariance;
        private readonly Type _type;

        
        public UnderlyingTypeFilter(Type filterType, bool covariance = false, bool contravariance = false)
        {
            _type = filterType;
            _covariance = covariance;
            _contravariance = contravariance;
        }
        
        public bool IsMatch(MemberInfo memberInfo)
        {
            Type underlyingType = memberInfo.GetUnderlyingType();
            
            return underlyingType == _type 
                   || (_covariance && _type.IsAssignableFrom(underlyingType))
                   || (_contravariance && underlyingType.IsAssignableFrom(_type));
        }

        public bool IsMatch(Type targetObject)
        {
            return IsMatch((MemberInfo) targetObject);
        }
    }
}