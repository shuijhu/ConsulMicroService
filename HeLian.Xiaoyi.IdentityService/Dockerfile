FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HeLian.Xiaoyi.IdentityService/HeLian.Xiaoyi.IdentityService.csproj", "HeLian.Xiaoyi.IdentityService/"]
RUN dotnet restore "HeLian.Xiaoyi.IdentityService/HeLian.Xiaoyi.IdentityService.csproj"
COPY . .
WORKDIR "/src/HeLian.Xiaoyi.IdentityService"
RUN dotnet build "HeLian.Xiaoyi.IdentityService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HeLian.Xiaoyi.IdentityService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HeLian.Xiaoyi.IdentityService.dll"]