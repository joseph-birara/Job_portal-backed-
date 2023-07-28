FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5294
EXPOSE 7275

ENV ASPNETCORE_URLS=http://+:5294

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["web.csproj", "./"]
RUN dotnet restore "web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "web.dll"]
