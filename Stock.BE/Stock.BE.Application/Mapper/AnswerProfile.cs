using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service.Mapper
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {

            CreateMap<AnswerEntity, CreateAnswerDto>();
            CreateMap<CreateAnswerDto, AnswerEntity>();
        }
    }
}

