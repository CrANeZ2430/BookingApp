FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src
COPY "./BookingApp.API/BookingApp.API.csproj" "./BookingApp.API/"
COPY "./BookingApp.Application/BookingApp.Application.csproj" "./BookingApp.Application/"
COPY "./BookingApp.Core/BookingApp.Core.csproj" "./BookingApp.Core/"
COPY "./BookingApp.Infrastructure/BookingApp.Infrastructure.csproj" "./BookingApp.Infrastructure/"

RUN dotnet restore "./BookingApp.API/BookingApp.API.csproj"

COPY . .

WORKDIR /src/BookingApp.API
RUN dotnet publish "BookingApp.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "BookingApp.API.dll"]