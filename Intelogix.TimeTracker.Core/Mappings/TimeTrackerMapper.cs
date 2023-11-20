using AutoMapper;

namespace Intelogix.TimeTracker.Core.Mappings
{
    internal static class TimeTrackerMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source, Profile profile = null)
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
                if (profile != null) cfg.AddProfile(profile);
            })).Map<TDestination>(source);
        }
    }
}
