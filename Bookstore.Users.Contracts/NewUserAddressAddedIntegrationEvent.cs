namespace Bookstore.Users.Contracts;

public record NewUserAddressAddedIntegrationEvent(UserAddressDetails details)
    : IntegrationEventBase;
