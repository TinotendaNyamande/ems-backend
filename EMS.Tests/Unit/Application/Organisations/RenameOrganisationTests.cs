using EMS.Application.Features.Organisations.Commands.ChangeOwner;
using EMS.Application.Features.Organisations.Commands.RenameOrganisation;
using EMS.Application.Interfaces;
using FluentValidation.TestHelper;
using NSubstitute;

namespace EMS.Tests.Unit.Application.Organisations
{
    public class RenameOrganisationTests
    {
        [Fact]
        public async Task Can_Rename_Organisation()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var handler = new RenameOrganisationHandler(repo);
            var organisationId = Guid.NewGuid();
            var command = new RenameOrganisationCommand(organisationId, "New-name");
            var ct = new CancellationTokenSource().Token;
            await handler.Handle(command,ct);
            await repo.Received(1).RenameOrganisationAsync(
                Arg.Is<Guid>(id => id == organisationId),
                Arg.Is<string>(newName => newName == "New-name")
                );
        }
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task Should_Fail_Validation_When_OrganisationId_IsInvalid(Guid organisationId)
        {
            var validator = new RenameOrganisationValidator();
            var result = validator.TestValidate(new RenameOrganisationCommand(organisationId, "new-owner-id"));
            result.ShouldHaveValidationErrorFor(x => x.OrganisationId);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Fail_Validation_When_Newname_IsInvalid(string newName)
        {
            var validator = new RenameOrganisationValidator();
            var result = validator.TestValidate(new RenameOrganisationCommand(Guid.NewGuid(), newName));
            result.ShouldHaveValidationErrorFor(x => x.OrganisationNewName);
        }
    }
}
