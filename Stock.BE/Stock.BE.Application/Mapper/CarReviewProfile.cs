using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service.Mapper
{
    public class CarReviewProfile : Profile
    {
        public CarReviewProfile()
        {
            CreateMap<CarReviewEntity, CreateCarReviewDto>();
            CreateMap<CreateCarReviewDto, CarReviewEntity>();
        }
    }
}

