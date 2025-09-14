using Microsoft.AspNetCore.Routing;

namespace Shared.Infrastructure.Extentions;

public abstract class EndpointGroupBase
{
    public virtual string? GroupName { get; }
    public abstract void Map(RouteGroupBuilder groupBuilder);
}
