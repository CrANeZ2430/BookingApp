FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore-copy

WORKDIR /src
COPY "./BookingApp.API/BookingApp.API.csproj" "./BookingApp.API/"
COPY "./BookingApp.Application/BookingApp.Application.csproj" "./BookingApp.Application/"
COPY "./BookingApp.Core/BookingApp.Core.csproj" "./BookingApp.Core/"
COPY "./BookingApp.Infrastructure/BookingApp.Infrastructure.csproj" "./BookingApp.Infrastructure/"

RUN dotnet restore "./BookingApp.API/BookingApp.API.csproj"

WORKDIR /src
COPY . .

FROM restore-copy AS build

RUN dotnet publish "./BookingApp.API/BookingApp.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "BookingApp.API.dll"]

FROM restore-copy AS migrator-build

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet ef migrations bundle -s "./BookingApp.API/BookingApp.API.csproj" -p "./BookingApp.Infrastructure/BookingApp.Infrastructure.csproj" -o /src/EFBundle/efbundle

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS migrator

ARG POSTGRES_DB
ARG POSTGRES_USER
ARG POSTGRES_PASSWORD

ENV POSTGRES_DB=${POSTGRES_DB}
ENV POSTGRES_USER=${POSTGRES_USER}
ENV POSTGRES_PASSWORD=${POSTGRES_PASSWORD}

WORKDIR /app
COPY --from=migrator-build "/src/EFBundle/efbundle" "./efbundle"
RUN chmod +x ./efbundle

ENTRYPOINT exec "./efbundle" "--connection" "Host=database;Port=5432;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}"