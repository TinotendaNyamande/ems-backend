using EMS.Application.Dtos.Auth;
using EMS.Application.Dtos.JoinRequests;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Infrastructure.persistence;
using Microsoft.EntityFrameworkCore;
using Projects.Domain.Exceptions;

namespace EMS.Infrastructure.Repository
{
    internal class JoinRequestsRepository(ApplicationDbContext context) : IJoinRequestsRepository
    {
        public async Task ApproveJoinRequestAsync(Guid requestId, string approvingUserId)
        {
            var request = await context.JoinRequests.FindAsync(requestId)
                ?? throw new ResourceNotFoundException("Join request",requestId);

            request.Approve(approvingUserId);
            await context.SaveChangesAsync();
        }

        public async Task CreateJoinRequestAsync(JoinRequest joinRequest)
        {
            context.Add(joinRequest);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRequestAsync(Guid requestId)
        {
            var affectedRows = await context.JoinRequests.Where(o => o.Id == requestId).ExecuteDeleteAsync();
            if(affectedRows == 0)
            {
                throw new ResourceNotFoundException("Join request", requestId);
            }
        }

        public async Task<IEnumerable<JoinRequestDto>> GetAllJoinRequestsAsync(Guid organisationId)
        {
            var query =
                 from request in context.JoinRequests.AsNoTracking()
                 where request.OrganisationId == organisationId && request.IsApproved == false && request.IsRejected == false

                 join requester in context.Users.AsNoTracking()
                     on request.RequestById equals requester.Id

                 join approverUser in context.Users.AsNoTracking()
                     on request.ApprovedByUserId equals approverUser.Id into approvers
                 from approver in approvers.DefaultIfEmpty()

                 join rejectorUser in context.Users.AsNoTracking()
                     on request.RejectedByUserId equals rejectorUser.Id into rejectors
                 from rejector in rejectors.DefaultIfEmpty()

                 select new JoinRequestDto
                 {
                     Id = request.Id,
                     Status = request.IsApproved ? "Approved" : request.IsRejected ? "Rejected" : "Pending",
                     RequestedAt = request.RequestedAt,
                     ApprovedAt = request.ApprovedAt,
                     RejectedAt = request.RejectedAt,
                     RequestedBy = new UserDto
                     {
                         Id = requester.Id,
                         FirstName = requester.FirstName,
                         LastName = requester.LastName,
                         Email = requester.Email
                     },

                     ApprovedBy = approver == null ? null : new UserDto
                     {
                         Id = approver.Id,
                         FirstName = approver.FirstName,
                         LastName = approver.LastName,
                         Email = approver.Email
                     },

                     RejectedBy = rejector == null ? null : new UserDto
                     {
                         Id = rejector.Id,
                         FirstName = rejector.FirstName,
                         LastName = rejector.LastName,
                         Email = rejector.Email
                     }
                 };
            return await query.ToListAsync();
        }

        public async Task<JoinRequest> GetJoinRequestByIdAsync(Guid requestId)
        {
           return await context.JoinRequests
                .AsNoTracking()
                .Where(j=>j.Id==requestId)
                .FirstOrDefaultAsync()??
                throw new ResourceNotFoundException("Join request", requestId);
        }

        public async Task<IEnumerable<JoinRequestDto>> GetPendingJoinRequestsAsync(Guid organisationId)
        {
            var query =
                from request in context.JoinRequests.AsNoTracking()
                where request.OrganisationId == organisationId && request.IsApproved == false && request.IsRejected == false

                join requester in context.Users.AsNoTracking()
                    on request.RequestById equals requester.Id

                join approverUser in context.Users.AsNoTracking()
                    on request.ApprovedByUserId equals approverUser.Id into approvers
                from approver in approvers.DefaultIfEmpty()

                join rejectorUser in context.Users.AsNoTracking()
                    on request.RejectedByUserId equals rejectorUser.Id into rejectors
                from rejector in rejectors.DefaultIfEmpty()

                select new JoinRequestDto
                {
                    Id = request.Id,
                    Status = request.IsApproved ? "Approved" : request.IsRejected ? "Rejected" : "Pending",
                    RequestedAt = request.RequestedAt,
                    ApprovedAt = request.ApprovedAt,
                    RejectedAt = request.RejectedAt,
                    RequestedBy = new UserDto
                    {
                        Id = requester.Id,
                        FirstName = requester.FirstName,
                        LastName = requester.LastName,
                        Email = requester.Email
                    },

                    ApprovedBy = approver == null ? null : new UserDto
                    {
                        Id = approver.Id,
                        FirstName = approver.FirstName,
                        LastName = approver.LastName,
                        Email = approver.Email
                    },

                    RejectedBy = rejector == null ? null : new UserDto
                    {
                        Id = rejector.Id,
                        FirstName = rejector.FirstName,
                        LastName = rejector.LastName,
                        Email = rejector.Email
                    }
                };
            return await query.ToListAsync();

        }

        public async Task<IEnumerable<JoinRequestDto>> GetUserJoinRequestsAsync(string userId)
        {
            var query =from request in context.JoinRequests.AsNoTracking()
                          where request.RequestById == userId
                          join requester in context.Users.AsNoTracking()
                              on request.RequestById equals requester.Id
                          join approverUser in context.Users.AsNoTracking()
                              on request.ApprovedByUserId equals approverUser.Id into approvers
                          from approver in approvers.DefaultIfEmpty()
                          join rejectorUser in context.Users.AsNoTracking()
                              on request.RejectedByUserId equals rejectorUser.Id into rejectors
                          from rejector in rejectors.DefaultIfEmpty()
                          select new JoinRequestDto
                          {
                              Id = request.Id,
                              Status = request.IsApproved ? "Approved" : request.IsRejected ? "Rejected" : "Pending",
                              RequestedAt = request.RequestedAt,
                              ApprovedAt = request.ApprovedAt,
                              RejectedAt = request.RejectedAt,
                              RequestedBy = new UserDto
                              {
                                  Id = requester.Id,
                                  FirstName = requester.FirstName,
                                  LastName = requester.LastName,
                                  Email = requester.Email
                              },
                              ApprovedBy = approver == null ? null : new UserDto
                              {
                                  Id = approver.Id,
                                  FirstName = approver.FirstName,
                                  LastName = approver.LastName,
                                  Email = approver.Email
                              },
                              RejectedBy = rejector == null ? null : new UserDto
                              {
                                  Id = rejector.Id,
                                  FirstName = rejector.FirstName,
                                  LastName = rejector.LastName,
                                  Email = rejector.Email
                              }
                          };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<JoinRequestDto>> GetUserPendingJoinRequestsAsync(string userId)
        {
            var query = from request in context.JoinRequests.AsNoTracking()
                        where request.RequestById == userId
                        join requester in context.Users.AsNoTracking()
                            on request.RequestById equals requester.Id
                        join approverUser in context.Users.AsNoTracking()
                            on request.ApprovedByUserId equals approverUser.Id into approvers
                        from approver in approvers.DefaultIfEmpty()
                        join rejectorUser in context.Users.AsNoTracking()
                            on request.RejectedByUserId equals rejectorUser.Id into rejectors
                        from rejector in rejectors.DefaultIfEmpty()
                        select new JoinRequestDto
                        {
                            Id = request.Id,
                            Status = request.IsApproved ? "Approved" : request.IsRejected ? "Rejected" : "Pending",
                            RequestedAt = request.RequestedAt,
                            RejectedAt = request.RejectedAt,
                            ApprovedAt = request.ApprovedAt,
                            RequestedBy = new UserDto
                            {
                                Id = requester.Id,
                                FirstName = requester.FirstName,
                                LastName = requester.LastName,
                                Email = requester.Email
                            },
                            ApprovedBy = approver == null ? null : new UserDto
                            {
                                Id = approver.Id,
                                FirstName = approver.FirstName,
                                LastName = approver.LastName,
                                Email = approver.Email
                            },
                            RejectedBy = rejector == null ? null : new UserDto
                            {
                                Id = rejector.Id,
                                FirstName = rejector.FirstName,
                                LastName = rejector.LastName,
                                Email = rejector.Email
                            }
                        };
            return await query.ToListAsync();
        }

        public async Task RejectJoinRequestAsync(Guid requestId, string rejectingUserId)
        {
            var request = await context.JoinRequests.FindAsync(requestId)
          ?? throw new ResourceNotFoundException("Join request", requestId);

            request.Reject(rejectingUserId);
            await context.SaveChangesAsync();
        }
    }
}
