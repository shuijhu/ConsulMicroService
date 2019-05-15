FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["HeLian.Xiaoyi.UserService/HeLian.Xiaoyi.UserService.csproj", "HeLian.Xiaoyi.UserService/"]
RUN dotnet restore "HeLian.Xiaoyi.UserService/HeLian.Xiaoyi.UserService.csproj"
COPY . .
WORKDIR "/src/HeLian.Xiaoyi.UserService"
RUN dotnet build "HeLian.Xiaoyi.UserService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HeLian.Xiaoyi.UserService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HeLian.Xiaoyi.UserService.dll"]