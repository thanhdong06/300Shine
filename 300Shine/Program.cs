using _300Shine.DataAccessLayer.DBContext;

using _300Shine.Repository.Repositories.Service;
using _300Shine.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("local")));
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostGresServer")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
    builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceEntityService, ServiceEntityService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlobalMind API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
