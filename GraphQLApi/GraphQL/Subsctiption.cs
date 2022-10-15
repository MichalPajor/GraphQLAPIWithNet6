using GraphQLApi.Models;

namespace GraphQLApi.GraphQL;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Platform OnPlatformAdded([EventMessage] Platform platform) => platform;
}