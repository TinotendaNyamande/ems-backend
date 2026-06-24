using AutoMapper;
using EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class EmailUserMatrixProfile:Profile
    {
        public EmailUserMatrixProfile()
        {
            CreateMap<CreateMatrixCommand,EmailCategoriesUserMatrix>();
        }
    }
}