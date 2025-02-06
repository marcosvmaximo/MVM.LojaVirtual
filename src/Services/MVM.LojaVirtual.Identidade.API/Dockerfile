FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/MVM.LojaVirtual.Identidade/MVM.LojaVirtual.Identidade.csproj", "src/Services/MVM.LojaVirtual.Identidade/"]
RUN dotnet restore "src/Services/MVM.LojaVirtual.Identidade/MVM.LojaVirtual.Identidade.csproj"
COPY . .
WORKDIR "/src/src/Services/MVM.LojaVirtual.Identidade"
RUN dotnet build "MVM.LojaVirtual.Identidade.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MVM.LojaVirtual.Identidade.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVM.LojaVirtual.Identidade.dll"]
