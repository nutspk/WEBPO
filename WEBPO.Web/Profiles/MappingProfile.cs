using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WEBPO.Domain.Entities;
using WEBPO.Core.ViewModels;

namespace EDI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMapper();
            ContactPersonMapper();
        }

        private void ContactPersonMapper()
        {
            CreateMap<MS_PIC, ContactPersonCreateViewModel>()
                    .ForMember(vm => vm.ContactId, m => m.MapFrom(map => map.IPicId))
                    .ForMember(vm => vm.ContactName, m => m.MapFrom(map => map.IPicName ?? string.Empty))
                    .ForMember(vm => vm.DepartmentId, m => m.MapFrom(map => map.ISectionCd ?? string.Empty))
                    .ForMember(vm => vm.VendorCode, m => m.MapFrom(map => map.IVsCd))
                    .ForMember(vm => vm.Vendor, m => m.MapFrom(map => map.MsVs))
                    .ForMember(vm => vm.Email, m => m.MapFrom(map => map.IMail ?? string.Empty))
                    .ForMember(vm => vm.SendMailFlag, m => m.MapFrom(map => map.IMailFlg))
                    .ForMember(vm => vm.TelNo, m => m.MapFrom(map => map.ITelNo ?? string.Empty))
                    .ForMember(vm => vm.MobileNo, m => m.MapFrom(map => map.IMobileNo ?? string.Empty));

            CreateMap<ContactPersonCreateViewModel, MS_PIC>()
                .ForMember(vm => vm.IPicId, m => m.MapFrom(map => map.ContactId))
                .ForMember(vm => vm.IPicName, m => m.MapFrom(map => map.ContactName ?? string.Empty))
                .ForMember(vm => vm.ISectionCd, m => m.MapFrom(map => map.DepartmentId ?? string.Empty))
                .ForMember(vm => vm.IVsCd, m => m.MapFrom(map => map.VendorCode ?? string.Empty))
                .ForMember(vm => vm.IMail, m => m.MapFrom(map => map.Email ?? string.Empty))
                .ForMember(vm => vm.IMailFlg, m => m.MapFrom(map => map.SendMailFlag))
                .ForMember(vm => vm.ILang, m => m.MapFrom(map => string.Empty))
                .ForMember(vm => vm.ITelNo, m => m.MapFrom(map => map.TelNo ?? string.Empty))
                .ForMember(vm => vm.IMobileNo, m => m.MapFrom(map => map.MobileNo ?? string.Empty));
        }

        private void UserMapper() {
            CreateMap<MS_USER, UserCreateViewModel>()
                    .ForMember(vm => vm.UserID, m => m.MapFrom(map => map.IUserId ?? string.Empty))
                    .ForMember(vm => vm.UserName, m => m.MapFrom(map => map.IUserName ?? string.Empty))
                    .ForMember(vm => vm.Email, m => m.MapFrom(map => map.IMail ?? string.Empty))
                    .ForMember(vm => vm.VendorCode, m => m.MapFrom(map => map.IVsCd))
                    .ForMember(vm => vm.Vendor, m => m.MapFrom(map => map.MS_VS))
                    .ForMember(vm => vm.SendMailFlag, m => m.MapFrom(map => map.IMailFlg))
                    .ForMember(vm => vm.UserType, m => m.MapFrom(map => map.IUserType ?? string.Empty))
                    .ForMember(vm => vm.SectionCode, m => m.MapFrom(map => map.ISectionCd ?? string.Empty))
                    .ForMember(vm => vm.Language, m => m.MapFrom(map => map.ILang ?? string.Empty))
                    .ForMember(vm => vm.ResetPin, m => m.MapFrom(map => map.IResetPin ?? string.Empty));

            CreateMap<UserCreateViewModel, MS_USER>()
                .ForMember(vm => vm.IUserId, m => m.MapFrom(map => map.UserID ?? string.Empty))
                .ForMember(vm => vm.IUserName, m => m.MapFrom(map => map.UserName ?? string.Empty))
                .ForMember(vm => vm.IMail, m => m.MapFrom(map => map.Email ?? string.Empty))
                .ForMember(vm => vm.IVsCd, m => m.MapFrom(map => map.VendorCode ?? string.Empty))
                .ForMember(vm => vm.IMailFlg, m => m.MapFrom(map => map.SendMailFlag))
                .ForMember(vm => vm.IUserType, m => m.MapFrom(map => map.UserType ?? string.Empty))
                .ForMember(vm => vm.ISectionCd, m => m.MapFrom(map => map.SectionCode ?? string.Empty))
                .ForMember(vm => vm.ILang, m => m.MapFrom(map => map.Language ?? string.Empty))
                .ForMember(vm => vm.IResetPin, m => m.MapFrom(map => map.ResetPin ?? string.Empty));
        }
    }
}
