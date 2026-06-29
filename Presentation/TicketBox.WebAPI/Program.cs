using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.CQRS.Attendees.Handlers;
using TicketBox.Application.Features.CQRS.Categories.Handlers;
using TicketBox.Application.Interfaces;
using TicketBox.Application.Mappings;
using TicketBox.Persistence.Context;
using TicketBox.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TicketContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(GeneralMappingProfile));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAttendeeRepository, AttendeeRepository>();


builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();

builder.Services.AddScoped<GetAttendeeQueryHandler>();
builder.Services.AddScoped<GetAttendeeByIdQueryHandler>();
builder.Services.AddScoped<CreateAttendeeCommandHandler>();
builder.Services.AddScoped<UpdateAttendeeCommandHandler>();
builder.Services.AddScoped<RemoveAttendeeCommandHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
