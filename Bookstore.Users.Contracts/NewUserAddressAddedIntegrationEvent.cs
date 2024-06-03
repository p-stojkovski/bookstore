using Bookstore.SharedKernel;

namespace Bookstore.Users.Contracts;

public record NewUserAddressAddedIntegrationEvent(UserAddressDetails Details)
    : IntegrationEventBase;
