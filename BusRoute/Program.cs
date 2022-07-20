using CollegeERPSystem.BusRoute.Domain;
using CollegeERPSystem.BusRoute.Grpc;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using SharedServices.Domain;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConn")));
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
builder.Services.AddTransient<BusRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapGrpcService<BusServiceGrpcImplementation>();
app.UseHttpsRedirection();
app.Run();
