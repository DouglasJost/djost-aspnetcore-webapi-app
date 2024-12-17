using AppDomainEntities.Entities;
using AppServiceCore.Models.Authentication;
using AutoMapper;

namespace AppServiceCore.Repositories.UserAuthentication
{
    public class UserAuthenticationMapper : Profile
    {
        public UserAuthenticationMapper()
        {
            CreateMap<UserLogin, AuthenticationDto>()
                .ForMember(d => d.UserAccountId, o => o.MapFrom(s => s.UserAccountId))
                .ForMember(d => d.Login, o => o.MapFrom(s => s.Login))
                .ForMember(d => d.UserLoginInactive, o => o.MapFrom(s => s.Inactive))
                .ForMember(d => d.UserAccountInactive, o => o.MapFrom(s => GetUserAccountInactive(s.UserAccount)))
                .ForMember(d => d.UserFirstName, o => o.MapFrom(s => GetUserAccountFirstName(s.UserAccount)))
                .ForMember(d => d.UserLastName, o => o.MapFrom(s => GetUserAccountLastName(s.UserAccount)));
        }

        private bool GetUserAccountInactive(UserAccount us)
        {
            return us != null ? us.Inactive : false;
        }

        private string GetUserAccountFirstName(UserAccount us)
        {
            if (us == null || string.IsNullOrWhiteSpace(us.FirstName))
            {
                return string.Empty;
            }
            else
            {
                return us.FirstName;
            }
        }

        private string GetUserAccountLastName(UserAccount us)
        {
            if (us == null || string.IsNullOrWhiteSpace(us.LastName))
            {
                return string.Empty;
            }
            else
            {
                return us.LastName;
            }
        }
    }
}
