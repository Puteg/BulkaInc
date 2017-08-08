using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bulka.DataModel;
using Bulka.Helpers;
using Bulka.Models.Player;
using BulkaBussinessLogic.Model.GameProcess;
using Profile = AutoMapper.Profile;

namespace Bulka.Mappings
{
    public class PlayerToViewModelMappingProfile : Profile
    {
        public PlayerToViewModelMappingProfile()
        {
            CreateMap<Player, PlayerProfileViewModel>();

            CreateMap<List<PlayerSession>, PlayerSessionListViewModel>()
                .ForMember(g => g.Items, map => map.MapFrom(vm => Mapper.Map<List<PlayerSessionItemViewModel>>(vm)))
                .ForMember(g => g.AvgDuration, map => map.MapFrom(vm => DurationHelper.GetDuration(vm.Sum(c => c.End.Subtract(c.Begin).Ticks/vm.Count))))
                .ForMember(g => g.DayCount, map => map.MapFrom(vm => vm.Count))
                .ForMember(g => g.AvgInput, map => map.MapFrom(vm => (vm.Sum(c => c.Input)/vm.Count).ToString(true)))
                .ForMember(g => g.AvgOutput, map => map.MapFrom(vm => (vm.Sum(c => c.Output)/vm.Count).ToString(true)))
                .ForMember(g => g.Profit, map => map.MapFrom(vm => (vm.Sum(c => c.Output) - vm.Sum(c => c.Input)).ToString(true)));

            CreateMap<PlayerItem, PlayerProfileItemViewModel>()
                .ForMember(g => g.LastGameDate, map => map.MapFrom(vm => vm.LastGameDate.ToString("dd.MM")))
                .ForMember(g => g.Input, map => map.MapFrom(vm => vm.Input.ToString(true)))
                .ForMember(g => g.Output, map => map.MapFrom(vm => vm.Output.ToString(true)))
                .ForMember(g => g.Total, map => map.MapFrom(vm => vm.Total.ToString(true)))
                .ForMember(g => g.Phone, map => map.MapFrom(vm => String.Format("{0:+# (###) ###-##-##}", vm.Phone)))
                .ForMember(g => g.Time, map => map.MapFrom(vm => vm.Time.GetDuration()));

            CreateMap<PlayerSession, PlayerSessionItemViewModel>()
                .ForMember(g => g.DateTime, map => map.MapFrom(vm => vm.Begin.ToString("dd.MM")))
                .ForMember(g => g.Input, map => map.MapFrom(vm => vm.Input.ToString(true)))
                .ForMember(g => g.Output, map => map.MapFrom(vm => vm.Output.ToString(true)))
                .ForMember(g => g.Duration, map => map.MapFrom(vm => DurationHelper.GetDuration(vm.Begin, vm.End)));

        }
    }
}