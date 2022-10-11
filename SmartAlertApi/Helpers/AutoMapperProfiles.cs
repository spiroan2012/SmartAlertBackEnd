using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.Utilities;
using System.Collections.Generic;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Incident, IncidentDto>()
                .ForMember(dest => dest.IncidentCount, opt => opt
                    .MapFrom(src => src.Details.Count))
                .ForMember(dest => dest.CreationDateTime, opt => opt
                    .MapFrom(dest => dest.CreationDateTime))
                .ForMember(dest => dest.Title, opt => opt
                    .MapFrom(src => src.Details.ToList()[0].Title))
                .ForMember(dest => dest.Description, opt => opt
                    .MapFrom(src => src.Details.ToList()[0].Description))
                .ForMember(dest => dest.ImageUrl, opt => opt
                    .MapFrom(src => src.Details.ToList()[0].ImageUrl))
                .ForMember(dest => dest.Latitude, opt => opt
                    .MapFrom(src => src.Coords.Y))
                .ForMember(dest => dest.Address, opt => opt
                    .MapFrom(src => src.Details.ToList()[0].Address))
                .ForMember(dest => dest.Category, opt => opt
                    .MapFrom(src => src.Category.Type))
                .ForMember(dest => dest.Longitude, opt => opt
                    .MapFrom(src => src.Coords.X))
                .ForMember(dest => dest.Grade, opt => opt
                    .MapFrom(src => IncidentUtilityClass.CalculateGrade(src.Coords, src.Details.ToList())));

            CreateMap<IncidentCategory, CategoryDto>();
        }
    }
}
