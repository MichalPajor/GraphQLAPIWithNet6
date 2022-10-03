using GraphQL.Data;
using GraphQLApi.Models;

namespace GraphQLApi.GraphQL.Commands;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represent any executable command");
        descriptor
            .Field(p=>p.Platform)
            .ResolveWith<Resolvers>(p=>p.GetPlatform(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the platform where you can use this command");
    }

    private class Resolvers
    {
        public Platform GetPlatform([Parent]Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(p=>p.Id == command.PlatformId);
        }
    }
}