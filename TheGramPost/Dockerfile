FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY TheGramPost.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TheGramPost.dll"]
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ServiceConfig__serviceDiscoveryAddress=http://consul:8500
ENV ServiceConfig__serviceAddress=http://the-gram-post:80
ENV ServiceConfig__serviceName=the-gram-post
ENV ServiceConfig__serviceId=the-gram-post-v1
EXPOSE 80