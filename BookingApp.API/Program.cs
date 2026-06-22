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
                http.Token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyQGNsaWVudHMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDc5IiwiaWF0IjoxNzgxOTY4NDA3LCJleHAiOjE3ODIwNTQ4MDcsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyIsImF6cCI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyIn0.M1WtAPbCe0QyfL-nyQija4XBsgAAk4bZrC4RXh9iNSghxkDAE9cTeMm47FMql4_pvqbPHXZxFY2KJRKkeuZkgSpxvgh3R_0WzMSoPSEfq36P9TrhRxiH9cqMvkhiJMLRkr3M84STz2_DqEkoyju5mv-OPKJrg4KqzpuwxqFk5D2ZxMsqRQJ5ll56hyxInvmadZuRqMueDSyTolC2o8qJpRQ80WxtKhvqldrK18-4_Xqc0JBn_ovAkLyJYua9vuxuwU-iCIqD9hdTFYcg-OleqjA89DPoD2SuGl2c1B0YXH2IK195rqmcD3yrA4KqrAHES-tNSBDS49oVUfp4QXcqEg";
            });
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();