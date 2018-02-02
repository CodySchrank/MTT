# Models To Typescript

Converts c# models to typescript

Right now it:

-- Only converts public properties 

-- Matches the directory structure of the models, however it only Checks 1 lower directory from working dir

-- Does not apply inheritence

## Options
*Working Directory* is the directory of the cs models
*Convert Directory* is the directory of the ts models

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
Follows the case and naming conventions of each language.
C# is Pascal Case
Typescript is Camel Case