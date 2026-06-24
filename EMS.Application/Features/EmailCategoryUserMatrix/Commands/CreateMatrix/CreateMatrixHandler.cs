using AutoMapper;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    internal class CreateMatrixHandler(IEmailCategoriesUserMatrixRepository matrixRepository, IMapper mapper) : IRequestHandler<CreateMatrixCommand>
    {
        public async Task Handle(CreateMatrixCommand command, CancellationToken cancellationToken)
        {
            var matrix = mapper.Map<EmailCategoriesUserMatrix>(command);
            await matrixRepository.AddUser(matrix);
        }
    }
}