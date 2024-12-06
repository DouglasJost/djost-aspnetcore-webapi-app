using AutoMapper;

namespace AppServiceCore.AutoMapper
{
    // By making the callers use this instead of IMapper,
    // we can verify that all mappings actually exist.
    // See AutoMapper Usage Guidelines https://jimmybogard.com/automapper-usage-guidelines/
    public class AutoTypeMapper<TSource, TTarget> : IAutoTypeMapper<TSource, TTarget>
    {
        private readonly IMapper _mapper;

        public AutoTypeMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget Map(TSource source)
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Map(TSource source, TTarget destination)
        {
            return _mapper.Map<TSource, TTarget>(source, destination);
        }
    }
}
