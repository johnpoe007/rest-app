# Use the .NET 7.0 SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy the .csproj and restore the NuGet packages
COPY ["rest-app.csproj", "./"]
RUN dotnet restore "rest-app.csproj"

# Copy the rest of the code and build the project
COPY . .
RUN dotnet build "rest-app.csproj" -c Release -o /app/build

# Use the .NET 7.0 runtime as the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

# Set the working directory
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/build .

# Set the entry point to run your application
ENTRYPOINT ["dotnet", "rest-app.dll"]