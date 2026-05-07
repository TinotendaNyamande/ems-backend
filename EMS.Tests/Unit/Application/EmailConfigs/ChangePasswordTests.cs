using AutoMapper;
using EMS.Application.Common.Mapping;
using EMS.Application.Dtos.EmailConfigs;
using EMS.Application.Features.EmailConfigs.Commands.ChangePassword;
using EMS.Application.Interfaces;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EMS.Tests.Unit.Application.EmailConfigs
{
    public class ChangePasswordTests
    {
        [Fact]
        public async Task Should_Change_Email_Config_Password()
        {
            var repo = Substitute.For<IEmailConfigurationRepository>();
            var handler = new ChangePasswordHandler(repo, CreateMapper());
            var emailId = Guid.NewGuid();
            var command = new ChangePasswordCommand(emailId, "old-password", "new-password");

            await handler.Handle(command, CancellationToken.None);

            await repo.Received(1).ChangePasswordAsync(
                emailId,
                Arg.Is<ChangePasswordDto>(dto =>
                    dto.OldPassword == "old-password" &&
                    dto.NewPassword == "new-password"));
        }

        [Fact]
        public async Task Should_Propagate_Exception_When_Repository_Fails()
        {
            var repo = Substitute.For<IEmailConfigurationRepository>();
            var handler = new ChangePasswordHandler(repo, CreateMapper());
            var command = new ChangePasswordCommand(Guid.NewGuid(), "old-password", "new-password");
            var exception = new Exception("DB error");

            repo.ChangePasswordAsync(Arg.Any<Guid>(), Arg.Any<ChangePasswordDto>())
                .Returns(Task.FromException(exception));

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage("DB error");
        }

        [Fact]
        public void Should_Fail_Validation_When_EmailId_Is_Empty()
        {
            var validator = new ChangePasswordValidator();

            var result = validator.TestValidate(new ChangePasswordCommand(Guid.Empty, "old-password", "new-password"));

            result.ShouldHaveValidationErrorFor(x => x.EmailId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_OldPassword_Is_Invalid(string oldPassword)
        {
            var validator = new ChangePasswordValidator();

            var result = validator.TestValidate(new ChangePasswordCommand(Guid.NewGuid(), oldPassword, "new-password"));

            result.ShouldHaveValidationErrorFor(x => x.OldPassword);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_NewPassword_Is_Invalid(string newPassword)
        {
            var validator = new ChangePasswordValidator();

            var result = validator.TestValidate(new ChangePasswordCommand(Guid.NewGuid(), "old-password", newPassword));

            result.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }

        [Fact]
        public void Should_Pass_Validation_When_Command_Is_Valid()
        {
            var validator = new ChangePasswordValidator();

            var result = validator.TestValidate(new ChangePasswordCommand(Guid.NewGuid(), "old-password", "new-password"));

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
