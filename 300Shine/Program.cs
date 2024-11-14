using _300Shine.Configuration;
using _300Shine.DataAccessLayer.DBContext;
using _300Shine.Repository.Repositories.Salon;
using _300Shine.Repository.Repositories.Service;
using _300Shine.Service.Salons;
using _300Shine.Service.Services;
using Microsoft.EntityFrameworkCore;
using _300Shine.Repository.Repositories.Stylist;
using _300Shine.Service.Stylists;
using _300Shine.Repository.Repositories.Manager;
using _300Shine.Service.Manager;
using _300Shine.Service.Slots;
using _300Shine.Repository.Repositories.Slot;
using _300Shine.Repository.Repositories.User;
using _300Shine.Repository.Repositories.Appoinment;
using _300Shine.Repository.Repositories.Shift;
using _300Shine.Service.Appoinments;
using _300Shine.Service.Users;
using _300Shine.Service.Shifts;
using _300Shine.Service.SMS;
using _300Shine.Service.UploadImage;
using Service.Password;
using _300Shine.Repository.Repositories.Style;
using _300Shine.Service.Styles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options =>
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

// Configuration Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceEntityService, ServiceEntityService>();
builder.Services.AddScoped<ISalonRepository, SalonRepository>();
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddScoped<IStylistRepository, StylistRepository>();
builder.Services.AddScoped<IStylistService, StylistService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<IUploadImageService, UploadImageService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IStyleRepository, StyleRepository>();
builder.Services.AddScoped<IStyleService, StyleService>();

// Configuration for background method
builder.Services.AddHostedService<ShiftHostedService>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
