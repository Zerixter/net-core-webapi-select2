# net-core-webapi-select2
select2 and net core webapi integration

Artícles interessants:

###[Goodbye Web API: Your Guide to RESTful APIs with ASP.NET Core](https://stackify.com/asp-net-core-web-api-guide/)

>When ASP.NET Core was released, Microsoft and the .NET community decided to merge the functionality of MVC and Web API. This makes sense since the two have always been very similar. We went through the process of making an ASP.NET Core Web API with various scenarios and came up with these tips for anyone out there wanting to do the same. Are you looking to implement a Web API with ASP.NET Core? Here’s how to accomplish exactly that.

###[Scaffolding with ASP.Net](https://dev.to/andre2w/scaffolding-with-aspnet)

>One of the things that I really like in rails is tha hability to generate files using the scaffolding through the CLI, and recently I've started to learn ASP.Net Core.

###Passes de la pràctica

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

Crear [model](./Models/Persona.cs) i [dbcontext](./Models/MyContext.cs).

Afegir paquest sqlite i entity framework:

```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet
```

Ull! les Tools d'EF són un ```DotNetCliToolReference```.

Cal fer les migracions:

```
dotnet build
dotnet ef migrations add "Migracio Inicial"
dotnet ef database update
```

[Fem la pàgina Razor que ens crearà les persones](./Pages/AddRandomPeople.cshtml.cs). Amb Core el context el rebrem per paràmetre perquè així ho hem configurat a l' [Startup.cs](./Startup.cs)

