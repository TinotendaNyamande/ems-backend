using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Features.Organisations.Commands.CreateOrganisation;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace EMS.Tests;

public class CreateOrganisationTests
{
    [Fact]
    public async Task Should_Create_Organisation()
    {
        var repo = Substitute.For<IOrganisationRepository>();
        var mapper = Substitute.For<IMapper>();
        var user = Substitute.For<IUserService>();

        var handler = new CreateOrganisationHandler(mapper, repo, user);

        var command = new CreateOrganisationCommand("Test Org", "user-1");

        var organisation = new Organisation("user-1", "Test Org");

        var organisationDto = new OrganisationDto
        {
            Id = organisation.Id,
            Name = organisation.Name,
            OwnerId = organisation.OwnerId
        };

        mapper.Map<Organisation>(command).Returns(organisation);

        mapper.Map<OrganisationDto>(organisation).Returns(organisationDto);
        var ct = new CancellationToken();
        repo.CreateAsync(organisation , ct).Returns(Task.CompletedTask);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test Org");
    }
}
