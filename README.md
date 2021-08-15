# C# DTOs to Typescript Interfaces

MTT generates TypeScript interfaces from .NET DTOs. It implements most major features of the current TypeScript specification. This utility could be preferred over some others as it is completely independent of your IDE or workflow, because it uses a MSBUILD task and converts the code directly from the source. This means its great for .Net Core and VS Code.

## Install

Using dotnet CLI:

`dotnet add package MTT`

`dotnet restore`

Then in .csproj add a Target.

## Options

_WorkingDirectory_ is the input directory of the c# dtos (seperate multiple directories with ';')

_ConvertDirectory_ is the output directory of the ts interfaces

_AutoGeneratedTag_ (default true) show "/\* Auto Generated \*/" at the top of every file.  If set to _false_ then no tags are generated

_EnumValues_ (default int enums) if set to _Strings_, generates typescript [string enums](https://www.typescriptlang.org/docs/handbook/enums.html#string-enums)

_PathStyle_ (default is folders stay the same and files become camelCase) if set to _Kebab_, changes the file and directory names to [kebab-case](https://wiki.c2.com/?KebabCase).

_PropertyStyle_ (default is CamelCase) if set to _PascalCase_, changes the properties to [PascalCase](https://wiki.c2.com/?PascalCase).

## Example

### .csproj

```XML
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="MTT" Version="0.7.2"/>
  </ItemGroup>

  <Target Name="Convert" BeforeTargets="PrepareForBuild">
    <ConvertMain WorkingDirectory="Resources/" ConvertDirectory="models/"/>
  </Target>

</Project>
```

### Vehicle.cs

```C#
using System.Collections.Generic;
using Example.Resources.Parts;
using Example.Resources.Parts.Unit;

namespace Example.Resources.Vehicles
{
    public abstract class Vehicle : Entity 
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Mileage;
        public Dictionary<string, Units> Options { get; set; }
        public VehicleState Condition { get; set; }  // this is an enum of type int
        public virtual ICollection<Part> Parts { get; set; }
        public IList<Part> SpareParts { get; set; } = new List<Part>();
    }
}
```

### vehicle.ts

```ts
/* Auto Generated */

import { Entity } from "./../entity";
import { Units } from "./../Parts/Unit/units";
import { VehicleState } from "./vehicleState";
import { Part } from "./../Parts/part";

export interface Vehicle extends Entity {
    year: number;
    make: string;
    model: string;
    mileage?: number;
    options: Partial<Record<string, Units>>;
    condition: VehicleState;
    parts: Part[];
    spareParts: Part[];
}

```

## Types

It correctly converts the following C# types to the equivalent typescript:

* bool
* byte
* decimal
* double
* float
* int
* uint
* long
* sbyte
* short
* string
* ulong
* ushort
* Boolean
* Byte
* Char
* DateTime
* Decimal
* Double
* Int16
* Int32
* Int64
* SByte
* UInt16
* UInt32
* UInt64
* Array
* Collection
* Enumerbale
* IEnumerable
* ICollection
* IList
* Enum
* Optional
* virtual
* abstract
* Dictionary
* IDictionary
* Guid

## Notes

**If a _Convert Directory_ is supplied, it will be deleted everytime script is ran and will be remade**

Comments like `//` are ignored in c# files.  Comments like `/* */` could cause undefined behavior.

Tested on Windows 10, macOS Big Sur, and Ubuntu 18.04

Follows the case and naming conventions of each language.

Thanks to natemcmaster [this project](https://github.com/natemcmaster/msbuild-tasks) really helped me out!