using MediatR;

namespace EMS.Application.Abstractions
{
    public interface ICommandBase { }
    public interface ICommand : ICommandBase, IRequest { }
    public interface ICommand<out TResponse> : ICommandBase, IRequest<TResponse> { }
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
    { }

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    { }
}