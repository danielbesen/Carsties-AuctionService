FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 8080

# copy all .csproj files and restore as distinct layers. 
# use of the same COPY command for every dockerfile in the project to take advantage of docker caching
COPY Carsties.sln Carsties.sln
COPY AuctionService/AuctionService.csproj AuctionService/AuctionService.csproj
COPY SearchService/SearchService.csproj SearchService/SearchService.csproj
COPY GatewayService/GatewayService.csproj GatewayService/GatewayService.csproj
COPY Contracts/Contracts.csproj Contracts/Contracts.csproj
COPY IdentityService/IdentityService.csproj IdentityService/IdentityService.csproj

# restore package deps
RUN dotnet restore Carsties.sln

# Copy the app folders over
COPY SearchService SearchService
COPY Contracts Contracts
WORKDIR /app/SearchService
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SearchService.dll"]