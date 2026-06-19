using AutoMapper;
using EMS.Application.Common.Mapping;
using EMS.Application.Features.EmailAccounts.Commands.CreateEmailAccount;
using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using Microsoft.Extensions.Logging;
using NSubstitute;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EMS.Tests.Unit.Application.EmailConfigs
{
    public class CreateEmailConfigTests
    {
        [Theory]
        [InlineData(EmailType.Gmail)]
        [InlineData(EmailType.Outlook)]
        [InlineData(EmailType.Office365)]
        public async Task Can_Create_Email_Config (EmailType emailType)
        {
            var repo = Substitute.For<IEmailAccountRepository>();
            var encryptionService = Substitute.For<IEncryptionService>();
            encryptionService.EncryptData(Arg.Any<string>()).Returns(x => (string)x[0]);
            var hander = new CreateEmailAccountHandler(repo, CreateMapper(), encryptionService);
            var organisationId = Guid.NewGuid();
            var command = new CreateEmailAccountCommand("test@gmail.com",emailType,organisationId,"password","clientId","clientsecret","tenantId");
            var ct = new CancellationTokenSource().Token;
            var result =await hander.Handle(command,ct);
            await repo.Received(1).CreateEmailAccountAsync(
                Arg.Is<EmailAccount>(
                    emailAccount =>
                    emailAccount.OrganisationId == organisationId &&
                    emailAccount.EmailType == emailType &&
                    emailAccount.EmailAddress == "test@gmail.com"

                    )
                );
        }

        [Theory]
        [InlineData(EmailType.Gmail)]
        [InlineData(EmailType.Outlook)]
        public async Task Task_Uses_Password_For_Basic_Email_Types(EmailType emailType)
        {
            var repo = Substitute.For<IEmailAccountRepository>();
            var encryptionService = Substitute.For<IEncryptionService>();
            encryptionService.EncryptData(Arg.Any<string>()).Returns(x => (string)x[0]);
            var hander = new CreateEmailAccountHandler(repo, CreateMapper(), encryptionService);
            var organisationId = Guid.NewGuid();
            var command = new CreateEmailAccountCommand("test@gmail.com", emailType,organisationId, "password", "", "", "" );
            var ct = new CancellationTokenSource().Token;
            var result = await hander.Handle(command, ct);
            await repo.Received(1).CreateEmailAccountAsync(
                Arg.Is<EmailAccount>(
                    emailAccount =>
                    emailAccount.OrganisationId == organisationId &&
                    emailAccount.EmailType == emailType &&
                    emailAccount.EmailAddress == "test@gmail.com" &&
                    emailAccount.Password == "password"

                    )
                );
            
            result.Password.Should().Be("password");
            result.EmailAddress.Should().Be("test@gmail.com");
            result.Id.Should().NotBeEmpty();
        }
        [Theory]
        [InlineData(EmailType.Office365)]
        public async Task Task_Uses_OAuth_For_Office_365(EmailType emailType)
        {
            var repo = Substitute.For<IEmailAccountRepository>();
            var encryptionService = Substitute.For<IEncryptionService>();
            encryptionService.EncryptData(Arg.Any<string>()).Returns(x => (string)x[0]);
            var hander = new CreateEmailAccountHandler(repo, CreateMapper(), encryptionService);
            var organisationId = Guid.NewGuid();
            var command = new CreateEmailAccountCommand("test@gmail.com", emailType,  organisationId,"", "clientId", "clientsecret", "tenantId");
            var ct = new CancellationTokenSource().Token;
            var result = await hander.Handle(command, ct);
            await repo.Received(1).CreateEmailAccountAsync(
                Arg.Is<EmailAccount>(
                    emailAccount =>
                    emailAccount.OrganisationId == organisationId &&
                    emailAccount.EmailType == emailType &&
                    emailAccount.EmailAddress == "test@gmail.com" &&
                    emailAccount.ClientId == "clientId" &&
                    emailAccount.ClientSecret == "clientsecret" &&
                    emailAccount.TenantId == "tenantId"

                    )
                );

            result.ClientId.Should().Be("clientId");
            result.ClientSecret.Should().Be("clientsecret");
            result.TenantId.Should().Be("tenantId");
            result.EmailAddress.Should().Be("test@gmail.com");
            result.Id.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("not-an-email")]
        [InlineData("test@")]
        public void Should_Fail_Validation_When_EmailAddress_Is_Invalid(string emailAddress)
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(CreateValidCommand(emailAddress: emailAddress));

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void Should_Fail_Validation_When_OrganisationId_Is_Empty()
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(CreateValidCommand(organisationId: Guid.Empty));

            result.ShouldHaveValidationErrorFor(x => x.OrganisationId);
        }

        [Fact]
        public void Should_Fail_Validation_When_EmailType_Is_Not_Supported()
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(CreateValidCommand(emailType: (EmailType)999));

            result.ShouldHaveValidationErrorFor(x => x.EmailType);
        }

        [Theory]
        [InlineData(EmailType.Gmail)]
        [InlineData(EmailType.Outlook)]
        [InlineData(EmailType.Custom)]
        public void Should_Fail_Validation_When_Password_Email_Type_Has_No_Password(EmailType emailType)
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(CreateValidCommand(emailType: emailType, password: ""));

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Theory]
        [InlineData(EmailType.Gmail)]
        [InlineData(EmailType.Outlook)]
        [InlineData(EmailType.Custom)]
        public void Should_Pass_Validation_When_Password_Email_Type_Has_Password(EmailType emailType)
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(CreateValidCommand(emailType: emailType, password: "password"));

            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [Theory]
        [InlineData("", "clientsecret", "tenantId")]
        [InlineData("clientId", "", "tenantId")]
        [InlineData("clientId", "clientsecret", "")]
        public void Should_Fail_Validation_When_Office365_OAuth_Input_Is_Missing(
            string clientId,
            string clientSecret,
            string tenantId)
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(
                CreateValidCommand(
                    emailType: EmailType.Office365,
                    password: "",
                    clientId: clientId,
                    clientSecret: clientSecret,
                    tenantId: tenantId));

            if (string.IsNullOrWhiteSpace(clientId))
            {
                result.ShouldHaveValidationErrorFor(x => x.ClientId);
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                result.ShouldHaveValidationErrorFor(x => x.ClientSecret);
            }

            if (string.IsNullOrWhiteSpace(tenantId))
            {
                result.ShouldHaveValidationErrorFor(x => x.TenantId);
            }
        }

        [Fact]
        public void Should_Pass_Validation_When_Office365_OAuth_Input_Is_Complete()
        {
            var validator = new CreateEmailAccountValidator();

            var result = validator.TestValidate(
                CreateValidCommand(
                    emailType: EmailType.Office365,
                    password: "",
                    clientId: "clientId",
                    clientSecret: "clientsecret",
                    tenantId: "tenantId"));

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static IMapper CreateMapper()
        {
            return CreateMapperConfiguration().CreateMapper();
        }

        private static MapperConfiguration CreateMapperConfiguration()
        {
            return new MapperConfiguration(
                cfg => cfg.AddProfile<EmailAccountMappingProfile>(),
                new LoggerFactory());
        }

        private static CreateEmailAccountCommand CreateValidCommand(
            string emailAddress = "test@gmail.com",
            EmailType emailType = EmailType.Gmail,
            Guid? organisationId = null,
            string password = "password",
            string clientId = "",
            string clientSecret = "",
            string tenantId = ""
            
            )
        {
            return new CreateEmailAccountCommand(
                emailAddress,
                emailType,
                organisationId ?? Guid.NewGuid(),
                password,
                clientId,
                clientSecret,
                tenantId
                
                );
        }
    }
}
