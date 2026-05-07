using EMS.Application.Dtos.Auth;
using EMS.Application.Features.Auth.Commands.Login;
using EMS.Application.Interfaces;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;

namespace EMS.Tests.Unit.Application.Auth
{
    public class LoginTests
    {
        [Fact]
        public async Task Should_Login_User()
        {
            var authService = Substitute.For<IAuthService>();
            var handler = new LoginHandler(authService);
            var command = new LoginCommand("test@example.com", "password");
            var expected = new AuthResponseDto("token", "test@example.com", "user-id", "refresh-token");

            authService.LoginAsync(Arg.Any<LoginUserDto>())
                .Returns(expected);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeSameAs(expected);
            await authService.Received(1).LoginAsync(
                Arg.Is<LoginUserDto>(dto =>
                    dto.Email == "test@example.com" &&
                    dto.Password == "password"));
        }

        [Fact]
        public async Task Should_Propagate_Unauthorized_When_Login_Fails()
        {
            var authService = Substitute.For<IAuthService>();
            var handler = new LoginHandler(authService);
            var command = new LoginCommand("test@example.com", "wrong-password");

            authService.LoginAsync(Arg.Any<LoginUserDto>())
                .Returns(Task.FromException<AuthResponseDto>(new UnauthorizedAccessException("Invalid credentials")));

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid credentials");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("not-an-email")]
        public void Should_Fail_Validation_When_Email_Is_Invalid(string email)
        {
            var validator = new LoginValidator();

            var result = validator.TestValidate(new LoginCommand(email, "password"));

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_Password_Is_Invalid(string password)
        {
            var validator = new LoginValidator();

            var result = validator.TestValidate(new LoginCommand("test@example.com", password));

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Pass_Validation_When_Command_Is_Valid()
        {
            var validator = new LoginValidator();

            var result = validator.TestValidate(new LoginCommand("test@example.com", "password"));

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
