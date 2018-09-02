FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

RUN ["mkdir", "./SuperHero.Web", "./SuperHero.Application", "./SuperHero.CrossCutting", "./SuperHero.Domain", "./SuperHero.Infrastructure"]

COPY SuperHero/SuperHero.Web/*.csproj ./SuperHero.Web
COPY SuperHero/SuperHero.Application/*.csproj ./SuperHero.Application
COPY SuperHero/SuperHero.CrossCutting/*.csproj ./SuperHero.CrossCutting
COPY SuperHero/SuperHero.Domain/*.csproj ./SuperHero.Domain
COPY SuperHero/SuperHero.Infrastructure/*.csproj ./SuperHero.Infrastructure
WORKDIR /app/SuperHero.Web
RUN dotnet restore

WORKDIR /app
COPY SuperHero/SuperHero.Web/. ./SuperHero.Web
COPY SuperHero/SuperHero.Application/. ./SuperHero.Application
COPY SuperHero/SuperHero.CrossCutting/. ./SuperHero.CrossCutting
COPY SuperHero/SuperHero.Domain/. ./SuperHero.Domain
COPY SuperHero/SuperHero.Infrastructure/. ./SuperHero.Infrastructure
WORKDIR /app/SuperHero.Web
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/SuperHero.Web/out ./
ENTRYPOINT ["dotnet", "SuperHero.Web.dll"]