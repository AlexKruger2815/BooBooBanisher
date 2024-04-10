# Stage 1: Restore dependencies
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR bbb/
COPY *.csproj ./
# RUN dotnet restore

# Stage 2: Build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 3: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN ls
WORKDIR .
COPY --from=build /bbb/out .
ENTRYPOINT ["dotnet", "bbb.dll"]