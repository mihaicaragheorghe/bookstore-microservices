FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/ .
COPY nuget.config /nuget.config
RUN dotnet restore Microservices/Books.Microservice/Books.Microservice.csproj

FROM build AS publish
WORKDIR /src/Microservices/Books.Microservice
RUN dotnet publish Books.Microservice.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books.Microservice.dll"]
