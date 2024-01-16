FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/ .
COPY nuget.config /nuget.config
RUN dotnet restore Microservices/Identity.Microservice/Identity.Microservice.csproj

FROM build AS publish
WORKDIR /src/Microservices/Identity.Microservice
RUN dotnet publish Identity.Microservice.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Microservice.dll"]
