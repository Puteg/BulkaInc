using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Bulka.Models.Club;
using Bulka.Models.Club.EditModel;
using BulkaBussinessLogic.Model.Club;

namespace Bulka.Mappings
{
    public class ClubToViewModelMappingProfile : Profile
    {
        public ClubToViewModelMappingProfile()
        {
            CreateMap<Clubs, ClubsViewModel>()
                .ForMember(g => g.Items, map => map.MapFrom(vm => Mapper.Map<List<ClubListItemViewModel>>(vm.Items)));
            CreateMap<ClubItem, ClubListItemViewModel>();
            
            CreateMap<ClubEdit, ClubEditModel>();
            CreateMap<ClubEditModel, ClubEdit>();
        }
    }
}