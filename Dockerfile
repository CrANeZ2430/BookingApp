FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef

WORKDIR /src
COPY "./BookingApp.API/BookingApp.API.csproj" "./BookingApp.API/"
COPY "./BookingApp.Application/BookingApp.Application.csproj" "./BookingApp.Application/"
COPY "./BookingApp.Core/BookingApp.Core.csproj" "./BookingApp.Core/"
COPY "./BookingApp.Infrastructure/BookingApp.Infrastructure.csproj" "./BookingApp.Infrastructure/"

RUN dotnet restore "./BookingApp.API/BookingApp.API.csproj"

COPY . .

RUN dotnet publish "./BookingApp.API/BookingApp.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "BookingApp.API.dll"]

FROM build AS migrator

WORKDIR /src/BookingApp.API
ENTRYPOINT ["dotnet", "ef", "database", "update"]