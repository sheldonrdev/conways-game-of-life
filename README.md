# Conway's Game of Life

A .NET 10 console application implementing [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) with configurable grid size and generation count.

For detailed architectural decisions, design rationale, and development approach, see the [Solution Architecture](LifeApp.Docs/SOLUTIONARCHITECTURE.md).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Getting Started

### Build

```bash
dotnet build
```

### Run

```bash
cd LifeApp
dotnet run
```

The application will prompt for:
1. **Rows**: Grid height
2. **Columns**: Grid width
3. **Generations**: Number of iterations to simulate

After the simulation completes, you can replay with a different configuration.

### Test

```bash
dotnet test
```

## Project Structure

- **LifeApp**: Console application
- **LifeApp.Tests**: xUnit tests
- **LifeApp.Docs**: Solution documentation
