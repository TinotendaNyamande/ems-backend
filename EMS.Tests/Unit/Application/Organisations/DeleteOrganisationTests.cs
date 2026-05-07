using EMS.Application.Features.Organisations.Commands.DeleteOrganisationById;
using EMS.Application.Interfaces;
using FluentValidation.TestHelper;
using NSubstitute;

namespace EMS.Tests.Unit.Application.Organisations
{
    public class DeleteOrganisationTests
    {
        [Fact]
        public async Task Can_Delete_Organisation()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var handler = new DeleteOrganisationByIdHandler(repo);
            var ct = new CancellationTokenSource().Token;
            var organisationId = Guid.NewGuid();
            var command = new DeleteOrganisationByIdCommand(organisationId);
            await handler.Handle(command, ct);
            await repo.Received(1).DeleteAsync(
                Arg.Is<Guid>(id => id == organisationId)
                );
        }
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task Should_Fail_Validation_When_OrganisationId_IsInvalid(Guid organisationId)
        {
            var validator = new DeleteOrganisationByIdValidator();
            var result = validator.TestValidate(new DeleteOrganisationByIdCommand(organisationId));
            result.ShouldHaveValidationErrorFor(x => x.OrganisationId);
        }
    }
}
