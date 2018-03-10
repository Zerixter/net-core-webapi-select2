# net-core-webapi-select2
select2 and net core webapi integration


Fem l'aplicació com a MVC razor:

```
mkdir net-core-webapi-select2
cd net-core-webapi-select2
dotnet new razor
```

Afegim paquet per assistent scaffolding:

```
dotnet add package Microsoft.AspNetCore.All -v 2.0.5
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 2.0.2
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Tools -v 2.0.2
dotnet restore
```

Amb compte amb el fitxer del projecte, després d'afegir els paquets s'ha de fixar:

```
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.5" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="2.0.2" />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.2" />
  </ItemGroup>
</Project>
```
