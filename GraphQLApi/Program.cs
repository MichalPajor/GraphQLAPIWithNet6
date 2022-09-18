using GraphQL.Data;
using GraphQL.Server.Ui.Voyager;
using GraphQLApi.GraphQL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Build docker connection string with secrets
var sqlConnBuilder = new SqlConnectionStringBuilder();
sqlConnBuilder.ConnectionString = builder.Configuration.GetConnectionString("DockerConnection");
sqlConnBuilder.UserID = builder.Configuration["UserId"];
sqlConnBuilder.Password = builder.Configuration["Password"];

//Add db context
builder.Services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(sqlConnBuilder.ConnectionString));
builder.Services.AddGraphQLServer().AddQueryType<Query>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseEndpoints(endpoints =>{
    endpoints.MapGraphQL();
});
app.UseGraphQLVoyager("/graphql-voyager",new GraphQL.Server.Ui.Voyager.VoyagerOptions(){
    GraphQLEndPoint = "/graphql"
});

app.UseHttpsRedirection();

app.Run();
