using AutoMapper;
using Bulka.Models.GameProcess;
using BulkaBussinessLogic.Model.GameProcess;

namespace Bulka.Mappings
{
    public class GameProcessToViewModelMappingProfile : Profile
    {
        public GameProcessToViewModelMappingProfile()
        {
            CreateMap<TestGameProcessResquest, TestGameProcessViewModel>();
            CreateMap<TestGameProcessViewModel, TestGameProcessResquest>();
        }
    }
}