using AutoMapper;
using EMS.Application.Common.Mapping;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Features.EmailConfigs.Commands.ChangeClientSecret;
using EMS.Application.Interfaces;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EMS.Tests.Unit.Application.EmailConfigs
{
    public class ChangeClientSecretTests
    {
        [Fact]
        public async Task Should_Change_Email_Config_Client_Secret()
        {
            var repo = Substitute.For<IEmailConfigurationRepository>();
            var handler = new ChangeClientSecretHandler(repo, CreateMapper());
            var emailId = Guid.NewGuid();
            var command = new ChangeClientSecretCommand(emailId, "old-secret", "new-secret");

            await handler.Handle(command, CancellationToken.None);

            await repo.Received(1).ChangeApplicationSecretAsync(
                emailId,
                Arg.Is<ChangeClientSecretDto>(dto =>
                    dto.OldSecret == "old-secret" &&
                    dto.NewSecret == "new-secret"));
        }

        [Fact]
        public async Task Should_Propagate_Exception_When_Repository_Fails()
        {
            var repo = Substitute.For<IEmailConfigurationRepository>();
            var handler = new ChangeClientSecretHandler(repo, CreateMapper());
            var command = new ChangeClientSecretCommand(Guid.NewGuid(), "old-secret", "new-secret");
            var exception = new Exception("DB error");

            repo.ChangeApplicationSecretAsync(Arg.Any<Guid>(), Arg.Any<ChangeClientSecretDto>())
                .Returns(Task.FromException(exception));

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage("DB error");
        }

        [Fact]
        public void Should_Fail_Validation_When_EmailId_Is_Empty()
        {
            var validator = new ChangeClientSecretValidator();

            var result = validator.TestValidate(new ChangeClientSecretCommand(Guid.Empty, "old-secret", "new-secret"));

            result.ShouldHaveValidationErrorFor(x => x.EmailId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_OldSecret_Is_Invalid(string oldSecret)
        {
            var validator = new ChangeClientSecretValidator();

            var result = validator.TestValidate(new ChangeClientSecretCommand(Guid.NewGuid(), oldSecret, "new-secret"));

            result.ShouldHaveValidationErrorFor(x => x.OldSecret);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_NewSecret_Is_Invalid(string newSecret)
        {
            var validator = new ChangeClientSecretValidator();

            var result = validator.TestValidate(new ChangeClientSecretCommand(Guid.NewGuid(), "old-secret", newSecret));

            result.ShouldHaveValidationErrorFor(x => x.NewSecret);
        }

        [Fact]
        public void Should_Pass_Validation_When_Command_Is_Valid()
        {
            var validator = new ChangeClientSecretValidator();

            var result = validator.TestValidate(new ChangeClientSecretCommand(Guid.NewGuid(), "old-secret", "new-secret"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static IMapper CreateMapper()
        {
            return new MapperConfiguration(
                cfg => cfg.AddProfile<EmailConfigMappingProfile>(),
                new LoggerFactory())
                .CreateMapper();
        }
    }
}
