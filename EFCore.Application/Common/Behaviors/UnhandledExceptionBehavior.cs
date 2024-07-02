using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Domain.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EFCore.Application.Common.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse: ResponseCommand
    {

        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();

            }catch (DomainException ex)
            {
                _logger.LogError($"Request: unhandled Exception type: {ex.GetType().Name} - {request}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request: unhandled Exception type: {ex.GetType().Name} - {request}");
                throw;
            }
        }
    }
}
