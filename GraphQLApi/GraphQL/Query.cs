using GraphQL.Data;
using GraphQLApi.Models;

namespace GraphQLApi.GraphQL;

public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }
}