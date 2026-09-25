using EMS.Application.Dtos.TaskAuditTrail;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class TaskAuditRepository(ApplicationDbContext context) : ITaskAuditRepository
    {
        public async Task<TaskAuditTrail> CreateTaskAuditTrailAsync(TaskAuditTrail taskAuditTrail)
        {
            context.TaskAuditTrails.Add(taskAuditTrail);
            return taskAuditTrail;
        }

        public async Task<GetTaskAuditTrailDto> GetAuditTrailEntryById(Guid id)
        {
            var query =
    from audit in context.TaskAuditTrails
    join user in context.Users
    on audit.UserId equals user.Id
    join task in context.EmailTasks
    on audit.EmailTaskId equals task.Id
    where audit.Id == id
    select new GetTaskAuditTrailDto
    (
        audit.Id,
        audit.UserId == "System" ? "System" : user.UserName,
        audit.Comments,
         audit.CreatedAt,
         task.Id
    );
            return await query.FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Adit trail entry", id);
        }
        public async Task<IEnumerable<GetTaskAuditTrailDto>> GetTaskAuditTrail(Guid emailTaskId)
        {
            var query =
                from audit in context.TaskAuditTrails
                join user in context.Users
                on audit.UserId equals user.Id
                join task in context.EmailTasks
                on audit.EmailTaskId equals task.Id
                where audit.EmailTaskId == emailTaskId
                select new GetTaskAuditTrailDto
                (
                    audit.Id,
                    audit.UserId == "System" ? "System" : user.UserName,
                    audit.Comments,
                     audit.CreatedAt,
                    emailTaskId
                );
            return await query.ToListAsync();



        }
    }
}