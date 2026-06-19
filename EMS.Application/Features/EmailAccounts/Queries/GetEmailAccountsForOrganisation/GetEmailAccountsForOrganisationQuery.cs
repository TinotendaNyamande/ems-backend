using EMS.Application.Dtos.EmailAccounts;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountsForOrganisation
{
    public class GetEmailAccountsForOrganisationQuery(Guid organisationId):IRequest<IEnumerable<EmailAccountDto>>
    {
        public Guid OrganisationId { get; init; } = organisationId;
    }
}
