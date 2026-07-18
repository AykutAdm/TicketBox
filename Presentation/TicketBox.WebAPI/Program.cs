using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketBox.Application.Features.CQRS.Categories.Handlers;
using TicketBox.Application.Features.CQRS.HeroSliders.Handlers;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Application.Interfaces.Repositories;
using TicketBox.Application.Interfaces.Services;
using TicketBox.Application.Mappings;
using TicketBox.Domain.Entities;
using TicketBox.Infrastructure.Auth;
using TicketBox.Infrastructure.Email;
using TicketBox.Infrastructure.ImageGeneration;
using TicketBox.Persistence.Context;
using TicketBox.Persistence.Repositories;
using TicketBox.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<TicketContext>()
.AddDefaultTokenProviders();


// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
    };
});


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateEventCommand).Assembly);
});


//Validation
builder.Services.AddValidatorsFromAssembly(typeof(CreateEventCommand).Assembly);


builder.Services.AddDbContext<TicketContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(GeneralMappingProfile));


//Repository
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IHeroSliderRepository, HeroSliderRepository>();
builder.Services.AddScoped<IUserDashboardRepository, UserDashboardRepository>();

//Service
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ITicketImageService, TicketImageService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//CQRS
builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();

builder.Services.AddScoped<GetHeroSliderQueryHandler>();
builder.Services.AddScoped<GetHeroSliderByIdQueryHandler>();
builder.Services.AddScoped<CreateHeroSliderCommandHandler>();
builder.Services.AddScoped<UpdateHeroSliderCommandHandler>();
builder.Services.AddScoped<RemoveHeroSliderCommandHandler>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
