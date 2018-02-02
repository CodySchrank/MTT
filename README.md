# Models To Typescript

Converts c# models to typescript

Right now it:

-- Only converts public properties 

-- Matches the directory structure of the models, however it only checks 1 lower directory from *Working Directory*

## Options
*Working Directory* is the input directory of the cs models

*Convert Directory* is the output directory of the ts models

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

## Notes
Does not apply the keyword *Resource* from the C# models to the Typescript models
If a *Convert Directory* is supplied, it will be deleted everytime script is ran and remade
Follows the case and naming conventions of each language.
C# is Pascal Case
Typescript is Camel Case

## WARNING
-- Does not apply inheritence

-- Does not convert enums

-- Does not look for private properties


