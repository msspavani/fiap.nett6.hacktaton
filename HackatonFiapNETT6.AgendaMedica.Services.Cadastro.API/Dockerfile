FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY HackatonFiapNETT6.AgendaMedica.Services.Auth.API/*.csproj ./HackatonFiapNETT6.AgendaMedica.Services.Auth.API/
COPY HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API/*.csproj ./HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API/
COPY HackatonFiapNETT6.AgendaMedica.Services.Consulta.API/*.csproj ./HackatonFiapNETT6.AgendaMedica.Services.Consulta.API/
COPY HackatonFiapNETT6.AgendaMedica.Services.Agenda.API/*.csproj ./HackatonFiapNETT6.AgendaMedica.Services.Agenda.API/
COPY HackatonFiapNETT6.AgendaMedica.Shared/*.csproj ./HackatonFiapNETT6.AgendaMedica.Shared/
COPY HackatonFiapNETT6.AgendaMedica.Messaging/*.csproj ./HackatonFiapNETT6.AgendaMedica.Messaging/

RUN dotnet restore ./HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API/HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.csproj

COPY . .

WORKDIR /src/HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HackatonFiapNETT6.AgendaMedica.Services.Cadastro.API.dll"]
