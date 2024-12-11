using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service.Mapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {

            CreateMap<GarageReviewsEntity, CreateReviewDto>();
            CreateMap<CreateReviewDto, GarageReviewsEntity>();
        }
    }
}

