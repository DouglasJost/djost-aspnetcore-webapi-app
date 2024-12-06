namespace AppServiceCore.AutoMapper
{
    public interface IAutoTypeMapper<in TSource, TTarget>
    {
        TTarget Map(TSource source);

        TTarget Map(TSource source, TTarget destination);
    }
}
