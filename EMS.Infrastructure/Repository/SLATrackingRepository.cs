using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Projects.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using EMS.Application.Dtos.SLATracking;
using EMS.Domain.Enums;


namespace EMS.Infrastructure.Repository
{
    internal class SLATrackingRepository(ApplicationDbContext context) : ISLATrackingRepository
    {


        public async Task<SLATracking> AddAsync(SLATracking slaEntry)
        {
            context.SLATrackings.Add(slaEntry);
            await context.SaveChangesAsync();
            return slaEntry;
        }

        public async Task UpdateAsync(Guid id, UpdateSLAEntryDto slaEntry)
        {
            var entry = await context.SLATrackings.FindAsync(id) ?? throw new ResourceNotFoundException("SLA Entry", id);
            entry.StopEntry(slaEntry.Comments);
            await context.SaveChangesAsync();
        }
        public async Task<SLATracking> GetByIdAsync(Guid id)
        {
            return await context.SLATrackings.FindAsync(id) ?? throw new ResourceNotFoundException("SLA Entry", id);
        }

        public async Task<IEnumerable<SLATracking>> GetByEmailTaskIdAsync(Guid emailTaskId)
        {
            var slaEntries = await context.SLATrackings.Where(s => s.EmailTaskId == emailTaskId).ToListAsync();
            return slaEntries;
        }



        public async Task DeleteAsync(Guid id)
        {
            var affectedRows = await context.SLATrackings.Where(s => s.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                throw new ResourceNotFoundException("SLA Entry", id);
            }
        }

        public async Task<SLATracking> GetCurrentEntryForTaskAsync(Guid emailTaskId)
        {
            var currentEntry = await context.SLATrackings
                .Where(s => s.EmailTaskId == emailTaskId && s.Status == SLAEntryStatus.Running && s.EndTime == null)
                .FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("SLA Entry for Email Task", emailTaskId);
            return currentEntry;
        }

        public async Task<IEnumerable<SLATracking>> GetEntriesByUserIdAsync(string userId)
        {
            return await context.SLATrackings.Where(s => s.UserId == userId).ToListAsync();
        }
    }
}