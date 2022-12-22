#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/StudyProject.UI.Web/StudyProject.UI.WebCore.csproj", "src/StudyProject.UI.Web/"]
COPY ["src/StudyProject.CrossCutting.Ioc/StudyProject.CrossCutting.Ioc.csproj", "src/StudyProject.CrossCutting.Ioc/"]
COPY ["src/StudyProject.Application/StudyProject.Application.csproj", "src/StudyProject.Application/"]
COPY ["src/StudyProject.Domain/StudyProject.Domain.csproj", "src/StudyProject.Domain/"]
COPY ["src/StudyProject.Infra.Data/StudyProject.Infra.Repository.csproj", "src/StudyProject.Infra.Data/"]
COPY ["src/StudyProject.Infra.Context/StudyProject.Infra.Context.csproj", "src/StudyProject.Infra.Context/"]
RUN dotnet restore "src/StudyProject.UI.Web/StudyProject.UI.WebCore.csproj"
COPY . .
WORKDIR "/src/src/StudyProject.UI.Web"
RUN dotnet build "StudyProject.UI.WebCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudyProject.UI.WebCore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyProject.UI.WebCore.dll"]