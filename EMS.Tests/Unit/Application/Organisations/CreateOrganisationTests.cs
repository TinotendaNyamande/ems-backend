using AutoMapper;
using EMS.Application.Common.Mapping;
using EMS.Application.Features.Organisations.Commands.CreateOrganisation;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EMS.Tests.Unit.Application.Organisations
{
    public class CreateOrganisationTests
    {
        [Fact]
        public async Task Should_Create_Organisation_With_Correct_Name_And_OwnerId()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var user = Substitute.For<IUserService>();
            var handler = new CreateOrganisationHandler(CreateMapper(), repo, user);
            var cancellationToken = new CancellationTokenSource().Token;
            var command = new CreateOrganisationCommand("Test Org", "owner-id");

            var result = await handler.Handle(command, cancellationToken);

            await repo.Received(1).CreateAsync(
                Arg.Is<Organisation>(o =>
                    o.Name == "Test Org" &&
                    o.OwnerId == "owner-id"),
                cancellationToken);
            result.Name.Should().Be("Test Org");
            result.OwnerId.Should().Be("owner-id");
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Assign_Owner_Role_To_Owner()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var user = Substitute.For<IUserService>();
            var handler = new CreateOrganisationHandler(CreateMapper(), repo, user);
            var command = new CreateOrganisationCommand("Test Org", "owner-id");

            await handler.Handle(command, CancellationToken.None);

            await user.Received(1).AssignRoleAsync("Owner", "owner-id");
        }

        [Fact]
        public async Task Should_Add_Owner_To_Created_Organisation()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var user = Substitute.For<IUserService>();
            var handler = new CreateOrganisationHandler(CreateMapper(), repo, user);
            var command = new CreateOrganisationCommand("Test Org", "owner-id");

            var result = await handler.Handle(command, CancellationToken.None);

            await user.Received(1).AddUserToCompanyAsync(result.Id, "owner-id");
        }

        [Fact]
        public async Task Should_Not_Call_UserService_When_Repository_Fails()
        {
            var repo = Substitute.For<IOrganisationRepository>();
            var user = Substitute.For<IUserService>();
            var exception = new Exception("DB error");
            var handler = new CreateOrganisationHandler(CreateMapper(), repo, user);
            var command = new CreateOrganisationCommand("Test Org", "owner-id");

            repo.CreateAsync(Arg.Any<Organisation>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException(exception));

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage("DB error");
            await user.DidNotReceive().AssignRoleAsync(Arg.Any<string>(), Arg.Any<string>());
            await user.DidNotReceive().AddUserToCompanyAsync(Arg.Any<Guid>(), Arg.Any<string>());
        }

        [Fact]
        public void Should_Have_Valid_AutoMapper_Configuration()
        {
            var mapperConfiguration = CreateMapperConfiguration();

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ab")]
        public void Should_Fail_Validation_When_Name_Is_Invalid(string name)
        {
            var validator = new CreateOrganisationValidator();

            var result = validator.TestValidate(new CreateOrganisationCommand(name, "owner-id"));

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_OwnerId_Is_Invalid(string ownerId)
        {
            var validator = new CreateOrganisationValidator();

            var result = validator.TestValidate(new CreateOrganisationCommand("Test Org", ownerId));

            result.ShouldHaveValidationErrorFor(x => x.OwnerId);
        }

        [Fact]
        public void Should_Pass_Validation_When_Command_Is_Valid()
        {
            var validator = new CreateOrganisationValidator();

            var result = validator.TestValidate(new CreateOrganisationCommand("Test Org", "owner-id"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static IMapper CreateMapper()
        {
            return CreateMapperConfiguration().CreateMapper();
        }

        private static MapperConfiguration CreateMapperConfiguration()
        {
            return new MapperConfiguration(
                cfg => cfg.AddProfile<OrganisationMappingProfile>(),
                new LoggerFactory());
        }
    }
}
