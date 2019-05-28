FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HeLian.Xiaoyi.Indetity.Host/HeLian.Xiaoyi.Indetity.Host.csproj", "HeLian.Xiaoyi.Indetity.Host/"]
RUN dotnet restore "HeLian.Xiaoyi.Indetity.Host/HeLian.Xiaoyi.Indetity.Host.csproj"
COPY . .
WORKDIR "/src/HeLian.Xiaoyi.Indetity.Host"
RUN dotnet build "HeLian.Xiaoyi.Indetity.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HeLian.Xiaoyi.Indetity.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HeLian.Xiaoyi.Indetity.Host.dll"]