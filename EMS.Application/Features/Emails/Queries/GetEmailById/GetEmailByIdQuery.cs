using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.Emails.Queries.GetEmailById
{
    public record GetEmailByIdQuery(Guid Id):IQuery<Email?>;
}