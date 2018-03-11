FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
RUN git clone https://github.com/ctrl-alt-d/net-core-webapi-select2.git
WORKDIR /app/net-core-webapi-select2
RUN dotnet restore
RUN dotnet build
RUN dotnet ef database update
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
RUN mkdir dades
COPY --from=build-env /app/net-core-webapi-select2/dades/* dades
ENTRYPOINT ["dotnet", "net-core-webapi-select2.dll"]
