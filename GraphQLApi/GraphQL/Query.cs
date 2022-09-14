using GraphQL.Data;
using GraphQL.Models;

namespace GraphQLApi.GraphQL;

public class Query
{
    public IQueryable<Platform> GetPlatforms([Service] AppDbContext context)
    {
        return context.Platforms;
    }
}