using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Bulka.Helpers;
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

            CreateMap<GameProcessItem, GameProcessItemViewModel>();
            CreateMap<PlayerStuff, PlayerStuffViewModel>()
                .ForMember(g => g.Amount, map => map.MapFrom(vm => vm.Amount.ToString(true)))
                .ForMember(g => g.Time, map => map.MapFrom(vm => vm.Time.ToShortTimeString()));
            CreateMap<Action, ActionEditModel>();

            CreateMap<GameProcessModel, GameProcessEditModel>()
                .ForMember(g => g.TotalInput, map => map.MapFrom(vm => vm.TotalInput.ToString(true)))
                .ForMember(g => g.TotalOutput, map => map.MapFrom(vm => vm.TotalOutput.ToString(true)))
                .ForMember(g => g.Total, map => map.MapFrom(vm => vm.Total.ToString(true)))
                .ForMember(g => g.DirationTime, map => map.MapFrom(vm => vm.DirationTime.GetDuration()))
                .ForMember(g => g.EditModel, map => map.MapFrom(vm => Mapper.Map<ActionEditModel>(vm.EditModel)))
                .ForMember(g => g.Items, map => map.MapFrom(vm => Mapper.Map<List<GameProcessItemViewModel>>(vm.Items)))
                .ForMember(g => g.Players, map => map.MapFrom(vm => Mapper.Map<List<SelectListItem>>(vm.Players)));

            CreateMap<ClubList, ClubsViewModel>()
                .ForMember(g => g.Clubs, map => map.MapFrom(vm => Mapper.Map<List<ClubItemViewModel>>(vm.Clubs)));

            CreateMap<ClubItem, ClubItemViewModel>()
                .ForMember(g => g.Total, map => map.MapFrom(vm => vm.Total.ToString(true)))
                .ForMember(g => g.Items, map => map.MapFrom(vm => Mapper.Map<List<GameProcessListItemViewModel>>(vm.Items)));

            CreateMap<GameProcessListItem, GameProcessListItemViewModel>()
                .ForMember(g => g.DateTime, map => map.MapFrom(vm => vm.DateTime.ToLongDateString()))
                .ForMember(g => g.TotalInput, map => map.MapFrom(vm => vm.TotalInput.ToString(true)))
                .ForMember(g => g.TotalOutput, map => map.MapFrom(vm => vm.TotalOutput.ToString(true)))
                .ForMember(g => g.Total, map => map.MapFrom(vm => vm.Total.ToString(true)))
                .ForMember(g => g.DirationTime, map => map.MapFrom(vm => vm.DirationTime.GetDuration()));
        }
    }
}