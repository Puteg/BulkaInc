using Bulka.Controllers;
using Bulka.DataModel;
using Profile = AutoMapper.Profile;

namespace Bulka.Mappings
{
    public class PaymentToViewModelMappingProfile : Profile
    {
        public PaymentToViewModelMappingProfile()
        {
            CreateMap<Payment, PaymentItemViewModel>()
                .ForMember(g => g.Sender, map => map.MapFrom(vm => vm.Sender != null ? vm.Sender.Name : vm.GameProcess.Club.Name))
                .ForMember(g => g.Recipient, map => map.MapFrom(vm => vm.Recipient != null ? vm.Recipient.Name : vm.GameProcess.Club.Name))
                .ForMember(g => g.DateTime, map => map.MapFrom(vm => vm.CreateDateTime.ToString()));

            CreateMap<Payment, PaymentEditModel>()
                .ForMember(g => g.From, map => map.MapFrom(vm => vm.Sender != null ? vm.Sender.Name : vm.GameProcess.Club.Name))
                .ForMember(g => g.To, map => map.MapFrom(vm => vm.Recipient != null ? vm.Recipient.Name : vm.GameProcess.Club.Name))
                .ForMember(g => g.DateTime, map => map.MapFrom(vm => vm.CreateDateTime));
            CreateMap<PaymentEditModel, Payment>()
                .ForMember(g => g.CreateDateTime, map => map.MapFrom(vm => vm.DateTime));
        }
    }
}