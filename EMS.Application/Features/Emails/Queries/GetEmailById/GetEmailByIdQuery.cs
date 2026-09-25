using EMS.Application.Abstractions;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Queries.GetEmailById
{
    public record GetEmailByIdQuery(Guid Id):ICommand<Email>;
}