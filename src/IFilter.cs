namespace mamift.Reflequery;

public interface IFilter<in T>
{
    bool IsMatch(T targetObject);
}