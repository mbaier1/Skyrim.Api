[![.NET](https://github.com/mbaier1/Skyrim.Api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mbaier1/Skyrim.Api/actions/workflows/dotnet.yml)

# Skyrim.Api

## Entity Framework Notes

If you create a new table based on the current TPC Mapping Strategy, check the Migrations because it is most likely going to attempt to drop
one of the Abstract classes as a table, which does not exist. If you see this, delete that specific MigrationBuilder.
