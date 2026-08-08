using System.Text.Json.Serialization;
using BookingApp.API.Conventions;
using BookingApp.API.ExceptionHandling;
using BookingApp.Application;
using BookingApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var  myAllowSpecificOrigins = "myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IExceptionMapper, ExceptionMapper>();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader();;
        });
});

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
});

builder.Services.AddAuthorization();

builder.Services.AddControllers(options =>
        options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer())))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

app.UseStatusCodePages();

//app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();