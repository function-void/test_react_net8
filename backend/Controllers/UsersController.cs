using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UserManagementApp.Application.Features.Users.Commands;
using UserManagementApp.Application.Features.Users.Queries;
using UserManagementApp.Application.Features.Users.Responses;
using UserManagementApp.Application.Helpers;
using UserManagementApp.Application.Services;
using UserManagementApp.Domain.Models;
using UserManagementApp.Presentation;

[ApiVersion(ApiConfigurationSettings.API_ACTUAL_VERSION)]
public class UsersController(IRequestSender sender) : ApiController
{
    private readonly IRequestSender _sender = sender;

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginatedList<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Get(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _sender.ExecuteAsync<GetUsersQuery, PaginatedList<UserDto>>(new GetUsersQuery() { Search = search, PageNumber = pageNumber, PageSize = pageSize}, cancellationToken);
        return TypedResults.Ok(result);
    }

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequest<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Create([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.ExecuteAsync<CreateUserCommand, Result<Guid>>(request, cancellationToken);
        return result.Map<IResult>(onSuccess: result => TypedResults.Created("", result), onFailure: error => TypedResults.BadRequest(error));
    }

    [HttpDelete("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequest<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound<Error>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.ExecuteAsync<DeleteUserCommand, Result>(new DeleteUserCommand(id), cancellationToken);
        return result.Map<IResult>(onSuccess: TypedResults.NoContent, onFailure: error => TypedResults.NotFound(error));
    }

    [HttpPut("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequest<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound<Error>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Update(Guid id, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id != id)
            return TypedResults.BadRequest(new Error(System.Net.HttpStatusCode.BadRequest, "Id mismatch."));

        var result = await _sender.ExecuteAsync<UpdateUserCommand, Result>(request, cancellationToken);
        return result.Map<IResult>(onSuccess: TypedResults.NoContent, onFailure: error => TypedResults.NotFound(error));
    }
}
