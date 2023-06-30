[![.NET](https://github.com/mbaier1/Skyrim.Api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mbaier1/Skyrim.Api/actions/workflows/dotnet.yml)

# Skyrim.Api

## PLEASE READ!

This is part of my project portfolio and is made public for display purposes only. Feel free to have fun with it, but I ask that you would not edit the code without my consent. If you wish to pair on an issue or future enhance, please contact me at the information provided on my profile. Paired-Programming is great!

## Entity Framework Notes

If you create a new table based on the current TPC Mapping Strategy, check the Migrations because it is most likely going to attempt to drop
one of the Abstract classes as a table, which does not exist. If you see this, delete that specific MigrationBuilder.
