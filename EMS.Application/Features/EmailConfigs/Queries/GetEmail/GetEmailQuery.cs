using EMS.Application.Dtos.EmailConfigs;
using MediatR;

namespace EMS.Application.Features.EmailConfigs.Queries.GetEmail
{
    public class GetEmailQuery(Guid id):IRequest<EmailConfigDto>
    {
        public Guid Id { get; init; } = id;
    }
}
