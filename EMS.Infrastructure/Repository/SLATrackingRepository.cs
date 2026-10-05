using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using EMS.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using EMS.Application.Dtos.SLATracking;
using EMS.Domain.Enums;


namespace EMS.Infrastructure.Repository
{
    internal class SLATrackingRepository(ApplicationDbContext context) : ISLATrackingRepository
    {


        public async Task<SLATracking> CreateSLAEntryAsync(SLATracking slaEntry)
        {
            context.SLATrackings.Add(slaEntry);
            return slaEntry;
        }

        public async Task StopTimerAsync(Guid id)
        {
            var entry = await context.SLATrackings.FindAsync(id) ?? throw new ResourceNotFoundException("SLA Entry", id);
            entry.StopEntry();
        }
        public async Task<GetSLATrackingDto?> GetByIdAsync(Guid id)
        {
            var query =
                from sla in context.SLATrackings
                join user in context.Users
                on sla.UserId equals user.Id
                where sla.Id==id
                select new GetSLATrackingDto
                {
                    Id=sla.Id,
                    StartTime=sla.StartTime,
                    EndTime=sla.EndTime,
                    Comments=sla.Comments,
                    Status=sla.Status,
                    UserName = user.UserName
                };
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetSLATrackingDto>> GetByEmailTaskIdAsync(Guid emailTaskId)
        {
           var query = 
                from sla in context.SLATrackings
                join user in context.Users
                on sla.UserId equals user.Id
                where sla.EmailTaskId == emailTaskId
                 orderby sla.StartTime descending
                select new GetSLATrackingDto
                {
                    Id=sla.Id,
                    StartTime= sla.StartTime,
                    EndTime=sla.EndTime,
                    Status=sla.Status,
                    Comments = sla.Comments,
                    UserName = user.UserName
                };
            
            return await query.ToListAsync();
        }



        public async Task DeleteAsync(Guid id)
        {
            var slaEntry = await context.SLATrackings.FindAsync(id) ??
             throw new ResourceNotFoundException("SLA Entry", id);
            context.Remove(slaEntry);
        }

        public async Task<GetSLATrackingDto?> GetCurrentEntryForTaskAsync(Guid emailTaskId)
        {

            var query = 
                from sla in context.SLATrackings
                join user in context.Users
                on sla.UserId equals user.Id
                where sla.EmailTaskId == emailTaskId
                && sla.Status== SLAEntryStatus.Running && sla.EndTime==null
                 orderby sla.StartTime descending
                select new GetSLATrackingDto
                {
                    Id=sla.Id,
                    StartTime= sla.StartTime,
                    EndTime=sla.EndTime,
                    Status=sla.Status,
                    Comments = sla.Comments,
                    UserName = user.UserName
                };
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetSLATrackingDto>> GetEntriesByUserIdAsync(string userId)
        {
                      var query = 
                from sla in context.SLATrackings
                join user in context.Users
                on sla.UserId equals user.Id
                where sla.UserId == userId
                orderby sla.StartTime descending
                select new GetSLATrackingDto
                {
                    Id=sla.Id,
                    StartTime= sla.StartTime,
                    EndTime=sla.EndTime,
                    Status=sla.Status,
                    Comments = sla.Comments,
                    UserName = user.UserName
                };
            
            return await query.ToListAsync();
        }
    }
}