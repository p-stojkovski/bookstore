using Ardalis.Result;
using Bookstore.Users.Contracts;
using Bookstore.Users.UseCases.User.GetById;
using MediatR;

namespace Bookstore.Users.Integrations;

internal class UserDetailsByIdHandler : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
    private readonly IMediator _mediator;

    public UserDetailsByIdHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.UserId);

        var result = await _mediator.Send(query);
        if(!result.IsSuccess)
        {
            return Result.NotFound();
        }

        return result.Map(x => new UserDetails(x.UserId, x.EmailAddress));
    }
}
