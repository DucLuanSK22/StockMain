using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service.Mapper
{
    public class QuestionsProfile : Profile
    {
        public QuestionsProfile()
        {

            CreateMap<QuestionsEntity, CreateQuestionsDto>();
            CreateMap<CreateQuestionsDto, QuestionsEntity>();
        }
    }
}

