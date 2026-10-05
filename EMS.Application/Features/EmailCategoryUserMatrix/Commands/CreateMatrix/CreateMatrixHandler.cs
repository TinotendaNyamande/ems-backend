using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    internal class CreateMatrixHandler(IEmailCategoriesUserMatrixRepository matrixRepository,IEmailCategoryRepository emailCategoryRepository,IUserService userService, IMapper mapper) : ICommandHandler<CreateMatrixCommand, EmailCategoriesUserMatrix>
    {
        public async Task<EmailCategoriesUserMatrix> Handle(CreateMatrixCommand command, CancellationToken cancellationToken)
        {
            var categoryExists = await emailCategoryRepository.GetCategoryByIdAsync(command.EmailCategoryId) ?? throw new ResourceNotFoundException("Email category", command.EmailCategoryId);
            var userExists = await userService.GetUserByIdAsync(command.UserId) ?? throw new ResourceNotFoundException("User", command.UserId);
            var matrixExists = await matrixRepository.MatrixAlreadyExistsAsync(command.EmailCategoryId, command.UserId);
            if (matrixExists)
            {
                throw new InvalidOperationException("Matrix already exists for the given category and user.");
            }
            var matrix = mapper.Map<EmailCategoriesUserMatrix>(command);
            await matrixRepository.AddUserToMatrixAsync(matrix);
            return matrix;
        }
    }
}