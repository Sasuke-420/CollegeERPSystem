using CollegeERPSystem.Services.Domain;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Client;
using SharedServices.Domain;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbSettings>();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConn")));
builder.Services.ConfigureRepoServices();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
builder.Services.AddSwaggerGen(option =>
{
    OpenApiInfo openApiInfo = new OpenApiInfo
    {
        Title = "CollegeERP",
        Version = "v1"
    };

    option.SwaggerDoc(
      name: "v1",
      info: openApiInfo);

    option.MapType(typeof(TimeSpan), () => new OpenApiSchema
    {
        Type = "string"
    });
});

/*var channel =  GrpcChannel.ForAddress("https://localhost:7031");
var client = channel.CreateGrpcService<IBusService>();

var result = await client.GetBusRoute();
Console.WriteLine(result.ToList()[0].Name);*/

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint(
                       url: "/swagger/v1/swagger.json",
                       name: "CollegeERP.Services v1"));
}
    app.UseHttpsRedirection();
app.MapControllers();
app.Run();






















/*services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString, builderOptions =>
    {
        builderOptions.EnableRetryOnFailure(maxRetryCount: retryCount,
            maxRetryDelay: TimeSpan.FromSeconds(retryDelay),
            errorCodesToAdd);

        if (migrationAssembly != null)
            builderOptions.MigrationsAssembly(migrationAssembly);
    })
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}, ServiceLifetime.Scoped);*/