using Ardalis.Result;
using Bookstore.Users.Interfaces;
using MediatR;

namespace Bookstore.Users.UseCases.User.GetById;

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IApplicationUserRepository _userRepository;

    public GetUserByIdQueryHandler(IApplicationUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        if(user is null)
        {
            return Result.NotFound();
        }

        return new UserDto(Guid.Parse(user.Id), user.Email!);
    }
}
