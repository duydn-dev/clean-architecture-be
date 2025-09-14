using Shared.Infrastructure.Identity;
using Shared.Infrastructure.Extentions;

namespace CleanArchitecture.System.PublicApi.EndPoints;

public class Users : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapIdentityApi<ApplicationUser>();
    }
}