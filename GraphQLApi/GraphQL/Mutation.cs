using GraphQL.Data;
using GraphQLApi.GraphQL.Commands;
using GraphQLApi.GraphQL.Platforms;
using GraphQLApi.Models;
using HotChocolate.Subscriptions;

namespace GraphQLApi.GraphQL;


public class Mutation
{
    [UseDbContext(typeof(AppDbContext))] //to use multithreaded dbContext
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
    {
        var platform = new Platform
        {
            Name = input.Name
        };

        context.Platforms.Add(platform);
        await context.SaveChangesAsync(cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

        return new AddPlatformPayload(platform);
    }

    [UseDbContext(typeof(AppDbContext))]
    public async Task<DeletePlatformPayload> DeletePlatformAsync(DeletePlatformInput input, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
    {
        var platform = context.Platforms.Where(p=>p.Id.Equals(input.Id)).FirstOrDefault();
        if(platform != null){
            context.Platforms.Remove(platform);
            await context.SaveChangesAsync();

            await eventSender.SendAsync(nameof(Subscription.OnPlatformDeleted), platform, cancellationToken);
        }

        return new DeletePlatformPayload(platform);
    }

    [UseDbContext(typeof(AppDbContext))] //to use multithreaded dbContext
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
    {
        var command = new Command
        {
            HowTo = input.HowTo,
            CommandLine = input.CommandLine,
            PlatformId = input.PlatformId
        };

        context.Commands.Add(command);
        await context.SaveChangesAsync();

        return new AddCommandPayload(command);
    }

    [UseDbContext(typeof(AppDbContext))]
    public async Task<DeleteCommandPayload> DeleteCommandAsync(DeleteCommandInput input, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
    {
        var command = context.Commands.Where(p=>p.Id.Equals(input.Id)).FirstOrDefault();
        if(command != null){
            context.Commands.Remove(command);
            await context.SaveChangesAsync();

            await eventSender.SendAsync(nameof(Subscription.OnCommandDeleted), command, cancellationToken);
        }

        return new DeleteCommandPayload(command);
    }
}