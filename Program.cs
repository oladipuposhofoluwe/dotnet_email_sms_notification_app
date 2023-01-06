using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotificationApp.Context;
using NotificationApp.Controller;
using NotificationApp.Services.EmailService;
using NotificationApp.Services.NotificationService;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<ISendNotification, SendNotifications>();
builder.Services.AddScoped<IAuthentication, Authentication>();



var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
var smsConfig = builder.Configuration.GetSection("TwilloConfig").Get<TwilloConfig>();

builder.Services.AddSingleton(smsConfig);

builder.Services.AddSingleton(emailConfig);

builder.Services.AddControllers();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=> 
// {
//     // options.TokenValidationParameters = new TokenValidationParameters()
//     // {
//     //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
//     // };
// });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(sw => 
        sw.SwaggerDoc("v1", 
        new OpenApiInfo {Title = "dot6JWTAuthentication", Version = "1.0"}));

builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
                In = ParameterLocation.Header,
                Description = "Insert JWT Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
        });
// builder.Services.AddSwaggerGen(w => 
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference =new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new String[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

