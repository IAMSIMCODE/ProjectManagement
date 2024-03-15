using AutoMapper;
using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Domain.MappingProfile
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<CreateDevAchievementRequest, Achievement>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(
                    dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.UpdatedDate,
                    opt => opt.MapFrom(src => DateTime.Now)
                );

            CreateMap<UpdateDevAchievementRequest, Achievement>()
               .ForMember(
                   dest => dest.UpdatedDate,
                   opt => opt.MapFrom(src => DateTime.Now)
               );


            // developer mapping

            CreateMap<CreateDeveloperRequest, Developer>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(
                    dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.UpdatedDate,
                    opt => opt.MapFrom(src => DateTime.Now)
                );


            CreateMap<UpdateDeveloperRequest, Developer>()
                .ForMember(
                   dest => dest.UpdatedDate,
                   opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.DeveloperId));


            //=========================================================================================================================
            //Note : If the name from the source is different from the name in the destination
            // Then you will have to explicitly map from source to destination and vis vasa. example: let mapresponsecode to status
            //.ForMember(
            //        dest => dest.Status,
            //        opt => opt.MapFrom(src => src.responsecode)); This will map the responsecode to status in the destination object
            //============================================================================================================================
        }
    }
}
