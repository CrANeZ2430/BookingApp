using System.Collections.Immutable;
using System.Text.Json.Serialization;
using BookingApp.API.Conventions;
using BookingApp.API.ExceptionHandling;
using BookingApp.API.Extensions;
using BookingApp.Application;
using BookingApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Scalar.AspNetCore;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IExceptionMapper, ExceptionMapper>();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
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
                http.Token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyQGNsaWVudHMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDc5IiwiaWF0IjoxNzgyMzIyMjI2LCJleHAiOjE3ODI0MDg2MjYsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyIsImF6cCI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyIn0.JdFSl_u0f0i5Hg5trREsnG4FgThsjJ0MrVU157EHWhowcsIh0B53fx5DcbEXKSRc4HpuqxAAMk04pL2QaBAVUUQV0KZ-EBSIZwL90E0HqJTq5vwMks7ULKSTIL7c1Sdr04sgQqpHXyumTzpO60hsBluTlIuxdJAqoU2Wo5LVDdy21VUZh0WsOYesPjYqOG-ufwVID0e5mPxWkG09fIRGvy9uFzsXNHBEqCk1EaDOrrvFtsQQffWbZAZaqjkyQFiNnHEPrcS-cCroJNFqYGqdOPAyR4_WH6aS_Ct5Dw82XXkC_IqV4cq9FjTFae5iwJVNTm91PDZzVbnmgJh7LvEpJg";
            });
    });
}

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();