using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Application.Common.Enums;
using EFCore.Domain.Common.ValueObject;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static EFCore.WebAPI.Common.Middlewares.ExceptionMiddleware;

namespace EFCore.WebAPI.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
[SwaggerResponse((int) System.Net.HttpStatusCode.NotFound)]
[SwaggerResponse((int) System.Net.HttpStatusCode.Forbidden)]
[SwaggerResponse((int) System.Net.HttpStatusCode.BadRequest, Type = typeof(List<ValidationFailure>))]
[SwaggerResponse((int) System.Net.HttpStatusCode.InternalServerError, Type = typeof(ExceptionResponse))]
public class ApiController: Controller
{
    protected readonly ILogger<ApiController> _logger;
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

    public ApiController(ILogger<ApiController> logger)
    {
        _logger = logger;   
    }

    [NonAction]
    protected ActionResult<T> GetReturn<T>(ResponseCommand response, bool hasContent = true)
    {
        switch (response.Status)
        {
            case ResponseStatusCommand.NotFound:
                return NotFound();
            case ResponseStatusCommand.Forbidden:
                return Forbid();

            case ResponseStatusCommand.Error:
            case ResponseStatusCommand.Exception:
            case ResponseStatusCommand.NotAllowed:
                return BadRequest(response.Errors.FirstOrDefault().Value) ;

            case ResponseStatusCommand.OK:
                if (!hasContent)
                    return NoContent();
                else
                    return (T)response.Result;

            default:
                return NoContent();
        }
    }

    [NonAction]
    protected ActionResult<ResultPaginationVO> GetReturnForPagination(ResponseCommand response)
    {
        switch (response.Status)
        {
            case ResponseStatusCommand.NotFound:
                return NotFound();
            case ResponseStatusCommand.Forbidden:
                return Forbid();

            case ResponseStatusCommand.Error:
            case ResponseStatusCommand.Exception:
            case ResponseStatusCommand.NotAllowed:
                return BadRequest(response.Errors.FirstOrDefault().Value);

            case ResponseStatusCommand.OK:                
                    return (ResultPaginationVO)response.Result;

            default:
                return NoContent();
        }
    }
}
