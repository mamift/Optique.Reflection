namespace Optique.Reflection
{
    public interface IFilter<in T>
    {
        bool IsMatch(T targetObject);
    }
}