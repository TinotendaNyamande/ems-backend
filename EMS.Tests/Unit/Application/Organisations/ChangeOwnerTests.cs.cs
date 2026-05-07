using EMS.Application.Features.Organisations.Commands.ChangeOwner;
using EMS.Application.Interfaces;
using FluentValidation.TestHelper;
using NSubstitute;

namespace EMS.Tests.Unit.Application.Organisations
{
    public class ChangeOwnerTests
    {
        [Fact]
        public async Task Can_Change_Owner()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var handler = new ChangeOwnerHandler(repo);
            var guid = Guid.NewGuid();
            var command = new ChangeOwnerCommand(guid, "owner-id");
            var cancellationToken = new CancellationTokenSource().Token;

             await handler.Handle(command,cancellationToken);
             await repo.Received(1).ChangeOwnerAsync(
                Arg.Is<Guid>(id=>id==guid),
                Arg.Is<string>(ownerId=>ownerId=="owner-id")
             );


        }
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task Should_Fail_Validation_When_OrganisationId_IsInvalid(Guid organisationId)
        {
            var validator = new ChangeOwnerValidator();
            var result = validator.TestValidate(new ChangeOwnerCommand(organisationId,"new-owner-id"));
            result.ShouldHaveValidationErrorFor(x=>x.OrganisationId);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Fail_Validation_When_NewownerId_IsInvalid(string newOwnerId)
        {
            var validator = new ChangeOwnerValidator();
            var result = validator.TestValidate(new ChangeOwnerCommand(Guid.NewGuid(), newOwnerId));
            result.ShouldHaveValidationErrorFor(x=>x.NewOwnerId);
        }

    }
}
