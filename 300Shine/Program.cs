using _300Shine.Configuration;
using _300Shine.DataAccessLayer.DBContext;
using _300Shine.Repository;
using _300Shine.Repository.Interface;
using _300Shine.Repository.Repositories.Salon;
using _300Shine.Repository.Repositories.Service;
using _300Shine.Service;
using _300Shine.Service.Interface;
using _300Shine.Service.Salons;
using _300Shine.Repository;
using _300Shine.Repository.Interface;
using _300Shine.Repository.Repositories.Service;
using _300Shine.Service;
using _300Shine.Service.Interface;
using _300Shine.Service.Services;
using Microsoft.EntityFrameworkCore;
using _300Shine.Repository.Repositories.Stylist;
using _300Shine.Service.Stylists;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();
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
builder.Services.AddScoped<ISalonRepository, SalonRepository>();
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStylistRepository, StylistRepository>();
builder.Services.AddScoped<IStylistService, StylistService>();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "300Shine API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
