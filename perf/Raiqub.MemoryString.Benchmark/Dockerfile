FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["perf/Raiqub.MemoryString.Benchmark/Raiqub.MemoryString.Benchmark.csproj", "Raiqub.MemoryString.Benchmark/"]
RUN dotnet restore "perf/Raiqub.MemoryString.Benchmark/Raiqub.MemoryString.Benchmark.csproj"
COPY . .
WORKDIR "/src/Raiqub.MemoryString.Benchmark"
RUN dotnet build "Raiqub.MemoryString.Benchmark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Raiqub.MemoryString.Benchmark.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Raiqub.MemoryString.Benchmark.dll"]
