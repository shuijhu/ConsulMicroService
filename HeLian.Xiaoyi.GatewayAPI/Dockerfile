FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HeLian.Xiaoyi.GatewayAPI/HeLian.Xiaoyi.GatewayAPI.csproj", "HeLian.Xiaoyi.GatewayAPI/"]
RUN dotnet restore "HeLian.Xiaoyi.GatewayAPI/HeLian.Xiaoyi.GatewayAPI.csproj"
COPY . .
WORKDIR "/src/HeLian.Xiaoyi.GatewayAPI"
RUN dotnet build "HeLian.Xiaoyi.GatewayAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HeLian.Xiaoyi.GatewayAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HeLian.Xiaoyi.GatewayAPI.dll"]