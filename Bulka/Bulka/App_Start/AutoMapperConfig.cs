using AutoMapper;
using Bulka.Mappings;

namespace Bulka
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<PlayerToViewModelMappingProfile>();
            });
        }
    }
}