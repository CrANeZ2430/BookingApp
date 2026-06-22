using System.Collections.Immutable;
using System.Text.Json.Serialization;
using BookingApp.API.Conventions;
using BookingApp.API.ExceptionHandling;
using BookingApp.API.Extensions;
using BookingApp.Application;
using BookingApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IExceptionMapper, ExceptionMapper>();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
});

builder.Services.AddAuthorization();

builder.Services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

builder.Services.AddControllers(options =>
        options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer())))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

app.UseStatusCodePages();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Booking API")
            .WithTheme(ScalarTheme.DeepSpace)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .AddPreferredSecuritySchemes("Bearer")
            .AddHttpAuthentication("Bearer", http =>
            {
                http.Token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyQGNsaWVudHMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDc5IiwiaWF0IjoxNzgyMTM2ODg2LCJleHAiOjE3ODIyMjMyODYsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyIsImF6cCI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyIn0.caRJH4fQnGpRhN4sRcWC_uE0v7INVJJWYA-2WS3ECr0rdmZV6-G9gI-IpESayRKGtHuCFOOUKwXbgWfrxE47VD_VJlUaV_yt6mO1Xd5vabky1UVLLdnImJ4rYRLEqeJ0rJECIYI4jYZn6nySQ4N7lRAfhBLALajBtLB3EE5c9HgRfzQ3Zq4unPX6DdHEoYfLpRsiRp-RMeHD8Ccyq6VDwtmlgLQFqH2kuR6kKb1v1IF9ntNwvAmdjP1TRhuKPBZQ__kGOdbOtGkSqQALP08vByD4TCQbRO6-lXIK6XAfIT2osMTv5uJdfbO0c1i8QIIgCqekAjcdgkcQZkpp7JWN_A";
            });
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();