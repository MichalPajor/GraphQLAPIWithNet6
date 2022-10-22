using GraphQLApi.Models;

namespace GraphQLApi.GraphQL;

public class Subscription
{
    [Subscribe]
    public Platform OnPlatformAdded([EventMessage] Platform platform) => platform; //return platform
    [Subscribe]
    public Platform OnPlatformDeleted([EventMessage] Platform platform) => platform; //return platform
    [Subscribe]
    public Command OnCommandDeleted([EventMessage] Command command) => command; //return command
}