using EMS.Application.Dtos.Auth;
using EMS.Application.Features.Auth.Commands.Register;
using EMS.Application.Interfaces;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;

namespace EMS.Tests.Unit.Application.Auth
{
    public class RegisterTests
    {
        [Fact]
        public async Task Should_Register_User()
        {
            var authService = Substitute.For<IAuthService>();
            var handler = new RegisterHandler(authService);
            var command = new RegisterCommand("Tino", "Nyamande", "test@example.com", "pass");
            var expected = new AuthResponseDto("token", "test@example.com", "user-id", "refresh-token");

            authService.RegisterAsync(Arg.Any<RegisterUserDto>())
                .Returns(expected);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeSameAs(expected);
            await authService.Received(1).RegisterAsync(
                Arg.Is<RegisterUserDto>(dto =>
                    dto.FirstName == "Tino" &&
                    dto.LastName == "Nyamande" &&
                    dto.Email == "test@example.com" &&
                    dto.Password == "pass"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_FirstName_Is_Invalid(string firstName)
        {
            var validator = new RegisterValidator();

            var result = validator.TestValidate(CreateValidCommand(firstName: firstName));

            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Fail_Validation_When_LastName_Is_Invalid(string lastName)
        {
            var validator = new RegisterValidator();

            var result = validator.TestValidate(CreateValidCommand(lastName: lastName));

            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("not-an-email")]
        public void Should_Fail_Validation_When_Email_Is_Invalid(string email)
        {
            var validator = new RegisterValidator();

            var result = validator.TestValidate(CreateValidCommand(email: email));

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        public void Should_Fail_Validation_When_Password_Is_Invalid(string password)
        {
            var validator = new RegisterValidator();

            var result = validator.TestValidate(CreateValidCommand(password: password));

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Pass_Validation_When_Command_Is_Valid()
        {
            var validator = new RegisterValidator();

            var result = validator.TestValidate(CreateValidCommand());

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static RegisterCommand CreateValidCommand(
            string firstName = "Tino",
            string lastName = "Nyamande",
            string email = "test@example.com",
            string password = "pass")
        {
            return new RegisterCommand(firstName, lastName, email, password);
        }
    }
}
