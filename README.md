# Models To Typescript

-- Converts c# classes to typescript interfaces

-- Only looks for public properties

-- Ignores c# constructors

-- Imports all required dependencies for typescript models

-- Matches the directory structure of the dto's, however it only checks 1 lower directory from *Working Directory*

## Options
*Working Directory* is the input directory of the cs dtos

*Convert Directory* is the output directory of the ts interfaces

## Example .csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp1.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="MTT" Version="0.1.0-*" PrivateAssets="All" />
  </ItemGroup>

  <Target Name="Convert" BeforeTargets="PrepareForBuild">
    <ConvertMain WorkingDirectory="Resources/" ConvertDirectory="models/"/>
  </Target>

</Project>
```

## Types
It correctly converts the following C# types to the equivalent typescript:

* byte
* sbyte
* decimal
* double
* float
* int
* uint
* long
* ulong
* short
* ushort            
* bool
* string
* DateTime
* IEnumerable
* ICollection

## Notes
**If a *Convert Directory* is supplied, it will be deleted everytime script is ran and will be remade**

Does not apply the keyword *Resource* from the C# models to the Typescript models

Does not apply class inheritence

Follows the case and naming conventions of each language.

Thanks to natemcmaster [this project](https://github.com/natemcmaster/msbuild-tasks) really helped me out!
