using AutoMapper;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class EmailCategoryMappingProfile:Profile
    {
        public EmailCategoryMappingProfile()
        {
            CreateMap<CreateEmailCategoryCommand,EmailCategory>();
            CreateMap<EmailCategory,GetEmailCategoriesDto>();
        }
    }
}