#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY /src/StudyProject.WebApi/StudyProject.WebApi.csproj src/StudyProject.WebApi/
COPY ["src/StudyProject.Secutity/StudyProject.Secutity.csproj", "src/StudyProject.Secutity/"]
COPY ["src/StudyProject.Application/StudyProject.Application.csproj", "src/StudyProject.Application/"]
COPY ["src/StudyProject.Domain/StudyProject.Domain.csproj", "src/StudyProject.Domain/"]
COPY ["src/StudyProject.Infra.Data/StudyProject.Infra.Repository.csproj", "src/StudyProject.Infra.Data/"]
COPY ["src/StudyProject.Infra.Context/StudyProject.Infra.Context.csproj", "src/StudyProject.Infra.Context/"]
COPY ["src/StudyProject.CrossCutting.Ioc/StudyProject.CrossCutting.Ioc.csproj", "src/StudyProject.CrossCutting.Ioc/"]
RUN dotnet restore "src/StudyProject.WebApi/StudyProject.WebApi.csproj"
COPY . .
WORKDIR "/src/src/StudyProject.WebApi"
RUN dotnet build "StudyProject.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudyProject.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyProject.WebApi.dll"]
