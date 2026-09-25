using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    internal class CreateMatrixHandler(IEmailCategoriesUserMatrixRepository matrixRepository,IEmailCategoryRepository emailCategoryRepository,IUserService userService, IMapper mapper) : ICommandHandler<CreateMatrixCommand, GetMatrixDto>
    {
        public async Task<GetMatrixDto> Handle(CreateMatrixCommand command, CancellationToken cancellationToken)
        {
             await emailCategoryRepository.GetCategoryByIdAsync(command.EmailCategoryId);
            await userService.GetUserByIdAsync(command.UserId);
            var matrixExists = await matrixRepository.MatrixAlreadyExistsAsync(command.EmailCategoryId, command.UserId);
            if (matrixExists)
            {
                throw new InvalidOperationException("Matrix already exists for the given category and user.");
            }
            var matrix = mapper.Map<EmailCategoriesUserMatrix>(command);
            await matrixRepository.AddUserToMatrixAsync(matrix);
            return await matrixRepository.GetMatrixByIdAsync(matrix.Id);
        }
    }
}