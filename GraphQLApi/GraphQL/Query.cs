using GraphQL.Data;
using GraphQL.Models;

namespace GraphQLApi.GraphQL;

public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }
}