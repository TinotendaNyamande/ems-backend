using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailTasks.Commands.AddNotes
{
    internal class AddNotesHandler(IEmailTasksRepository tasksRepository) : IRequestHandler<AddNotesCommand>
    {
        public async Task Handle(AddNotesCommand command,CancellationToken cancellation)
        {
            await tasksRepository.EditAdditionalInformation(command.Id,command.AdditionalInfo);
        }
    }
}