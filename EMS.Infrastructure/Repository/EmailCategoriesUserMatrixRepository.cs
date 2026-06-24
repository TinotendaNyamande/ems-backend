using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class EmailCategoriesUserMatrixRepository(ApplicationDbContext context) : IEmailCategoriesUserMatrixRepository
    {
        public async Task AddUser(EmailCategoriesUserMatrix userMatrix)
        {
            context.Add(userMatrix);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMatrix(Guid id)
        {
            var affectedRows = await context.EmailCategoriesUserMatrices.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("Email category matrix", id);
            }
        }
        public async Task<IEnumerable<EmailCategoriesUserMatrix>> GetAllAvailableForCategory(Guid? categoryID)
        {
            var matrix = await context.EmailCategoriesUserMatrices.Where(e => e.EmailCategoryId == categoryID && e.IsAvailable == true).ToListAsync();
            if (matrix.Count == 0)
            {
                throw new ResourceNotFoundException("Email category matrix", categoryID);
            }
            return matrix;
        }

        public async Task<GetMatrixDto> GetMatrixById(Guid id)
        {
                        var query = from matrices in context.EmailCategoriesUserMatrices.AsNoTracking()
                        join categories in context.EmailCategories.AsNoTracking()
                        on matrices.EmailCategoryId equals categories.Id
                        join users in context.Users.AsNoTracking()
                        on matrices.UserId equals users.Id
                        where matrices.Id == id
                        select new GetMatrixDto
                        {
                            Id = matrices.Id,
                            UserFirstName = users.FirstName,
                            UserLastName = users.LastName,
                            Userid = matrices.UserId,
                            CategoryId = matrices.EmailCategoryId,
                            CategoryName = categories.CategoryName,
                            IsAvailable = matrices.IsAvailable,
                            LastAssignedDate = matrices.LastAssignedAt

                        };
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetMatrixDto>> GetMatrixForOrganisation(Guid organisationId)
        {
            var query = from matrices in context.EmailCategoriesUserMatrices.AsNoTracking()
                        join categories in context.EmailCategories.AsNoTracking()
                        on matrices.EmailCategoryId equals categories.Id
                        join users in context.Users.AsNoTracking()
                        on matrices.UserId equals users.Id
                        where categories.OrganisationId == organisationId
                        select new GetMatrixDto
                        {
                            Id = matrices.Id,
                            UserFirstName = users.FirstName,
                            UserLastName = users.LastName,
                            Userid = matrices.UserId,
                            CategoryId = matrices.EmailCategoryId,
                            CategoryName = categories.CategoryName,
                            IsAvailable = matrices.IsAvailable,
                            LastAssignedDate = matrices.LastAssignedAt

                        };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<GetMatrixDto>> GetMatrixForUser(string id)
        {
            var query = from matrices in context.EmailCategoriesUserMatrices.AsNoTracking()
                        join categories in context.EmailCategories.AsNoTracking()
                        on matrices.EmailCategoryId equals categories.Id
                        join users in context.Users.AsNoTracking()
                        on matrices.UserId equals users.Id
                        where users.Id == id
                        select new GetMatrixDto
                        {
                            Id = matrices.Id,
                            UserFirstName = users.FirstName,
                            UserLastName = users.LastName,
                            Userid = matrices.UserId,
                            CategoryId = matrices.EmailCategoryId,
                            CategoryName = categories.CategoryName,
                            IsAvailable = matrices.IsAvailable,
                            LastAssignedDate = matrices.LastAssignedAt

                        };
            return await query.ToListAsync();
        }

        public async Task UserAssignedTaskAction(Guid matrixId)
        {
            var matrix = await context.EmailCategoriesUserMatrices.FindAsync(matrixId)
            ?? throw new ResourceNotFoundException("Matrix", matrixId);
            matrix.ChangeLastAssignedDate();
            await context.SaveChangesAsync();
        }
    }
}