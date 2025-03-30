using api.Dtos;
using api.Models;
using AutoMapper;
namespace api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() {
            CreateMap<UserModel, MemberDto>();
            CreateMap<PhotoModel, PhotoDto>();
        }

    }
}
