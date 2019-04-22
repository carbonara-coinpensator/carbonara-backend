FROM microsoft/dotnet:sdk

# Copy resources to root directory (relative path in csproj)
WORKDIR /
COPY ./Resources ./Resources

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Carbonara/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY Carbonara/. ./
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/Carbonara.dll"]
