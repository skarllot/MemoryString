FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["perf/MemoryString.Benchmark/MemoryString.Benchmark.csproj", "MemoryString.Benchmark/"]
RUN dotnet restore "perf/MemoryString.Benchmark/MemoryString.Benchmark.csproj"
COPY . .
WORKDIR "/src/MemoryString.Benchmark"
RUN dotnet build "MemoryString.Benchmark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MemoryString.Benchmark.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MemoryString.Benchmark.dll"]
